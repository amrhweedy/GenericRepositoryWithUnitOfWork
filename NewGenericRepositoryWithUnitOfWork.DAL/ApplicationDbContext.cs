using Microsoft.EntityFrameworkCore;
using NewGenericRepositoryWithUnitOfWork.BL.Models;

namespace NewGenericRepositoryWithUnitOfWork.DAL;
public class ApplicationDbContext : DbContext
{
    public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
    {


    }

    public DbSet<Author> Authors { get; set; }
    public DbSet<Book> Books { get; set; }
}
