using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using MandiraApi.Data;
using MandiraApi.Models;

namespace MandiraApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImagenesProductoesController : ControllerBase
    {
        private readonly MandiraDbContext _context;

        public ImagenesProductoesController(MandiraDbContext context)
        {
            _context = context;
        }

        // GET: api/ImagenesProductoes
        [HttpGet]
        public async Task<ActionResult<IEnumerable<ImagenesProducto>>> GetImagenesProductos()
        {
            return await _context.ImagenesProductos.ToListAsync();
        }

        // GET: api/ImagenesProductoes/5
        [HttpGet("{id}")]
        public async Task<ActionResult<ImagenesProducto>> GetImagenesProducto(int id)
        {
            var imagenesProducto = await _context.ImagenesProductos.FindAsync(id);

            if (imagenesProducto == null)
            {
                return NotFound();
            }

            return imagenesProducto;
        }

        // PUT: api/ImagenesProductoes/5
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPut("{id}")]
        public async Task<IActionResult> PutImagenesProducto(int id, ImagenesProducto imagenesProducto)
        {
            if (id != imagenesProducto.ImagenProductoId)
            {
                return BadRequest();
            }

            _context.Entry(imagenesProducto).State = EntityState.Modified;

            try
            {
                await _context.SaveChangesAsync();
            }
            catch (DbUpdateConcurrencyException)
            {
                if (!ImagenesProductoExists(id))
                {
                    return NotFound();
                }
                else
                {
                    throw;
                }
            }

            return NoContent();
        }

        // POST: api/ImagenesProductoes
        // To protect from overposting attacks, see https://go.microsoft.com/fwlink/?linkid=2123754
        [HttpPost]
        public async Task<ActionResult<ImagenesProducto>> PostImagenesProducto(ImagenesProducto imagenesProducto)
        {
            _context.ImagenesProductos.Add(imagenesProducto);
            await _context.SaveChangesAsync();

            return CreatedAtAction("GetImagenesProducto", new { id = imagenesProducto.ImagenProductoId }, imagenesProducto);
        }

        // DELETE: api/ImagenesProductoes/5
        [HttpDelete("{id}")]
        public async Task<IActionResult> DeleteImagenesProducto(int id)
        {
            var imagenesProducto = await _context.ImagenesProductos.FindAsync(id);
            if (imagenesProducto == null)
            {
                return NotFound();
            }

            _context.ImagenesProductos.Remove(imagenesProducto);
            await _context.SaveChangesAsync();

            return NoContent();
        }

        private bool ImagenesProductoExists(int id)
        {
            return _context.ImagenesProductos.Any(e => e.ImagenProductoId == id);
        }
    }
}
