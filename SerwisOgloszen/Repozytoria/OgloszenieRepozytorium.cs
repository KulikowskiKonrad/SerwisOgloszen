using SerwisOgloszen.BazaDanych;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SerwisOgloszen.Repozytoria
{
    public class OgloszenieRepozytorium
    {
        //public long Edytuj(Ogloszenie ogloszenie)
        //{
        //    try
        //    {
        //        long rezultat = 0;
        //        using (SerwisOgloszenEntities baza = new SerwisOgloszenEntities())
        //        {
        //            baza.Entry(ogloszenie).State = ogloszenie.Id > 0 ? EntityState.Modified : EntityState.Added;
        //            baza.SaveChanges();
        //            rezultat = ogloszenie.Id;
        //        }
        //        return rezultat;
        //    }
        //    catch (Exception ex)
        //    {
        //        return 0;
        //    }

        //}
        public long? Zapisz(Ogloszenie ogloszenie)
        {
            try
            {

                long? rezultat = null;
                using (SerwisOgloszenEntities baza = new SerwisOgloszenEntities())
                {
                    baza.Entry(ogloszenie).State = ogloszenie.Id > 0 ? EntityState.Modified : EntityState.Added;
                    baza.SaveChanges();
                    rezultat = ogloszenie.Id;
                }
                return rezultat;
            }
            catch (Exception ex)
            {
                return null;
            }
        }



        public List<Ogloszenie> PobierzWszystkie()
        {
            try
            {
                List<Ogloszenie> rezultat = null;
                using (SerwisOgloszenEntities baza = new SerwisOgloszenEntities())
                {
                    rezultat = baza.Ogloszenie.Include(x=>x.Kategoria).Include(x=>x.Uzytkownik)
                        .OrderByDescending(x => x.DataDodania).ToList(); //include dodalo mi pola z tabeli kategori powiazane kluczem obcym
                }
                return rezultat;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Ogloszenie Pobierz(long id)
        {
            try
            {
                Ogloszenie rezultat = null;
                using(SerwisOgloszenEntities baza = new SerwisOgloszenEntities())
                {
                    rezultat=baza.Ogloszenie.Where(x => x.Id == id).Single();
                }
                return rezultat;
            }catch(Exception ex)
            {
                return null;
            }
        }

        public bool UsunOgloszenie ( long id)
        {
            try
            {
                bool rezultat = false;
                using(SerwisOgloszenEntities baza = new SerwisOgloszenEntities())
                {
                    Ogloszenie ogloszenieZBazy = null;
                    ogloszenieZBazy = baza.Ogloszenie.Where(x => x.Id == id).Single();
                    baza.Ogloszenie.Remove(ogloszenieZBazy);
                    baza.SaveChanges();
                    rezultat = true;
                }
                return rezultat;
            }catch(Exception ex)
            {
                return false;
            }
        }

    }
}