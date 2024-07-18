using System.ComponentModel.DataAnnotations;

namespace BookStore.Models.Domain
{
    public class Author
    {
        public int Id { get; set; }
        [Required]
        public String AuthorName { get; set; }
    }
}
