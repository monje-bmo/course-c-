using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using miApi.models;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design.Internal;

namespace miApi.Data
{
    public class ApplicationDBContext : DbContext
    {
        public ApplicationDBContext(DbContextOptions<ApplicationDBContext> options)
        : base(options)
        { }

        public DbSet<Stock> Stock { get; set; }
        public DbSet<Comment> Comments { get; set; }


    }
}