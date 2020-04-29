import { Injectable } from '@angular/core';
import { HttpClient, HttpParams } from '@angular/common/http';
import { Observable } from 'rxjs';
import { Evento } from '../_models/Evento';
import { JsonPipe } from '@angular/common';

@Injectable({
  providedIn: 'root'// injeta service na aplicação inteira
})
// let options = {params: HttpParams};

export class EventoService {
  baseURL = 'http://localhost:5000/api/evento';
  baseURLput = 'http://localhost:5000/api/evento/8';
  constructor(private http: HttpClient) { }
  

  getAllEvento(): Observable<Evento[]> {
    return this.http.get<Evento[]>(this.baseURL);
  }

  getEventoByTema(tema: string): Observable<Evento[]> {
    return this.http.get<Evento[]>(`${this.baseURL}/getByTema/${tema}`);
  }

  getEventoById(id: number): Observable<Evento[]> {
    return this.http.get<Evento[]>(`${this.baseURL}/${id}`);
  }
  
  postEvento(evento: Evento) {
    return this.http.post(this.baseURL, evento);
  }

  // putEvento(id: number, evento: Evento) {
  //   return this.http.put(this.baseURL, id, evento);
  // }

  putEvento(id:number, evento: Evento) {
    console.log("chamou putEvento "+evento.tema+"   "+evento.id+"   "+id)
    //return this.http.put(`${this.baseURL}/${id}`, id, evento);
    //return this.http.put(this.baseURLput, id, evento);
    return this.http.put(`${this.baseURL}/${id}`, evento);
    // $"/api/evento/{model.ID}"
  }

  deleteEvento(id:number, evento: Evento) {
    console.log("chamou deleteEvento "+"    "+id)
    // return this.http.put(`${this.baseURL}/${id}`, id, evento);
    // return this.http.put(this.baseURLput, id, evento);
    // return this.http.delete(this.baseURL);
    return this.http.delete(`${this.baseURL}/${id}`);

  }

  

}

