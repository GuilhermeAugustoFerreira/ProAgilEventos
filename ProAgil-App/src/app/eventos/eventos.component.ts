import { Component, OnInit, TemplateRef } from '@angular/core';
import { stringify } from 'querystring';
import { EventoService } from '../_services/evento.service';
import { Evento } from '../_models/Evento';
import { BsModalRef, BsModalService } from 'ngx-bootstrap/modal';
import { BsDatepickerModule } from 'ngx-bootstrap/datepicker';
import { FormGroup, FormControl, Validators, FormBuilder } from '@angular/forms';
import { BsLocaleService } from 'ngx-bootstrap/datepicker';
import { ptBrLocale } from 'ngx-bootstrap/locale';
import { defineLocale } from 'ngx-bootstrap/chronos';
//import { defineLocale, } from 'ngx-bootstrap';

defineLocale('pt-br', ptBrLocale);

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
  evento: Evento;
  ImagemLargura = 50;
  ImagemMargem = 2;
  mostrarImagem = false;
  modalRef: BsModalRef;
  registerForm: FormGroup;
  bodyDeletarEvento = '';

  _filtroLista = '';

  constructor(private eventoService: EventoService,
              private modalService: BsModalService,
              private fb: FormBuilder,
              private localeService: BsLocaleService
              ) { 
                this.localeService.use('pt-br');
              }

  get filtroLista(): string{
    return this._filtroLista;
  }
  set filtroLista(value: string){
    this._filtroLista = value;
    this.eventosFiltrados = this.filtroLista ? this.filtrarEventos(this.filtroLista) : this.eventos;
  }

  openModal(template: TemplateRef<any>) {
  // openModal(template: any){
    this.registerForm.reset();
    this.modalRef = this.modalService.show(template);
    // template.show();
  }

  openModalEdit(_evento: Evento, template: TemplateRef<any>) {
    // openModal(template: any){
      this.evento = _evento;
      this.registerForm.reset();
      this.modalRef = this.modalService.show(template);
      // template.show();
    }

  excluirEvento(evento: Evento, template: TemplateRef<any>) {
    this.evento = evento;
    this.registerForm.reset();
    this.modalRef = this.modalService.show(template);
    this.bodyDeletarEvento = `Tem certeza que deseja excluir o Evento: ${evento.tema}, Código: ${evento.id}`;
  }
  
  confirmeDelete(evento: Evento, template: any) {
    this.eventoService.deleteEvento(evento.id, evento).subscribe(
      () => {
          //this.modalRef = this.modalService.show(template);
          this.modalRef = this.modalService.show(template);
          this.getEventos();
        }, error => {
          console.log(error);
        }
    );
  }

  filtrarEventos(filtrarPor: string): any{
    filtrarPor = filtrarPor.toLocaleUpperCase();
    return this.eventos.filter(
      evento => evento.tema.toLocaleUpperCase().indexOf(filtrarPor) !== -1
    );
  }

  ngOnInit() {
    this.getEventos();
    this.validation();
  }

  AlternarImagem(){
    this.mostrarImagem = !this.mostrarImagem;
  }

  validation(){
    this.registerForm = this.fb.group({
      tema: ['', [Validators.required, Validators.minLength(4), Validators.maxLength(50)]],
      local: ['', Validators.required],
      dataEvento: ['', Validators.required],
      qtdPessoas: ['', [Validators.required, Validators.max(20000)]],
      imagemUrl: ['', Validators.required],
      telefone: ['', Validators.required],
      email: ['', [Validators.required, Validators.email]],
    });
  }

  salvarAlteracao(template: TemplateRef<any>){
    if(this.registerForm.valid){
      this.evento = Object.assign({}, this.registerForm.value);
      this.eventoService.postEvento(this.evento).subscribe(
        (novoEvento: Evento) => {
          console.log(novoEvento);
          console.log("chamou salvarAlteracao")
          this.modalRef = this.modalService.show(template);
          this.getEventos();
        }, error => {
          console.log(error.stringify);
        }
      )

    }
  }

  editarEvento(evento: Evento, id: number, template: TemplateRef<any>){
    if(this.registerForm.valid){
      evento = Object.assign({}, this.registerForm.value);// ESTA INSERINDO O VALOR QUE ESTA NO FORM NO OBJETO EVENTO
      evento.id = id;
      this.eventoService.putEvento(id, evento).subscribe(
        (novoEvento: Evento) => {
          console.log(novoEvento);
          console.log("chamou editarEvento "+evento.tema)
          this.modalRef = this.modalService.show(template);
          this.getEventos();
        }, error => {
          console.log(error.stringify);
        }
      )

    }
  }


  // editarEvento(evento: Evento, template: TemplateRef<any>){
  //   if(this.registerForm.valid){
  //     //this.evento = Object.assign({}, this.registerForm.value);
  //     //evento = Object.assign({}, this.registerForm.value);
  //     this.eventoService.putEvento(evento.id, evento).subscribe(
  //       (novoEvento: Evento) => {
  //         console.log(novoEvento);
  //         console.log("chamou editarEvento "+evento.tema+evento.id)
  //       }, error => {
  //         console.log(error.stringify);

  //       }
  //     )

  //   }
  // }


  getEventos(){
    this.eventoService.getAllEvento().subscribe((_eventos: Evento[]) => {
      this.eventos = _eventos;
      this.eventosFiltrados = this.eventos;
      console.log(_eventos);
    },
      error => {console.log(stringify(error));}
    );
  }

  

  // getEventosById(id: number){
  //   this.eventoService.getEventoById(id).subscribe((_evento: Evento) => {
  //     this.evento = _evento;
  //     this.eventosFiltrados = this.eventos;
  //     console.log(_evento);
  //   },
  //     error => {console.log(stringify(error));}
  //   );
  // }

}
