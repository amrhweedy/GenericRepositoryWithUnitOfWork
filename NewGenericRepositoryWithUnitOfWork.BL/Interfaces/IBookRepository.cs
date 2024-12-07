using NewGenericRepositoryWithUnitOfWork.BL.Models;

namespace NewGenericRepositoryWithUnitOfWork.BL.Interfaces;
public interface IBookRepository : IBaseRepository<Book>
{
    Task<IEnumerable<Book>> SpecialFindAllAsync();

}
