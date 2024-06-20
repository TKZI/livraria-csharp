using Livraria.Exceptions;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq.Expressions;
using System.Net;

namespace Livraria.Models
{
    public class Livro
    {

        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id {  get; set; }

       
        public string Titulo { get; set; }

        [Column("ano_publicacao")]
        public int AnoPublicacao { get; set; }

        public virtual  List<LivroAutor> Autores { get; } = new List<LivroAutor>();

        public void associarAutor(LivroAutor livroAutor)
        {
            if (Autores.Contains(livroAutor))
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "Autor já está associado a este livro!");
            }

            Autores.Add(livroAutor);
        }

        public void dessassociarAutor(LivroAutor livroAutor)
        {
            if (Autores.Contains(livroAutor))
            {
                Autores.Remove(livroAutor);
            }
            else
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest, "Autor não está associado a este livro!");
            }
            

            
        }
    }
}
