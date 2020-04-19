import { Component, OnInit, TemplateRef } from '@angular/core';
import { stringify } from 'querystring';
import { EventoService } from '../_services/evento.service';
import { Evento } from '../_models/Evento';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';


@Component({
  selector: 'app-eventos',
  templateUrl: './eventos.component.html',
  styleUrls: ['./eventos.component.css']
  // providers: [EventoService] --> outro jeito de se injetar o serviço
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
  ]*/
  eventosFiltrados: Evento[];
  eventos: Evento[];
  ImagemLargura = 50;
  ImagemMargem = 2;
  mostrarImagem = false;
  modalRef: BsModalRef;

  _filtroLista = '';

  constructor(private eventoService: EventoService,
              private modalService: BsModalService,
              ) { }

  get filtroLista(): string{
    return this._filtroLista;
  }
  set filtroLista(value: string){
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }

  openModal(template: TemplateRef<any>){
    this.modalRef = this.modalService.show(template);
  }

  filtrarEventos(filtrarPor: string): any{
    filtrarPor = filtrarPor.toLocaleUpperCase();
    return this.eventos.filter(
      evento => evento.tema.toLocaleUpperCase().indexOf(filtrarPor) !== -1
    );
  }

  ngOnInit() {
    this.getEventos();
  }

  AlternarImagem(){
    this.mostrarImagem = !this.mostrarImagem;
  }

  getEventos(){
    this.eventoService.getAllEvento().subscribe((_eventos: Evento[]) => {
      this.eventos = _eventos;
      this.eventosFiltrados = this.eventos;
      console.log(_eventos);
    },
      error => {console.log(stringify(error));}
    );
  }

}
