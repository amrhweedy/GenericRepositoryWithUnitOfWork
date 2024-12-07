using Microsoft.AspNetCore.Mvc;
using NewGenericRepositoryWithUnitOfWork.BL.Dtos;
using NewGenericRepositoryWithUnitOfWork.BL.Interfaces;
using NewGenericRepositoryWithUnitOfWork.BL.Models;

namespace NewGenericRepositoryWithUnitOfWork.API.Controllers;
[Route("api/[controller]")]
[ApiController]
// inject the IUnitOfWork instead of IBaseRepository<Book>
public class BookController(IUnitOfWork unitOfWork) : ControllerBase
{
    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var book = await unitOfWork.Books.GetByIdAsync(id);
        return Ok(book);
    }


    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await unitOfWork.Books.GetAllAsync());
    }

    [HttpGet("GetByTitle")]
    public async Task<IActionResult> GetByTitle(string title)
    {
        return Ok(await unitOfWork.Books.FindAsync(a => a.Title == title, ["Author"]));
    }

    [HttpGet("GetAllWithAuthors")]
    public async Task<IActionResult> GetAllWithAuthors(string title)
    {
        return Ok(await unitOfWork.Books.FindAllAsync(a => a.Title.Contains(title), ["Author"]));
    }


    [HttpGet("GetAllWithAuthorsPaginated")]
    public async Task<IActionResult> GetAllWithAuthorsPaginated(string title, int skip, int take)
    {
        return Ok(await unitOfWork.Books.FindAllAsync(a => a.Title.Contains(title), skip, take));
    }


    [HttpGet("GetAllWithAuthorsPaginatedWithOrderById")]
    public async Task<IActionResult> GetAllWithAuthorsPaginatedWithOrder(string title, int? skip, int? take, string? OrderByDirection)
    {
        return Ok(await unitOfWork.Books.FindAllAsync(b => b.Title.Contains(title), skip, take, ["Author"], b => b.Id, OrderByDirection));
    }


    [HttpGet("GetAllWithAuthorsPaginatedWithGeneralOrder")]
    public async Task<IActionResult> GetAllWithAuthorsPaginatedWithGeneralOrder(string title, int? skip, int? take, string? orderBy, string? OrderByDirection)
    {
        return Ok(await unitOfWork.Books.FindAllAsync(b => b.Title.Contains(title), skip, take, ["Author"], orderBy, OrderByDirection));
    }


    [HttpPost("AddOne")]

    public async Task<ActionResult<Book>> AddOneAsync([FromBody] BookDto bookDto)
    {

        var book = new Book()
        {
            Title = bookDto.Title,
            AuthorId = bookDto.AuthorId,
        };

        var addedBook = await unitOfWork.Books.AddAsync(book);
        await unitOfWork.CompleteAsync();  // use unit of work
        return addedBook;

    }

    [HttpPost("AddRange")]

    public async Task<ActionResult<IEnumerable<Book>>> AddRangeAsync([FromBody] IEnumerable<BookDto> booksDto)
    {

        var books = booksDto.Select(b => new Book
        {
            Title = b.Title,
            AuthorId = b.AuthorId,
        }).ToList();

        var entities = await unitOfWork.Books.AddRangeAsync(books);
        await unitOfWork.CompleteAsync();  // use unit of work

        return Ok(entities);

    }


    [HttpPut("{id}")]

    public async Task<ActionResult<Book>> UpdateOneAsync([FromBody] BookDto bookDto, int id)
    {
        var book = await unitOfWork.Books.GetByIdAsync(id);
        book.Title = bookDto.Title;
        book.AuthorId = bookDto.AuthorId;
        await unitOfWork.Books.UpdateAsync(book);
        return book;
    }


    [HttpDelete("{id}")]
    public async Task<IActionResult> DeleteOneAsync(int id)
    {
        var book = await unitOfWork.Books.GetByIdAsync(id);
        await unitOfWork.Books.DeleteAsync(book);
        return Ok();
    }


    [HttpGet("GetSpecialFindAllAsync")]
    public async Task<IActionResult> GetSpecialFindAllAsync()
    {
        return Ok(await unitOfWork.Books.SpecialFindAllAsync());
    }

}




