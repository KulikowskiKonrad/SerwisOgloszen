using SerwisOgloszen.BazaDanych;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SerwisOgloszen.Repozytoria
{
    public class WiadomoscRepozytorium
    {

        public List<Wiadomosc> PobierzWszystkie(long uzytkownikId)
        {
            try
            {
                List<Wiadomosc> rezultat = null;
                using (SerwisOgloszenEntities baza = new SerwisOgloszenEntities())
                {
                    rezultat = baza.Wiadomosc.Include(x=>x.UzytkownikOdbierajacy).Include(x=>x.UzytkownikWysylajacy).Include(x=>x.Ogloszenie)
                        .Where(x => x.OdbierajacyUzytkownikId == uzytkownikId || x.WysylajacyUzytkownikId == uzytkownikId)
                        .OrderByDescending(x=>x.DataDodania).ToList();
                }
                return rezultat;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Wiadomosc Pobierz(long id)
        {
            try
            {
                Wiadomosc rezultat = null;
                using (SerwisOgloszenEntities baza = new SerwisOgloszenEntities())
                {
                    rezultat = baza.Wiadomosc.Where(x => x.Id == id).Single();
                }
                return rezultat;
            }
            catch (Exception ex)
            {
                return null;
            }
        }


        public List<Wiadomosc> Pobierz(long uzytkownik1Id, long uzytkownik2Id, long ogloszenieId)
        {
            try
            {
                List<Wiadomosc> rezultat = null;
                using (SerwisOgloszenEntities baza = new SerwisOgloszenEntities())
                {
                    rezultat = baza.Wiadomosc.Include(x => x.UzytkownikWysylajacy).Include(x => x.UzytkownikOdbierajacy)  //daje id do wyboru bo uzytkownik raz moze byc wysylajacy lub odbierajacy
                        .Where(x => x.OgloszenieId == ogloszenieId && (x.OdbierajacyUzytkownikId == uzytkownik1Id || x.OdbierajacyUzytkownikId == uzytkownik2Id)
                            && (x.WysylajacyUzytkownikId == uzytkownik1Id || x.WysylajacyUzytkownikId == uzytkownik2Id)).OrderByDescending(x=>x.DataDodania)
                        .ToList();
                }
                return rezultat;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
        public List<Wiadomosc> PobierzWszystkie()
        {
            try
            {
                List<Wiadomosc> rezultat = null;
                using (SerwisOgloszenEntities baza = new SerwisOgloszenEntities())
                {
                    rezultat = baza.Wiadomosc.ToList();
                }
                return rezultat;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public long? Zapisz(Wiadomosc wiadomosc)
        {
            try
            {
                long? rezultat = null;
                using (SerwisOgloszenEntities baza = new SerwisOgloszenEntities())
                {
                    baza.Entry(wiadomosc).State = wiadomosc.Id > 0 ? EntityState.Modified : EntityState.Added;
                    baza.SaveChanges();
                    rezultat = wiadomosc.Id;
                }
                return rezultat;
            }
            catch (Exception ex)
            {
                return null;
            }
        }
    }
}