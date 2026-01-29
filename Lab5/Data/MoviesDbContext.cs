using Microsoft.EntityFrameworkCore;

public class MoviesDbContext : DbContext
{
    public DbSet<Movie> Movies { get; set; }
    public MoviesDbContext(DbContextOptions options) :
        base(options)
    {
    }
}