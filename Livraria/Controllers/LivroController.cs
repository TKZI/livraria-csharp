﻿using AutoMapper;
using Livraria.DTO;
using Livraria.Models;
using Livraria.repository;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Livraria.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LivroController : ControllerBase
    {
        readonly ILivroRepository _repository;
        readonly IMapper _mapper;

        public LivroController(ILivroRepository repository, IMapper mapper)
        {
            _repository = repository;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<List<LivroDTO>>> GetAllLivros()
        {
            var livroDTO = _mapper.Map<List<LivroDTO>>(await _repository.getAllAscync());
            return Ok(livroDTO);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<LivroDTO>> GetById(int id)
        {
            var  livroDTO = _mapper.Map<LivroDTO>( await _repository.GetLivroByIdAscync(id));

            return  Ok(livroDTO);
        }

        [HttpPost]
        public async Task<ActionResult<LivroDTO>> CreateLivro(Livro livro)
        {
            var livroDTO = _mapper.Map<LivroDTO>(await _repository.CreateLivroAscync(livro));
            return Created("", livroDTO);
        }

        [HttpPut("{id}")]
        public async Task<ActionResult<LivroDTO>> UpdateLivro(Livro livro, int id)
        {
            var livroDTO = _mapper.Map<LivroDTO>(await _repository.UpdateLivroAscync(livro, id));

            return Ok(livroDTO);
        } 

        [HttpDelete("{id}")]
        public async Task<ActionResult> DeleteLivro(int id)
        {
            await _repository.DeleteLivroAscync(id);
            return NoContent();
        }

        [HttpDelete("{autorId}/{livroId}")]
        public async Task<ActionResult> DesassociarAutor(int autorId, int livroId)
        {
             await _repository.DesassociarAutorLivro(autorId, livroId);
            return NoContent();
        }

        [HttpPut("{autorId}/{livroId}")]
        public async Task<ActionResult>  AssociarAutor(int autorId, int livroId)
        {
            await _repository.AssociarAutorLivro(autorId, livroId);

            return Ok();
        }
    
    
    
    }
}
