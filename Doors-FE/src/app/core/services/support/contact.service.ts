import { Injectable } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { Observable } from 'rxjs';
import {environment} from '../../../../environments/environment';
import {ApiResponse} from '../../../models/auth.models';
import {ContactMessageTypeDTO, CreateContactMessageDTO} from '../../../models/support.models';

@Injectable({
  providedIn: 'root'
})
export class ContactService {
  private readonly apiUrl = `${environment.apiUrl}/contact`;

  constructor(private http: HttpClient) {}

  sendMessage(dto: CreateContactMessageDTO): Observable<ApiResponse<object>> {
    return this.http.post<ApiResponse<object>>(this.apiUrl, dto);
  }

  getMessageTypes(): Observable<ApiResponse<ContactMessageTypeDTO[]>> {
    return this.http.get<ApiResponse<ContactMessageTypeDTO[]>>(`${this.apiUrl}/message-types`);
  }
}
