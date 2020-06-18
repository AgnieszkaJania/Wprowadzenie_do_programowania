using System;
using System.Collections.Generic;
using System.Threading;

namespace Model_Gry
{
    /// <summary>
    /// Klasa ModelGryIleDodac odpowiada za logikę gry - ile dodać?
    /// </summary>
    /// <remarks>
    /// Gra może znajdować się w dwóch stanach: w takcie i zakończona.
    /// </remarks>
    public class ModelGryIleDodac
    {

        /// <summary>
        /// Wylosowana liczba z przedziału od 0 do 100.
        /// </summary>
        int Liczba;
        /// <summary>
        /// Wylosowana liczba z przedziału od 0 do wartości zmiennej Liczba.
        /// </summary>
        public int LiczbaOdejmowana { get; private set; }
        /// <summary>
        /// Różnica pomiędzy wartością zmiennej Liczba, a wartością zmiennej LiczbaOdejmowana.
        /// </summary>
        int Roznica;
        
        List<int> wylosowaneLiczby;
        readonly Random Suma = new Random();

        /// <summary>
        /// Zmienna określająca początkowy czas opóźnienia w Ms.
        /// </summary>
        int CzasMs;
        /// <summary>
        /// Buffor opóźniający skrócenie czasu wyświetlania liczb o trzy rundy.
        /// </summary>
        int buffor = 2;
        /// <summary>
        /// Ilosc punktow w grze, które zdobył gracz.
        /// </summary>
        public int Punkty { get; private set; }
        /// <summary>
        /// Runda gry.
        /// </summary>
        public int Runda { get; private set; }
        /// <summary>
        /// Stan, w którym może znaleźć się gra, przyjmuje jedną z dwóch 
        /// stałych wartości, określonych w enum StanGryIle.
        /// </summary>
        public StanGryIle Stan { get; private set; }
        /// <summary>
        /// Konstruktor klasy, inicjujący zmienne Punkty, Runda, CzasMs i Stan.
        /// Zmiennej Stan zostaje początkowo przypisana wartość zakończona.
        /// </summary>
        public ModelGryIleDodac()
        {
            CzasMs = 400;
            Punkty = 0;
            Runda = 0;
            Stan = StanGryIle.zakończona;
        }

        /// <summary>
        /// Funkcja losująca liczbę z przedziału od 0 do 100 i zapisująca ją w zmiennej Liczba.
        /// Następnie funkcja losuje liczbę z przedziału od 0 do wartości zmiennej Liczba i zapisuje
        /// ją w zmiennej LiczbaOdejmowana.
        /// Funkcja zmiennej Roznica przypisuje różnicę pomiędzy wartością zmiennej 
        /// Liczba i LiczbaOdejmowana.
        /// Nastęnie funkcja zapisuje wartości zmiennych Liczba i Roznica w liście wylosowaneLiczby.
        /// </summary>
        public void LosujLiczbe()
        {
            wylosowaneLiczby = new List<int>();
            Liczba = Suma.Next(0, 101);
            LiczbaOdejmowana = Suma.Next(0, Liczba + 1);
            Roznica = Liczba - LiczbaOdejmowana;
            wylosowaneLiczby.Add(Liczba);
            wylosowaneLiczby.Add(Roznica);
        }
        /// <summary>
        /// Funkcja tworząca i zwracająca listę tylko do odczytu WylosowaneLiczby, 
        /// która przyjmuje wartości z listy wylosowaneLiczby.
        /// </summary>
        /// <returns>Lista tylko do odczytu WylosowaneLiczby</returns>

        public IReadOnlyList<int> WylosowaneLiczby()
        {
            return wylosowaneLiczby.AsReadOnly();
        }
        /// <summary>
        /// Funkcja, która daje użytkownikowi czas na obliczenie poszukiwanej liczby.
        /// Za każdym razem, gdy funkcja CzasNaOdp zostaje wywołana, zmniejsza ona wartość zmiennej buffor o 1.
        /// Gdy buffor osiągnie wartość 0 jego wartość zmieniana jest na 2, natomiast wartość zmiennej
        /// CzasMs jest zmniejszana o 50.
        /// Wartość zmiennej CzasMs jest zmniejszana do momentu, gdy jest ona większa od 150.
        /// Za każdym razem, gdy funkcja CzasNaOdp zostaje wywołana wartość zmiennej Runda zostaje zwiększona o 1, 
        /// natomiast wartości zmiennej Stan zostaje przypisana wartość zakończona.
        /// </summary>
        /// <param name="postep">Argumentem funkcji CzasNaOdp jest funkcja postep, która jest wywoływana
        /// w pętli z opóźnieniem równym wartości zmiennej CzasMs.</param>

        public void CzasNaOdp(Action postep)
        {
            Stan = StanGryIle.zakończona;
            for (int i = 0; i < 20; i++)
            {
                postep();
                Thread.Sleep(CzasMs);
            }
            if (CzasMs > 150)
            {
                if (buffor == 0)
                {
                    buffor = 2;
                    CzasMs -= 50;
                }
                else
                {
                    buffor--;
                }
            }
            Runda++;
        }
        /// <summary>
        /// Funkcja sprawdzająca, czy odpowiedź użytkownika zgadza się z wylosowaną liczbą.
        /// Funkcja sprawdza czy wartość zmiennej LiczbaOdejmowana jest równa liczbie podanej przez użytkownika.
        /// Jeżeli odpowiedź udzielona przez użytkownika jest prawidłowa to funkcja zwiększa wartość
        /// zmiennej Punkty o odpowiednią liczbę, która zależy od wartości zmiennej CzasMs.
        /// Jeżeli udzielona odpowiedź jest prawidłowa to funkcja przypisuje zmiennej Stan wartość wTrakcie.
        /// </summary>
        /// <param name="odpowiedzUzytkownika">Odpowiedź użytkownika będąca liczbą.</param>
        /// <returns>Funkcja zwraca wartość true w przypadku udzielenia prawidłowej odpowiedzi lub wartość
        /// false, gdy podana przez użytkownika odpowiedź jest błędna.</returns>

        public bool Weryfikacja(int odpowiedzUzytkownika)
        {

            if (odpowiedzUzytkownika == LiczbaOdejmowana)
            {

                switch (CzasMs)
                {
                    case 400:
                        Punkty++;
                        break;
                    case 350:
                        Punkty += 2;
                        break;
                    case 300:
                        Punkty += 3;
                        break;
                    case 250:
                        Punkty += 4;
                        break;
                    case 200:
                        Punkty += 5;
                        break;
                    case 150:
                        Punkty += 10;
                        break;



                }
                Stan = StanGryIle.wTrakcie;
                return true;
            }
            else
            {

                return false;
            }
        }
        /// <summary>
        /// Enum składające się z dwóch stałych - stanów, w których może znajdować się gra.
        /// </summary>
        public enum StanGryIle
        {
            wTrakcie,
            zakończona
        }

    }
}
