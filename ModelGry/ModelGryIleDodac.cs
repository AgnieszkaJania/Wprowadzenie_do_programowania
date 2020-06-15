using System;
using System.Collections.Generic;
using System.Threading;

namespace Model_Gry
{
    /// <summary>
    /// Klasa ModelGry odpowiada za logikę gry - ile dodać?
    /// </summary>
    /// <remarks>
    /// Gra może znajdować się w dwóch stanach: w takcie i zakończona.
    /// </remarks>
    public class ModelGryIleDodac
    {
        readonly Random Suma = new Random();
        //Random Odejmowana = new Random();//zmiana
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
            LiczbaOdejmowana = Suma.Next(0, Liczba);
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
        /// <summary>
        /// Ilosc punktow w grze
        /// </summary>
        public int punkty { get; private set; }
        public int runda = 0;
        public ModelGryIleDodac()
        {
            punkty = 0;
        }
        public void CzasNaOdp(Action postep)
        {
            stan = StanGryIle.zakończona;
            for (int i = 0; i < 20; i++)
            {
                postep();
                Thread.Sleep(czasMs);
            }
            if (czasMs > 150)
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
            runda++;
        }
        public StanGryIle stan = StanGryIle.zakończona;
        public bool Weryfikacja(int odpowiedzUzytkownika)
        {

            if (odpowiedzUzytkownika == LiczbaOdejmowana)
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

                return false;
            }
        }
        public enum StanGryIle
        {
            wTrakcie,
            zakończona
        }

    }
}
