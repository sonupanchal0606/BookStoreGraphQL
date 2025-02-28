using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace BookStoreGraphQL.Models
{
    public class Book
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public Guid Id { get; set; } = Guid.NewGuid();

        [Required]
        [MaxLength(200)]
        public string Title { get; set; }

        [MaxLength(100)]
        public string Genre { get; set; }

        [Required]
        public Guid AuthorId { get; set; }

        // Foreign Key and Navigation Property
        [ForeignKey("AuthorId")]
        public Author Author { get; set; }
    }
}
