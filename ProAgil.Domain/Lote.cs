using System;

namespace ProAgil.Domain
{
    public class Lote
    {
        public int Id { get; set; }
        public int Nome { get; set; }
        public int Preco { get; set; }
        public DateTime? DataInicio { get; set; }
        public DateTime? DataFim { get; set; }
        public int quantidade { get; set; }
        public int EventoId { get; set; }
        public Evento evento { get; set; }
    }
}