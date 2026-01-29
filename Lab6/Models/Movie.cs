using System.ComponentModel.DataAnnotations;

namespace Lab6.Models
{
    public class Movie
    {
        [Key]
        public int Id { get; set; }

        [Required(ErrorMessage = "Tytuł jest wymagany.")]
        [StringLength(50, ErrorMessage = "Długość tytułu nie może przekraczać 50 znaków.")]
        public string Title { get; set; } = string.Empty;

        [UIHint("LongText")]
        [Required(ErrorMessage = "Opis filmu jest wymagany.")]
        public string Description { get; set; } = string.Empty;

        [UIHint("Stars")]
        [Range(1, 5, ErrorMessage = "Ocena filmu musi być liczbą pomiędzy 1 a 5.")]
        public int Rating { get; set; }

        [Url(ErrorMessage = "Link do zwiastunu jest nieprawidłowy.")]
        public string? TrailerLink { get; set; }

        public Genre? Genre { get; set; } 

    }
}
