using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;

namespace SerwisOgloszen.Models
{
    public class ZapiszKategorieViewModel
    {
        public long? Id { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        public string Nazwa { get; set; }

        public bool CzyUsunieta { get; set; }
    }
}