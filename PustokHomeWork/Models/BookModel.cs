using System.ComponentModel.DataAnnotations.Schema;
using System.Diagnostics.CodeAnalysis;

namespace PustokHomeWork.Models
{
    public class BookModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public double Price { get; set; }

        public int AuthorId { get; set; }

        public AuthorModel Author { get; set; }

        public ICollection<TagModel> Tags { get; set; }

        [NotMapped]
        [AllowNull]
        public List<int>? TagIds { get; set; }


    }
}
