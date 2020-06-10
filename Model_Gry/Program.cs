using System;
using System.Collections.Generic;
using System.IO;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace Model_Gry
{   
    public class ModelGry
    {
        public int ileCyfr = 4;
        public int[] cyfry;
        
        Random random = new Random();
        List<int> wylosowane;
        
        int min;
        int max;

        
        public ModelGry(int min, int max)
        {
            if (min < 0 || max < 0)
                throw new ArgumentException("Liczby nie mogą być ujemne!");
            if (min > max)
                throw new ArgumentException("Zły przedział do losowania: pierwsza liczba musi być mniejsza od drugiej.");

            this.min = min;
            this.max = max;
  
        }
        public void Losuj()
        {
            wylosowane = new List<int>();
            for (int i = 0; i < ileCyfr; i++)
            {

                wylosowane.Add(random.Next(min, max + 1));

            }

        }
        
        
        public IReadOnlyList<int> Wylosowane()
        {
            return wylosowane.AsReadOnly();
        }
        
        int buffor = 2;
        int bufforCzasu = 2;
        /// <summary>
        /// 
        /// </summary>
        /// <param name="OdpowiedzUzytkownika"></param>
        /// <returns></returns>
        public StanGry stangry= StanGry.przegrana;
        
        public bool Sprawdzenie(int[] OdpowiedzUzytkownika)
        {
            if(wylosowane.Count != OdpowiedzUzytkownika.Length)
            {
                stangry = StanGry.przegrana;
                return false;
                
            }

            for(int i = 0; i < wylosowane.Count; i++)
            {
                if (wylosowane[i] != OdpowiedzUzytkownika[i])
                {
                    stangry = StanGry.przegrana;
                    return false;
                }
                    
            }
            if (buffor == 0)
            {
                ileCyfr++;
                buffor = 2;
            }
            else
            {
                buffor--;
            }
            stangry = StanGry.wTrakcie;
            
            if(ileCyfr == 6)
            {
                stangry = StanGry.wygrana;
            }
            return true;
        }
        
        public int czasMs = 200;
        public void Przerwa(Action postep)
        {
            for (int i = 0; i < 20; i++)
            {
                postep();
                Thread.Sleep(czasMs);
            }
            if(bufforCzasu == 0)
            {
                bufforCzasu = 2;
                czasMs -= 10;

            }
            else
            {
                bufforCzasu--;
            }
        }
        


        public enum StanGry
        {
            wTrakcie,
            przegrana,
            wygrana



        }

        


    }
    public class ModelGryIleDodac
    {
        readonly Random Suma = new Random();
        Random Odejmowana = new Random();
        int Liczba;
        public int LiczbaOdejmowana;
        int roznica;
        List<int> wylosowaneLiczby;

        //public ModelGryIleDodac()
        //{
            



        //}
        public void LosujLiczbe()
        {
            wylosowaneLiczby = new List<int>();
            Liczba = Suma.Next(0, 101);
            LiczbaOdejmowana = Odejmowana.Next(0, Liczba);
            roznica = Liczba - LiczbaOdejmowana;
            wylosowaneLiczby.Add(Liczba);
            wylosowaneLiczby.Add(roznica);
        }
        
        public IReadOnlyList<int> WylosowaneLiczby()
        {
            return wylosowaneLiczby.AsReadOnly();
        }
        public int czasMs = 400;
        public int buffor = 2;
        public int punkty = 0;
        public void CzasNaOdp(Action postep)
        {
            for (int i = 0; i < 20; i++)
            {
                postep();
                Thread.Sleep(czasMs);
            }
            if(czasMs > 150)
            {
                if (buffor == 0)
                {
                    buffor = 2;
                    czasMs -= 50;
                }
                else
                {
                    buffor--;
                }
            }
        }
        public StanGryIle stan = StanGryIle.zakończona;
        public bool Weryfikacja(int odpowiedzUzytkownika)
        {
            if(odpowiedzUzytkownika == LiczbaOdejmowana)
            {

                switch (czasMs)
                {
                    case 400:
                        punkty++;
                        break;
                    case 350:
                        punkty += 2;
                        break;
                    case 300:
                        punkty += 3;
                        break;
                    case 250:
                        punkty += 4;
                        break;
                    case 200:
                        punkty += 5;
                        break;
                    case 150:
                        punkty += 10;
                        break;



                }
                stan = StanGryIle.wTrakcie;
                return true;
            }
            else
            {
                
                stan = StanGryIle.zakończona;
                return false;
            }
        }
        public enum StanGryIle
        {
            wTrakcie,
            zakończona
        }
        
    }
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Hello World!");
        }
    }
}
