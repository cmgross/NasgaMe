using System.ComponentModel.DataAnnotations;

namespace NasgaMe.Models
{
    public class Search
    {
        [Required(ErrorMessage = "Name required.")]
        public string NameAndClass { get; set; }
    }
}