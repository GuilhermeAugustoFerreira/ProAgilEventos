namespace ProAgil.Domain
{
    public class PalestranteEvento
    {
        public int PalestranteId { get; set; }
        public int EventoId { get; set; }
        public Palestrante palestrante { get; set; }
        public Evento evento { get; set; }
    }
}