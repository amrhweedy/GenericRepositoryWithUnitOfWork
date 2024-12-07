using System.ComponentModel.DataAnnotations;

namespace NewGenericRepositoryWithUnitOfWork.BL.Models;
public class Author
{
    public int Id { get; set; }
    [Required, MaxLength(150)]
    public string? Name { get; set; }
}
