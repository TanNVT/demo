import { Component, Inject, inject } from '@angular/core';
import { User } from '../Model/user.model';
import { UserService } from '../user.service';
import { MAT_DIALOG_DATA, MatDialogModule, MatDialogRef } from '@angular/material/dialog';
import { CreateUserRequest } from '../Model/create.user.model';
import { BrowserModule } from '@angular/platform-browser';
import { BrowserAnimationsModule } from '@angular/platform-browser/animations';
import { MatFormFieldModule } from '@angular/material/form-field';
import { MatInputModule } from '@angular/material/input';
import { MatButtonModule } from '@angular/material/button';
import { FormsModule } from '@angular/forms';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-user-dialog',
  standalone: true,
  imports: [
    MatDialogModule,
    MatFormFieldModule,
    MatInputModule,
    MatButtonModule,FormsModule,CommonModule],
  templateUrl: './user-dialog.component.html',
  styleUrl: './user-dialog.component.scss'
})
export class UserDialogComponent {
  user: CreateUserRequest = { id: '',password: '',username: '', email: '', role: '' };
  isEditMode: boolean = false;
  constructor(
    private userService: UserService,
    private dialogRef: MatDialogRef<UserDialogComponent>,
    @Inject(MAT_DIALOG_DATA) public data: any
  ) 
  {
    console.log(data);
    this.user = data.user
    this.isEditMode = data.isEditMode
  }

  onSubmit(): void {
    if (this.isEditMode) {
      this.userService.updateUser(this.user).subscribe({
        next: () => this.dialogRef.close(true),
        error: (err) => console.error('Error updating user:', err)
      });
    } else {
      this.userService.addUser(this.user).subscribe({
        next: () => this.dialogRef.close(true),
        error: (err) => console.error('Error adding user:', err)
      });
    }
  }

  onCancel(): void {
    this.dialogRef.close(false); 
  }

}
