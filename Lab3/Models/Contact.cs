using System.ComponentModel;

namespace Lab3.Models
{
    public class Contact
    {
        [DisplayName("Identyfikator")]
        public int Id { get; set; } 
        [DisplayName("Imię")]
        public string Name { get; set; } = string.Empty;
        [DisplayName("Nazwisko")]
        public string Surname { get; set; } = string.Empty;
        [DisplayName("Adres e-mail")]
        public string Email { get; set; } = string.Empty;
        [DisplayName("Miasto")]
        public string City { get; set; } = string.Empty;
        [DisplayName("Numer telefonu")]
        public string PhoneNumber { get; set; } = string.Empty;
    }
}