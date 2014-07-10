using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web.Mvc;

namespace NasgaMe.Models
{
    public class Search
    {
        [Required(ErrorMessage = "Name required.")]
        public string NameAndClass { get; set; }
        public List<SelectListItem> Throws { get; set; }

        public Search()
        {
            var throws = new List<string> { string.Empty, "Braemar", "Open", "HWFD", "LWFD", "HH", "LH", "Caber", "Sheaf", "WFH" };
            Throws = throws.Select(t => new SelectListItem
            {
                Text = t,
                Value = t
            }).ToList();
        }
    }
}