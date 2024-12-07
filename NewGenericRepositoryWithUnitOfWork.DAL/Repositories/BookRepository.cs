using Microsoft.EntityFrameworkCore;
using NewGenericRepositoryWithUnitOfWork.BL.Interfaces;
using NewGenericRepositoryWithUnitOfWork.BL.Models;

namespace NewGenericRepositoryWithUnitOfWork.DAL.Repositories;
public class BookRepository : BaseRepository<Book>, IBookRepository
{
    private readonly ApplicationDbContext _context;
    public BookRepository(ApplicationDbContext context) : base(context)
    {
        _context = context;
    }
    public async Task<IEnumerable<Book>> SpecialFindAllAsync()
    {
        return await _context.Books.Include(b => b.Author).ToListAsync();
    }
}
