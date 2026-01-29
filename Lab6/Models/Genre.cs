using System.ComponentModel.DataAnnotations;
using Microsoft.EntityFrameworkCore;
public class Genre
{
    [Key]
    public int Id { get; set; }
    public string Name { get; set; } = string.Empty;


}