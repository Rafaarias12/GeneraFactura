
import { Injectable } from '@angular/core';
import { HttpClient, HttpBackend } from '@angular/common/http';
import { Observable } from 'rxjs';
import { factura } from '../models/factura.model';
import { environment } from '../../environments/environment';

@Injectable({
  providedIn: 'root'
})
export class FacturaService {
  private apiUrl = environment.apiUrl;
  private http: HttpClient;

  constructor(handler: HttpBackend) {
    this.http = new HttpClient(handler);
  }

  getFacturas(): Observable<factura[]> {
    return this.http.get<factura[]>(`${this.apiUrl}factura`);
  }

  addFactura(obj: factura): Observable<boolean>{
    return this.http.post<boolean>(`${this.apiUrl}factura`, obj)
  }

  getFacturasId(id: number): Observable<factura>{
    return this.http.get<factura>(`${this.apiUrl}factura/` + id)
  }

  putFactura(id: number, obj: factura): Observable<boolean>{
    return this.http.put<boolean>(`${this.apiUrl}factura/` + id, obj);
  }

  deleteFactura(id:number): Observable<boolean>{
    return this.http.delete<boolean>(`${this.apiUrl}factura/`+ id);
  }
}
