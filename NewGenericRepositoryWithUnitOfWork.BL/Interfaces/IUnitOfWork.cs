using NewGenericRepositoryWithUnitOfWork.BL.Models;

namespace NewGenericRepositoryWithUnitOfWork.BL.Interfaces;
public interface IUnitOfWork : IDisposable
{
    IBaseRepository<Author> Authors { get; }
    // IBaseRepository<Book> Books { get; }
    IBookRepository Books { get; }

    Task<int> CompleteAsync();
}
