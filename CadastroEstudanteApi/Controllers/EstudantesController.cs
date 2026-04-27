using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CadastroEstudanteApi.Data;
using CadastroEstudanteApi.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace CadastroEstudanteApi.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    public class EstudantesController : ControllerBase
    {
        private readonly AppDbContext _context;
        public EstudantesController(AppDbContext context)
        {
            _context = context;
        }

        [HttpGet]
        public IActionResult GetEstudantes()
        {
            var estudante = _context.Estudantes.ToList();
            return Ok(estudante);
        }
        [HttpGet("{id}")]
        public IActionResult GetEstudante(int id)
        {
            var estudante = _context.Estudantes.Find(id);
            if (estudante == null)
            {
                return NotFound();
            }
            return Ok(estudante);
        }
        [HttpPost]
        public IActionResult PostEstudante(Estudante estudante)
        {
            _context.Estudantes.Add(estudante);
            _context.SaveChanges();
            return CreatedAtAction(nameof(GetEstudante), new { id = estudante.Id }, estudante);
        }

        [HttpPut("{id}")]
        public IActionResult PutEstudante(int id, Estudante estudante)
        {
            if (id != estudante.Id) return BadRequest();
            _context.Entry(estudante).State = EntityState.Modified;

            try
            {
                _context.SaveChanges();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!_context.Estudantes.Any(e => e.Id == id)) return NotFound();
                throw;
            }
            return NoContent();
        }


        
    }
}