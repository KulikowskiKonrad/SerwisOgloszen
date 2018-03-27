using SerwisOgloszen.BazaDanych;
using SerwisOgloszen.Repozytoria;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace SerwisOgloszen.Models
{
    public class ZapiszOgloszenieViewModel
    {
        public long? Id { get; set; }

        [Display(Name = "Treść")]
        [StringLength(1000, MinimumLength = 20, ErrorMessage = "Niepoprawna ilość znaków")]
        [Required(ErrorMessage = "Pole wymagane")]
        public string Tresc { get; set; }

        [Required(ErrorMessage = "Pole wymagane")]
        [StringLength(200, MinimumLength = 5, ErrorMessage = "Niepoprawna ilość znaków")]
        public string Temat { get; set; }

        [Display(Name = "Kategoria")]
        [Required(ErrorMessage = "Pole wymagane")]
        public long? KategoriaId { get; set; }

        public List<SelectListItem> ListaKategorii { get; set; }


        public ZapiszOgloszenieViewModel()
        {
            //kategoriwZbazy=pobierzzbazykategorie()
            //ListaEKatewgorii=?

            ListaKategorii = new List<SelectListItem>();
            KategoriaRepozytorium kategoriaRepozytorium = new KategoriaRepozytorium();
            List<Kategoria> pobraneKategorie = kategoriaRepozytorium.PobierzWszystkie();
            foreach (Kategoria kategoria in pobraneKategorie)
            {
                ListaKategorii.Add(new SelectListItem()
                {
                    Value = kategoria.Id.ToString(),
                    Text = kategoria.Nazwa
                });
            }
            //ListaKategorii.Add(new SelectListItem()
            //{
            //    Value = "1",
            //    Text = "Motoryzacja"
            //});

            //ListaKategorii.Add(new SelectListItem()
            //{
            //    Value = "2",
            //    Text = "Elektronika"
            //});
            //ListaKategorii.Add(new SelectListItem()
            //{
            //    Value = "3",
            //    Text = "Dom"
            //});
        }
    }
}