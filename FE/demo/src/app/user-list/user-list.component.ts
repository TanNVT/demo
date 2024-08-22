import { Component, OnInit } from '@angular/core';
import { User } from '../Model/user.model';
import { UserService } from '../user.service';
import { CommonModule, NgFor } from '@angular/common';
import { FormsModule } from '@angular/forms';

@Component({
  selector: 'app-user-list',
  standalone: true,
  imports: [CommonModule,FormsModule],
  templateUrl: './user-list.component.html',
  styleUrl: './user-list.component.scss'
})
export class UserListComponent implements OnInit {
  users: User[] = [];
  searchUsername: string = '';
  searchEmail: string = '';
  searchRole: string = '';

  constructor(private userService: UserService) { }

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
}