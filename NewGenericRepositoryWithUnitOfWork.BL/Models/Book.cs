using System.ComponentModel.DataAnnotations;

namespace NewGenericRepositoryWithUnitOfWork.BL.Models;
public class Book
{
    public int Id { get; set; }
    [Required, MaxLength(250)]
    public string? Title { get; set; }

    public int AuthorId { get; set; }
    public Author? Author { get; set; }

}

