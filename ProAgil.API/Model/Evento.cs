namespace ProAgil.API.Model
{
    public class Evento
    {
        public int EventoID { get; set; }
        public string Local { get; set; }
        public string DataEvento { get; set;}
        public string Tema { get; set; }
        public int QtdPessoas { get; set; }
        public string lote { get; set; }
        public string ImagemUrl { get; set; }
    }
}