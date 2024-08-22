import { Component, EventEmitter, Output } from '@angular/core';
import { UserService } from '../user.service';
import { FormsModule } from '@angular/forms';
import { CreateUserRequest } from '../Model/create.user.model';
import { Router } from '@angular/router';

@Component({
  selector: 'app-user-form',
  standalone: true,
  imports: [FormsModule],
  templateUrl: './user-form.component.html',
  styleUrl: './user-form.component.scss'
})
export class UserFormComponent {
  @Output() userAdded = new EventEmitter<CreateUserRequest>();
  isEditMode: boolean = false;
  user: CreateUserRequest = {
    id: '',
    username: '', email: '', role: 'User',
    password: ''
  };

  constructor(private userService: UserService, private router: Router) { }

  ngOnInit(): void {
    if (this.user.username) {
      this.isEditMode = true;
    }
  }
  onSubmit(): void {
    if (this.isEditMode) {
      this.userService.updateUser(this.user).subscribe(() => this.router.navigate(['/users']));
    } else {
      this.userService.addUser(this.user).subscribe(newUser => {
        this.userAdded.emit(newUser);
        this.user = { id: '', username: '', password: '', email: '', role: 'User' };
      });;//.subscribe(() => this.router.navigate(['/users']));
    }
  }

  onCancel(): void {
    this.router.navigate(['/users']);
  }

  addUser(): void {
    this.userService.addUser(this.user).subscribe(newUser => {
      this.userAdded.emit(newUser);
      this.user = { id: '', username: '', password: '', email: '', role: 'User' };
    });
  }
}