using Microsoft.EntityFrameworkCore;
using System.Globalization;
using System;

namespace TemplateApiCS.Database
{
    public partial class DatabaseContex : DbContext
    {
        public DatabaseContex(DbContextOptions<DatabaseContex> options)
            : base(options)
        {
        }

        partial void OnModelCreatingPartial(ModelBuilder modelBuilder);
    }
}
