using Livraria.Models;

namespace Livraria.repository
{
    public interface ILivroRepository
    {
        Task<List<Livro>> getAllAscync();
        Task<Livro> GetLivroByIdAscync(int id);
        Task<Livro> CreateLivroAscync(Livro livro);
        Task<Livro> UpdateLivroAscync(Livro livro, int id);
        Task DeleteLivroAscync(int id);
        Task DesassociarAutorLivro(int autorId, int livroId);
        Task AssociarAutorLivro(int autorId, int livroId);
        
    }
}
