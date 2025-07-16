namespace BibliotecaWebApp.Models
{
    public class Livro
    {
        public int Id { get; set; }
        public required string Titulo { get; set; }
        public int AnoPublicacao { get; set; }
        public required string Genero { get; set; }
        public string? FaixaEtaria { get; set; }
        
        //Chaves estrangeiras
        public int? AutorId { get; set; }
        public required Autor Autor { get; set; }
    }
}