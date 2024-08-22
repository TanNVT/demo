import { provideHttpClient } from '@angular/common/http';
import { Component } from '@angular/core';
import { RouterOutlet } from '@angular/router';
import { UserService } from './user.service';
import { User } from './Model/user.model';
import { UserFormComponent } from './user-form/user-form.component';
import { UserListComponent } from './user-list/user-list.component';
import { CommonModule } from '@angular/common';

@Component({
  selector: 'app-root',
  standalone: true,
  imports: [RouterOutlet,UserFormComponent,UserListComponent,CommonModule],
  templateUrl: './app.component.html',
  styleUrl: './app.component.scss'
})
export class AppComponent {
  users: User[] = [];

  
  constructor(private userService: UserService) {}

  ngOnInit(): void {
    //this.userService.getUsers().subscribe(users => this.users = users);
  }

  onUserAdded(user: User): void {
    this.users.push(user);
  }
}