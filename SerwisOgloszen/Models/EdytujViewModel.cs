using SerwisOgloszen.Enums;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SerwisOgloszen.Models
{
    public class EdytujViewModel
    {
        public long Id { get; set; }

        [Display(Name = "Hasło")]
        [StringLength(20, MinimumLength = 4, ErrorMessage = "Niepoprawna ilość znaków")]
        public string Haslo { get; set; }

        [System.ComponentModel.DataAnnotations.Compare("Haslo", ErrorMessage = "Niezgodne hasła")]
        [Display(Name = "Powtórz hasło")]
        public string PowtorzoneHaslo { get; set; }

        public RolaUzytkownika Rola { get; set; }

        public List<SelectListItem> ListaRol { get; set; }

        public string Login { get; set; }

        public EdytujViewModel()
        {
            ListaRol = new List<SelectListItem>();
            ListaRol.Add(new SelectListItem()
            {
                Text=RolaUzytkownika.Administrator.ToString(), /* wchodze w enuma pobieram z niego wartos i robie to string bo tego wymaga*/
                Value=((byte)RolaUzytkownika.Administrator).ToString()  /*tu musialem dac typ byte bo w bazie jest tinyint a pozniej dac to string*/
            });
            ListaRol.Add(new SelectListItem()
            {
                Text = RolaUzytkownika.Uzytkownik.ToString(), /* wchodze w enuma pobieram z niego wartos i robie to string bo tego wymaga*/
                Value = ((byte)RolaUzytkownika.Uzytkownik).ToString()  /*tu musialem dac typ byte bo w bazie jest tinyint a pozniej dac to string*/
            });
        }
    }
}