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
  eventosFiltrados: any = [];
  eventos: any = [];
  ImagemLargura = 50;
  ImagemMargem = 2;
  mostrarImagem = false;
  _filtroLista: string;
  get filtroLista(): string{
    return this._filtroLista;
  }
  set filtroLista(value: string){
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }

  filtrarEventos(filtrarPor: string): any{
    filtrarPor = filtrarPor.toLocaleUpperCase();
    return this.eventos.filter(
      evento => evento.tema.toLocaleUpperCase().indexOf(filtrarPor) !== -1
    );
  }

  constructor(private http: HttpClient) { }

  ngOnInit() {
    this.getEventos();
  }

  AlternarImagem(){
    this.mostrarImagem = !this.mostrarImagem;
  }

  getEventos(){
    this.eventos = this.http.get('http://localhost:5000/api/values').subscribe(
      response => {this.eventos = response;
      console.log(response);},
      error => {console.log(stringify(error));}
    );
  }

}
