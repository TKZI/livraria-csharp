namespace Livraria.DTO
{
    public class LivroDTO
    {
        public int Id { get; set; }
        public string Titulo { get; set; }
        public List<AutorDTO> Autores { get; set; }
    }
}
