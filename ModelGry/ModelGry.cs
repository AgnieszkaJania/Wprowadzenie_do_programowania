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
/// W ostatnich trzech rundach gry losowany ciąg liczb składa się z 9 liczb.
/// Druga gra - ile dodać ? Komputer losuje liczbę z przedziału od 0 do 100, następnie losuje kolejną z przedziału od 0 do pierwszej wylosowanej liczby.
/// Następnie druga wylosowana liczba jest odejmowana od pierwszej wylosowanej liczby, a wynik zapisywany jest w zmiennej różnica.
/// Następnie pierwsza wylosowana liczba oraz różnica są wyświetlane użytkownikowi na konsoli przez pewien czas, po któym konsola jest czyszczona.
/// Użytkownik musi odgadnąć jaką liczbę należy dodać do różnicy, aby otrzymać pierwszą wylosowaną liczbę.
/// Co trzy rundy czas na odgadnięcie liczby, którą należy dodać zmniejsza się, do momentu osiągnięcia czasu 150ms.
/// Co trzy rundy ilość punktów przyznawanych użytkownikowi za każdą poprawną odpowiedż zwiększa się.
/// Gra trwa do momentu podania przez użytkownika złej odpowiedzi.
/// Po zakończeniu gry użytkownikowi wyświetlana jest ilość zdobytych punktów oraz ilość wszystkich rund, które przeszedł.
/// Podanie nieprawidłowej odpowiedzi tj. takiej, która nie jest liczbą powoduje zakończenie gry bez podania informacji o jej przebiegu.
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
        /// Początkowa ilość liczb w losowanym ciągu.
        /// </summary>
        public int IleCyfr { get; private set; }
        /// <summary>
        /// Buffor opóźniający wydłużenie losowanego ciągu liczb o trzy rundy.
        /// </summary>
        int buffor = 2;
        /// <summary>
        /// Buffor opóźniający skrócenie czasu wyświetlania wylosowanego ciągu liczb o trzy rundy.
        /// </summary>
        int bufforCzasu = 2;
        /// <summary>
        /// Stan, w którym może znaleźć się gra, przyjmuje jedną z trzech stałych wartości określonych w enum StanGry.
        /// </summary>
        public StanGry Stangry { get; private set; }
        
        readonly Random random = new Random();
        List<int> wylosowane;

        /// <summary>
        /// Zmienna określająca początkowy czas opóźnienia w Ms.
        /// </summary>
        int CzasMs;
        /// <summary>
        /// Zmienna określająca dolną granicę przedziału losowania liczb.
        /// </summary>
        int min;
        /// <summary>
        /// Zmienna określająca górną granicę przedziału losowania liczb.
        /// </summary>
        int max;

        /// <summary>
        /// Konstruktor klasy, tworzący przedział do losowania liczb i zgłaszający wyjątki 
        /// w przypadku podania niepoprawnych danych. 
        /// Konstruktor inicjuje zmienne CzasMs, IleCyfr oraz Stangry.
        /// Zmiennej Stangry zostaje początkowo przypisana warość przegrana.
        /// </summary>
        /// <param name="min">Minimalna wartość dla przedziału losowania ciągu liczb</param>
        /// <param name="max">Maksymalna wartość dla przedziału losowania ciągu liczb</param>
        public ModelGry(int min, int max)
        {
            CzasMs = 200;
            IleCyfr = 4;
            Stangry = StanGry.przegrana;
            if (min < 0 || max < 0)
                throw new ArgumentException("Liczby nie mogą być ujemne!");
            if (min > max)
                throw new ArgumentException("Zły przedział do losowania: pierwsza liczba musi być mniejsza od drugiej.");
            if (min == max)
                throw new ArgumentException("Liczby nie mogą być takie same!");

            this.min = min;
            this.max = max;

        }
        /// <summary>
        /// Funkcja losująca liczby z zadanego przedziału i zapisująca je w liście wylosowane.
        /// </summary>
        public void Losuj()
        {
            wylosowane = new List<int>();
            for (int i = 0; i < IleCyfr; i++)
            {

                wylosowane.Add(random.Next(min, max + 1));

            }

        }

        /// <summary>
        /// Funkcja zwracająca listę tylko do odczytu Wylosowane, która przyjmuje wartości z listy wylosowane.
        /// </summary>
        /// <returns>Lista tylko do odczytu Wylosowane</returns>
        public IReadOnlyList<int> Wylosowane()
        {
            return wylosowane.AsReadOnly();
        }


        /// <summary>
        /// Funkcja sprawdzająca czy odpowiedż użytkownika zgadza się z wylosowaną listą liczb. 
        /// Funkcja sprawdza, czy użytkownik podał taką samą ilość liczb oraz czy  podał takie same 
        /// liczby i w takiej samej kolejności, jak liczby wylosowane. 
        /// Funkcja za każdym razem zmniejsza buffor o 1.
        /// Gdy buffor osiągnie wartość 0 to jego wartość wraca do
        /// poziomu 2, natomiast ilość liczb w losowanym ciągu zwiększa się o kolejną liczbę. 
        /// Gdy ilość liczb w losowanym ciągu osiągnie wartość 10 to stan gry zmienia się na wygrana.
        /// Przy podaniu prawidłowej odpowiedzi funkcja przypisuje zmiennej stangry wartość wTrakcie.
        /// </summary>
        /// <param name="OdpowiedzUzytkownika">Odpowiedź użytkownika, będąca tablicą liczb.</param>
        /// <returns>Funkcja zwraca wartość true w przypadku udzielenia prawidłowej odpowiedzi lub wartość
        /// false, gdy podana przez użytkownika odpowiedź jest błędna.
        /// </returns>

        public bool Sprawdzenie(int[] OdpowiedzUzytkownika)
        {
            if (wylosowane.Count != OdpowiedzUzytkownika.Length)
            {

                return false;

            }
            
            for (int i = 0; i < wylosowane.Count; i++)
            {
                if (wylosowane[i] != OdpowiedzUzytkownika[i])
                {
                    
                    return false;
                }

            }

            if (buffor == 0)
            {
               IleCyfr++;
                buffor = 2;
            }
            else
            {
                buffor--;
            }
            Stangry = StanGry.wTrakcie;

            if (IleCyfr == 10)
            {
                Stangry = StanGry.wygrana;
            }
            return true;
        }
        /// <summary>
        /// Funkcja, która daje użytkownikowi czas na zapamiętanie ciągu wylosowanych liczb.
        /// Za każdym razem, gdy funkcja Przerwa zostaje wywołana, zmniejsza ona wartość zmiennej bufforCzasu o 1.
        /// Gdy bufforCzasu osiągnie wartość 0 jego wartość zmieniana jest na 2, natomiast wartość zmiennej
        /// CzasMs jest zmniejszana o 10.
        /// Za każdym razem, gdy funkcja Przerwa zostaje wywołana 
        /// wartości zmiennej Stangry zostaje przypisana wartość przegrana.
        /// </summary>
        /// <param name="postep">Argumentem funkcji Przerwa jest funkcja postep, która jest wywoływana
        /// w pętli z opóźnieniem równym wartości zmiennej CzasMs. 
        /// </param>

        public void Przerwa(Action postep)
        {
            Stangry = StanGry.przegrana;
            for (int i = 0; i < 20; i++)
            {
                postep();
                Thread.Sleep(CzasMs);
            }
            if (bufforCzasu == 0)
            {
                bufforCzasu = 2;
                CzasMs -= 10;

            }
            else
            {
                bufforCzasu--;
            }
        }


        /// <summary>
        /// Enum składające się z trzech stałych - stanów, w których może znajdować się gra.
        /// </summary>
        public enum StanGry
        {
            wTrakcie,
            przegrana,
            wygrana
        }




    }

}
