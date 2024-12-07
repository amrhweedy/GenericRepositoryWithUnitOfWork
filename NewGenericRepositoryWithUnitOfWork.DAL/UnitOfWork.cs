using NewGenericRepositoryWithUnitOfWork.BL.Interfaces;
using NewGenericRepositoryWithUnitOfWork.BL.Models;
using NewGenericRepositoryWithUnitOfWork.DAL.Repositories;

namespace NewGenericRepositoryWithUnitOfWork.DAL;
public class UnitOfWork : IUnitOfWork
{
    private readonly ApplicationDbContext _context;
    public IBaseRepository<Author> Authors { get; private set; }

    public IBookRepository Books { get; private set; }

    public UnitOfWork(ApplicationDbContext context)
    {
        // initialize repositories to avoid the null reference exception
        _context = context;
        Authors = new BaseRepository<Author>(_context);
        Books = new BookRepository(_context);

    }
    public async Task<int> CompleteAsync()
    {
        return await _context.SaveChangesAsync();
    }

    public void Dispose()
    {
        _context.Dispose();
    }
}
