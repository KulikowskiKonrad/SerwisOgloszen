using SerwisOgloszen.BazaDanych;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SerwisOgloszen.Repozytoria
{
    public class KategoriaRepozytorium
    {

        public List<Kategoria> PobierzWszystkie()
        {
            try
            {
                List<Kategoria> reultat = null;
                using (SerwisOgloszenEntities baza = new SerwisOgloszenEntities())
                {
                    reultat = baza.Kategoria.OrderBy(x => x.Nazwa).Where(x => x.CzyUsunieta == false).ToList();
                }
                return reultat;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Kategoria Pobierz(long id)
        {
            try
            {
                Kategoria reultat = null;
                using (SerwisOgloszenEntities baza = new SerwisOgloszenEntities())
                {
                    reultat = baza.Kategoria.Where(x => x.Id == id).Single();
                    return reultat;
                }
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public long? Zapisz(Kategoria kategoria)
        {
            try
            {

                long? rezultat = null;
                using (SerwisOgloszenEntities baza = new SerwisOgloszenEntities())
                {
                    baza.Entry(kategoria).State = kategoria.Id > 0 ? EntityState.Modified : EntityState.Added;
                    baza.SaveChanges();
                    rezultat = kategoria.Id;
                }
                return rezultat;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public bool UsunKategorie(long id)
        {
            try
            {
                bool rezultat = false;
                using (SerwisOgloszenEntities baza = new SerwisOgloszenEntities())
                {
                    Kategoria kategoria = null;
                    kategoria = baza.Kategoria.Where(x => x.Id == id).Single();
                    kategoria.CzyUsunieta = true;
                    baza.SaveChanges();
                    rezultat = true;
                }
                return rezultat;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}