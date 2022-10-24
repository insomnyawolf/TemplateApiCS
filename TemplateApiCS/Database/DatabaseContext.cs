using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System;

namespace TemplateApiCS.Database
{
    public partial class DatabaseContext : DbContext
    {
        public DatabaseContext(DbContextOptions<DatabaseContext> options)
            : base(options)
        {
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
