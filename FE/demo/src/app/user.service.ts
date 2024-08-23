import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { map, Observable } from 'rxjs';
import { User } from './Model/user.model';
import { CreateUserRequest } from './Model/create.user.model';

@Injectable({
  providedIn: 'root'
})
export class UserService {
  private apiUrl = 'https://localhost:44383/api/User'; 

  constructor(private http: HttpClient) {}

  getUsers(searchParams: { username?: string, email?: string, role?: string }): Observable<User[]> {
    let params = new HttpParams();
    
    if (searchParams.username) {
      params = params.set('username', searchParams.username);
    }
    if (searchParams.email) {
      params = params.set('email', searchParams.email);
    }
    if (searchParams.role) {
      params = params.set('role', searchParams.role);
    }
    return this.http.get<User[]>(this.apiUrl,{params}).pipe(map((response: any) => {return response.content;} ));
  }

  getUserById(id: string): Observable<User> {
    return this.http.get<User>(`${this.apiUrl}/${id}`);
  }

  addUser(user: CreateUserRequest): Observable<CreateUserRequest> {
    return this.http.post<CreateUserRequest>(this.apiUrl, user);
  }

  updateUser(user: CreateUserRequest): Observable<CreateUserRequest> {
    return this.http.put<CreateUserRequest>(this.apiUrl, user);
  }

  deleteUser(id: string): Observable<void> {
    console.log("is this going in"+ `${this.apiUrl}/${id}`);
    return this.http.delete<void>(`${this.apiUrl}/${id}`);
  }
}