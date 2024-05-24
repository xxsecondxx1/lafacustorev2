using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using lafacustorev2.Data;
using lafacustorev2.Models;
using Microsoft.EntityFrameworkCore;
using lafacustorev2.Service;

namespace lafacustorev2.Controllers.Rest
{
    [ApiController]
    [Route("api/producto")]
    public class ProductoApiController : ControllerBase
    {
        private readonly ApplicationDbContext _context;
        private readonly ProductoService _productoService;
        public ProductoApiController(ApplicationDbContext context,ProductoService productoService)
        {
            _context = context;
            _productoService = productoService;
        }

        [HttpGet]
        [ProducesResponseType(StatusCodes.Status200OK)]
        [ProducesResponseType(StatusCodes.Status404NotFound)]
        
        public async Task<ActionResult<List<Producto>>> List()
        {
            var productos = await _productoService.GetAll();
            if(productos == null)
                return NotFound();
            return Ok(productos);
        }

    }
}