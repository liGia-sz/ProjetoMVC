using System;
using System.Collections.Generic;

namespace BibliotecaMVCApp.Models
{
    public class Autor
    {
        public int Id { get; set; }
        public string Nome { get; set; }
        public DateTime DataNascimento { get; set; }
        public ICollection<Livro> Livros { get; set; } = new List<Livro>();
    }
}