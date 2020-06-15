using System;
using System.Collections.Generic;
using System.Threading;

/// <summary>
/// Aplikacja składa się z dwórch różnych gier. 
/// W głównym menu dokonujemy wyboru, w którą grę chcemy zagrać albo wychodzimy z aplikacji.
/// Pierwsza gra - zapamiętaj liczby. Komputer losuje ciąg liczb i wyświtla je przez określony czas. 
/// Następnie konsola się czyści i użytkownik musi przepisać te liczby w takiej samej kolejności, w jakiej zostały wyświetlone na konsoli.
/// Co trzy rundy czas na zapamiętanie ciągu liczb zmniejsza się, a ciąg liczb wydłuża się o jedną liczbę. 
/// Gracz wygywa przepisując poprawnie ciąg liczb w każdej rundzie.
/// Druga gra - ile dodać ? Komputer losuje liczbę z przedziału od 0 do 100, następnie losuje kolejną z przedziału od 0 do pierwszej wylosowanej liczby.
/// Następnie druga wylosowana liczba jest odejmowana od pierwszej wylosowanej liczby, a wynik zapisywany jest w zmiennej różnica.
/// Następnie pierwsza wylosowana liczba oraz różnica są wyświetlane użytkownikowi na konsoli przez pewien czas, po któym konsola jest czyszczona.
/// Użytkownik musi odgadnąć jaką liczbę należy dodać do różnicy, aby otrzymać pierwszą wylosowaną liczbę.
/// Co trzy rundy czas na odgadnięcie liczby, którą należy dodać zmniejsza się do momentu osiągnięcia czasu 150ms.
/// Co trzy rundy ilość punktów przyznawanych użytkownikowi za każdą poprawną odpowiedż zwiększa się.
/// Gra trwa do momentu podania przez użytkownika złej odpowiedzi.
/// Po zakończeniu gry użytkownikowi wyświetlana jest ilość zdobytych punktów oraz ilość wszystkich rund, które przeszedł.
/// Podanie nieprawidzłowej odpowiedzi tj. takiej, która nie jest liczbą powoduje zakończenie gry bez podania informacji o jej przebiegu.
/// </summary>
namespace Model_Gry
{

    ///<summary>
    ///Klasa ModelGry odpowiada za logikę gry - zapamiętaj liczby.
    ///</summary>
    ///<remarks>
    ///Gra może znajdować się w trzech stanach: w takcie, wygrana lub przegrana.
    /// </remarks>
    public class ModelGry
    {
        /// <summary>
        /// Ilość liczb w losowanym ciągu
        /// </summary>
        public int ileCyfr { get; private set; }
        //public int[] cyfry;
        //buffor opóźniający wydłużenie losowanego ciągu o trzy rundy
        int buffor = 2;
        //buffor opóźniający skrócenie czasu wyświetlania wylosowanego ciągu o trzy rundy
        int bufforCzasu = 2;
        //zadeklarowanie i inicjacja zmiennej stangry
        public StanGry stangry = StanGry.przegrana;
        //zmienna przyjmująca losowe wartości
        readonly Random random = new Random();
        //zadeklarowanie listy wylosowane
        List<int> wylosowane;
        //zmienna określająca czas opóźnienia
        public int czasMs = 200;
        //zadeklarowanie zmiennych min i max dla przedziału losowania ciągu liczb
        int min;
        int max;

        //Konstruktor klasy, tworzący przedział do losowania liczb i zgłaszający wyjątki w przypadku podania niepoprawnych danych.
        public ModelGry(int min, int max)
        {

            ileCyfr = 4;
            if (min < 0 || max < 0)
                throw new ArgumentException("Liczby nie mogą być ujemne!");
            if (min > max)
                throw new ArgumentException("Zły przedział do losowania: pierwsza liczba musi być mniejsza od drugiej.");
            if (min == max)
                throw new ArgumentException("Liczby nie mogą być takie same!");

            this.min = min;
            this.max = max;

        }
        //funkcja losująca liczby z zadanego przedziału i zapisująca je w liście wylosowane
        public void Losuj()
        {
            wylosowane = new List<int>();
            for (int i = 0; i < ileCyfr; i++)
            {

                wylosowane.Add(random.Next(min, max + 1));

            }

        }

        //funkcja zwracająca listę tylko do odczytu Wylosowane, która przejmuje wartości listy wylosowane
        public IReadOnlyList<int> Wylosowane()
        {
            return wylosowane.AsReadOnly();
        }


        //funkcja sprawdzająca czy odpowiedż użytkownika zgadza się z wylosowaną listą liczb. 

        public bool Sprawdzenie(int[] OdpowiedzUzytkownika)
        {
            //Czy użytkownik podał taką samą ilość liczb.
            if (wylosowane.Count != OdpowiedzUzytkownika.Length)
            {

                return false;

            }
            // Czy użytkownik podał takie same liczby i w takiej samej kolejności.
            for (int i = 0; i < wylosowane.Count; i++)
            {
                if (wylosowane[i] != OdpowiedzUzytkownika[i])
                {
                    //stangry = StanGry.przegrana;
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

            if (ileCyfr == 6)
            {
                stangry = StanGry.wygrana;
            }
            return true;
        }


        public void Przerwa(Action postep)
        {
            stangry = StanGry.przegrana;
            for (int i = 0; i < 20; i++)
            {
                postep();
                Thread.Sleep(czasMs);
            }
            if (bufforCzasu == 0)
            {
                bufforCzasu = 2;
                czasMs -= 10;

            }
            else
            {
                bufforCzasu--;
            }
        }


        // enum składające się z trzech stałych - stanów, w których może znajdować się gra
        public enum StanGry
        {
            wTrakcie,
            przegrana,
            wygrana



        }




    }

}
