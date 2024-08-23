import { Component, OnInit } from '@angular/core';
import { User } from '../Model/user.model';
import { UserService } from '../user.service';
import { CommonModule, NgFor } from '@angular/common';
import { FormsModule } from '@angular/forms';
import { MatDialog } from '@angular/material/dialog';
import { UserDialogComponent } from '../user-dialog/user-dialog.component';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';

@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [CommonModule,FormsModule,MatFormFieldModule,MatInputModule],
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.scss',
})
export class UserListComponent implements OnInit {
  users: User[] = [];
  searchUsername: string = '';
  searchEmail: string = '';
  searchRole: string = '';

  constructor(private userService: UserService,private dialog: MatDialog) { }

  ngOnInit(): void {
    this.loadUser();
  }
  loadUser(): void{
    this.userService.getUsers({
      username: this.searchUsername,
      email: this.searchEmail,
      role: this.searchRole
    }).subscribe(users => this.users = users);
  }
  onSearchChange(): void {
    this.loadUser();
  }

  deleteUser(id: string): void {
    console.log("this is running Delete")
    this.userService.deleteUser(id).subscribe(x =>{
      this.loadUser();
    });
  }

  openAddUserDialog(): void {
    const dialogRef = this.dialog.open(UserDialogComponent, {
      data: {
        user: { username: '', email: '', role: '' },
        isEditMode: false
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadUser(); 
      }
    });
  }

  openEditUserDialog(user: User): void {
    const dialogRef = this.dialog.open(UserDialogComponent, {
      data: {
        user: { ...user },
        isEditMode: true
      }
    });

    dialogRef.afterClosed().subscribe(result => {
      if (result) {
        this.loadUser();
      }
    });
  }
}