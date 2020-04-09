import { Component, OnInit } from '@angular/core';
import { HttpClient } from '@angular/common/http';
import { stringify } from 'querystring';

@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
})
export class EventosComponent implements OnInit {

  /*eventos: any = [
    {
      EventoId: 1,
      Tema:'Angular',
      Local: 'BH'
    },

    {
      EventoId: 2,
      Tema:'Dotnet',
      Local: 'Lafaiete'
    },

    {
      EventoId: 3,
      Tema:'Angular e Dotnet',
      Local: 'Araxa'
    },

  ]*/
  eventos: any;

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getEventos();
  }

  getEventos(){
    this.eventos = this.http.get('http://localhost:5000/api/values').subscribe(
      response => {this.eventos = response;
      console.log(response);},
      error => {console.log(stringify(error));}
    );
  }

}
