using AutoMapper;
using Livraria.DataContext;
using Livraria.Exceptions;
using Livraria.Models;
using Livraria.repository;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel;
using System.Net;

namespace Livraria.service
{
    public class LivroService : ILivroRepository
    {
        readonly ApplicationDbContext _context;
        readonly IMapper _mapper;
        readonly IAutorRepository _repository;
    
        public LivroService(ApplicationDbContext context, IMapper mapper, IAutorRepository repository)
        {
            _context = context;
            _mapper = mapper;
            _repository = repository;
        }



        public async Task<Livro> CreateLivroAscync(Livro livro)
        {
            Livro livroAdicionado = livro;
            
            if (livroAdicionado != null)
            {
                await _context.Livro.AddAsync(livro);
                await _context.SaveChangesAsync();
                return livroAdicionado;
            }
            else
            {
                throw new HttpStatusCodeException(HttpStatusCode.BadRequest ,"Não foi inserido dados Validos para inserção do livro!");
            }
            
            
        }

        public async Task DeleteLivroAscync(int id)
        {
            Livro livro = await _context.Livro.FindAsync(id);
            if (livro != null)
            {
                _context.Livro.Remove(livro);
                await _context.SaveChangesAsync();
            }
        }

        public async Task<List<Livro>> getAllAscync()
        {
            return await _context.Livro.Include(livro => livro.Autores).ThenInclude(la => la.Autor).
                ToListAsync();
        }

        public async Task<Livro> GetLivroByIdAscync(int id)
        {
            try
            {
                Livro livro = await _context.Livro.FirstOrDefaultAsync(x => x.Id == id);

                return livro;
            }
            catch (Exception ex)
            {
                throw new HttpStatusCodeException(HttpStatusCode.NotFound ,"Livro não encontrado!");
            }
            
        }

       public async Task<Livro> UpdateLivroAscync(Livro livro, int id)
        {
            Livro LivroAtual = await GetLivroByIdAscync(id);
            _mapper.Map(livro, LivroAtual);
            await _context.SaveChangesAsync();
            return LivroAtual;
        }

        public async Task AssociarAutorLivro(int autorId, int livroId)
        {
            Livro Livro =  await GetLivroByIdAscync(livroId);
            var Autor = await _repository.GetAutorByIdAscync(autorId);
            LivroAutor livroAutor = new LivroAutor
            {
                AutorId = autorId,
                LivroId = livroId
            };
            Livro.associarAutor(livroAutor);
           

        }
        public async Task DesassociarAutorLivro(int autorId, int livroId)
        {
            Livro livro = await GetLivroByIdAscync(livroId);
            var Autor = await _repository.GetAutorByIdAscync(autorId);
            LivroAutor livroAutor = new LivroAutor
            {
                AutorId = autorId,
                LivroId = livroId
            };
            livro.dessassociarAutor(livroAutor);
        }
        

        }
       
    }

