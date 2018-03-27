using SerwisOgloszen.BazaDanych;
using SerwisOgloszen.Helpers;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace SerwisOgloszen.Repozytoria
{
    public class UzytkownikRepozytorium
    {
        public long Edytuj(Uzytkownik uzytkownik)
        {
            try
            {
                long rezultat = 0;
                using (SerwisOgloszenEntities baza = new SerwisOgloszenEntities())
                {
                    baza.Entry(uzytkownik).State = uzytkownik.Id > 0 ? EntityState.Modified : EntityState.Added;
                    baza.SaveChanges();
                    rezultat = uzytkownik.Id;
                }
                return rezultat;
            }
            catch (Exception ex)
            {
                return 0;
            }

        }
        public bool Usun(long id)
        {
            bool rezultat = false;
            try
            {
                using (SerwisOgloszenEntities baza = new SerwisOgloszenEntities())
                {
                    Uzytkownik uzytkownik = null;
                    uzytkownik = baza.Uzytkownik.Where(x => x.Id == id).Single();
                    uzytkownik.CzyUsuniety = true;
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

        public List<Uzytkownik> PobierzWszystkich()
        {
            try
            {
                List<Uzytkownik> rezultat = null;

                using (SerwisOgloszenEntities baza = new SerwisOgloszenEntities())
                {
                    rezultat = baza.Uzytkownik.Where(x => x.CzyUsuniety == false).ToList();
                }

                return rezultat;
            }
            catch (Exception ex)
            {
                return null;
            }
        }

        public Uzytkownik Pobierz(string login, string haslo)
        {
            try
            {
                Uzytkownik rezultat = null;

                using (SerwisOgloszenEntities baza = new SerwisOgloszenEntities())
                {
                    rezultat = baza.Uzytkownik.Where(x => x.Login == login && x.CzyUsuniety == false).SingleOrDefault();
                }
                if(rezultat != null)
                {
                    string hasloZakodowane = MD5Helper.GenerujMD5(haslo + rezultat.Sol);
                    if(hasloZakodowane != rezultat.Haslo)
                    {
                        rezultat = null;
                    }
                }
                return rezultat;
            }
            catch (Exception ex)
            {
                return null;
            }


        }
        public Uzytkownik Pobierz(long id)
        {
            try
            {
                Uzytkownik rezultat = null;

                using (SerwisOgloszenEntities baza = new SerwisOgloszenEntities())
                {
                    rezultat = baza.Uzytkownik.Where(x => x.Id == id).Single();
                }
                return rezultat;
            }
            catch (Exception ex)
            {
                return null;
            }


        }
        public Uzytkownik Pobierz(string login)
        {
            try
            {
                Uzytkownik rezultat = null;

                using (SerwisOgloszenEntities baza = new SerwisOgloszenEntities())
                {
                    rezultat = baza.Uzytkownik.Where(x => x.Login == login && x.CzyUsuniety == false).SingleOrDefault();
                }
                return rezultat;
            }
            catch (Exception ex)
            {
                return null;
            }


        }


        public long? Zapisz(Uzytkownik uzytkownik)  // typ zalezy od tego co chcemy zwracac
        {
            try
            {
                long? rezultat = null;
                using (SerwisOgloszenEntities baza = new SerwisOgloszenEntities())
                {
                    //EntityState stan = EntityState.Added;
                    //if(uzytkownik.Id > 0)
                    //{
                    //    stan = EntityState.Modified;
                    //}
                    //baza.Entry(uzytkownik).State = stan;


                    baza.Entry(uzytkownik).State = uzytkownik.Id > 0 ? EntityState.Modified : EntityState.Added;  // te linie odpowiadaja liniom ponizej; 
                    //entry oznacza zawartosc bazy danych tu konkretnie oznacza dane uzytkownika 
                    // state oznacza stan w jakim sie znajduje  czyli czy chcemy modyfikowac czy dodawac 
                    // uzytkonik.Id > 0 ? jezli w tym wypadku jest wieksze od 0 to ustawia stan modyfikuj a w przeciwnym wypadku dodaj
                    baza.SaveChanges();
                    rezultat = uzytkownik.Id;


                    //if (uzytkownik.Id == 0)
                    //{
                    //    baza.Uzytkownik.Add(uzytkownik);
                    //    baza.SaveChanges();
                    //    rezultat = uzytkownik.Id;

                    //}
                    //else
                    //{
                    //    Uzytkownik temp = baza.Uzytkownik.Where(x => x.Id == uzytkownik.Id).Single();
                    //    temp.Haslo = uzytkownik.Haslo;
                    //    baza.SaveChanges();
                    //    rezultat = uzytkownik.Id;
                    //}
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