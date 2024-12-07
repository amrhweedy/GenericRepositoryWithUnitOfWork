using Microsoft.AspNetCore.Mvc;
using NewGenericRepositoryWithUnitOfWork.BL.Interfaces;

namespace NewGenericRepositoryWithUnitOfWork.API.Controllers;
[Route("api/[controller]")]
[ApiController]
public class AuthorController(IUnitOfWork unitOfWork) : ControllerBase // inject the IUnitOfWork instead of IBaseRepository<Author>
{

    [HttpGet("{id}")]
    public async Task<IActionResult> GetById(int id)
    {
        var author = await unitOfWork.Authors.GetByIdAsync(id);
        return Ok(author);
    }


    [HttpGet]
    public async Task<IActionResult> GetAllAsync()
    {
        return Ok(await unitOfWork.Authors.GetAllAsync());
    }

    [HttpGet("getByName")]
    public async Task<IActionResult> GetByName(string name)
    {
        return Ok(await unitOfWork.Authors.FindAsync(a => a.Name == name));
    }
}
