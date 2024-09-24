using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using WebApplicationMongoDB.Models;

namespace WebApplicationMongoDB.Data
{
    public class WebApplicationMongoDBContext : DbContext
    {
        public WebApplicationMongoDBContext (DbContextOptions<WebApplicationMongoDBContext> options)
            : base(options)
        {
        }

        public DbSet<WebApplicationMongoDB.Models.Usuario> Usuario { get; set; } = default!;
    }
}
