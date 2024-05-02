using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Logging;
using lafacustorev2.Data;
using Microsoft.EntityFrameworkCore;

namespace lafacustorev2.Controllers
{

    public class CatalogoController : Controller
    {
        private readonly ILogger<CatalogoController> _logger;
        private readonly ApplicationDbContext _context;

        public CatalogoController(ILogger<CatalogoController> logger,ApplicationDbContext context)
        {
            _logger = logger;
            _context = context;
        }

        public IActionResult Index()
        {
            var productos = from o in _context.DataProducto select o;
            productos = productos.Where(l => l.Status.Contains("A"));
            return View(productos.ToList());
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View("Error!");
        }
    }
}