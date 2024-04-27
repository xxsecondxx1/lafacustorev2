using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace lafacustorev2.Data;

public class ApplicationDbContext : IdentityDbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
        : base(options)
    {}
        public DbSet<lafacustorev2.Models.Contacto> DataContacto {get; set; }
        public DbSet<lafacustorev2.Models.Producto> DataProducto {get; set; }
    
}
