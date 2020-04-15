using System;
using System.Collections.Generic;

namespace ProAgil.Domain
{
    public class Evento
    {
        public int ID { get; set; }
        public string Local { get; set; }
        public DateTime DataEvento { get; set;}
        public string Tema { get; set; }
        public int QtdPessoas { get; set; }
        public string Telefone { get; set; }
        public string Email { get; set; }
        public List<Lote> lote { get; set; }
        public List<RedeSocial> RedesSociais { get; set; }
        public List<PalestranteEvento> PalestrantesEventos { get; set; }
        public string ImagemUrl { get; set; }
    }
}