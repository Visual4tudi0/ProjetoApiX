using AutoMapper;
using ClassLibrary1Service.Service;
using ClassLibraryDomain.IService;
using ClassLibraryDomain.Models;
using ClassLibraryDomain.Models.DTO;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace ProjetoWebApiX.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CategoriaController : ControllerBase
    {
        private readonly IServiceCategoria _serviceCategoria;
        private readonly IMapper _mapper;
        public CategoriaController(IServiceCategoria serviceCategoria, IMapper mapper)
        {
            _serviceCategoria = serviceCategoria;
            _mapper = mapper;
        }

        [HttpGet]
        public async Task<ActionResult<IEnumerable<Categoria>>> GetAll()
        {
            var categorias = await _serviceCategoria.ListarTodasCategorias();
            return Ok(categorias);
        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Categoria>> GetById(Guid id)
        {
            var categoria = await _serviceCategoria.BuscarCategoriaPorId(id);
            if (categoria == null) return NotFound();
            return Ok(categoria);
        }

        [HttpPost]
        public async Task<ActionResult> Post(CategoriaDto dto)
        {
            var categoria = _mapper.Map<Categoria>(dto);
            await _serviceCategoria.AdicionarCategoria(categoria);
            return CreatedAtAction(nameof(GetById), new { id = categoria.Id }, categoria);
        }
    }
}