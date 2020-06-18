using Model_Gry;
using System;

namespace Projekt_Wprowadzenie_do_programowania_Agnieszka_Jania
{
    class Program
    {
        static void Main(string[] args)
        {

            while (true)
            {

                PrintAction();
                string wersjaGry = Console.ReadLine();
                Console.Clear();

                if (wersjaGry == "1")
                {
                    PrintAction2();

                    try
                    {
                        string[] dane = Console.ReadLine().Split(' ');
                        int[] przedzial = Array.ConvertAll(dane, int.Parse);
                        if (przedzial.Length != 2)
                        {
                            throw new IndexOutOfRangeException();
                        }

                        ModelGry gra = new ModelGry(przedzial[0], przedzial[1]);
                        gra.Losuj();


                        while (gra.IleCyfr < 10)
                        {
                            Console.WriteLine("Zapamiętaj w czasie wylosowane poniżej liczby, a następnie przepisz je.");
                            foreach (var tmp in gra.Wylosowane())
                            {
                                Console.Write(tmp);
                                Console.Write(" ");
                            }
                            Console.WriteLine();
                            gra.Przerwa(Przerwa);
                            Console.Clear();
                            Console.WriteLine("Przepisz liczby w kolejności:");

                            try
                            {
                                string[] odpowiedz = Console.ReadLine().Split(' ');
                                int[] odp = Array.ConvertAll(odpowiedz, int.Parse);
                                if (gra.Sprawdzenie(odp))
                                {
                                    Console.WriteLine("Odpowiedziano poprawnie.");
                                    Console.WriteLine($"Status gry: {gra.Stangry}");
                                    gra.Losuj();
                                }
                                else
                                {
                                    Console.WriteLine("Odpowiedziano źle.");
                                    Console.WriteLine("Gra zakończona.");
                                    Console.WriteLine($"Status gry: {gra.Stangry}");
                                    break;
                                }
                            }
                            catch (Exception)
                            {
                                Console.WriteLine("Odpowiedziano źle. Nie wpisano liczb.");
                                Console.WriteLine("Gra zakończona.");
                                Console.WriteLine($"Status gry: {gra.Stangry}");

                                break;

                            }



                        }
                        if (gra.IleCyfr == 6)
                        {
                            Console.WriteLine("Gratulacje! Wygrałeś!");


                        }

                    }
                    catch (IndexOutOfRangeException)
                    {
                        Console.Clear();
                        Console.WriteLine("Przedział losowania musi składać się z dwóch liczb!");
                    }
                    catch (FormatException)
                    {
                        Console.Clear();
                        Console.WriteLine("Przedział musi składać się z liczb!");
                    }
                    catch (ArgumentException e)
                    {
                        Console.Clear();
                        Console.WriteLine(e.Message);
                    }
                    PrintAction3();


                }
                else if (wersjaGry == "2")
                {
                    PrintAction1();
                    ModelGryIleDodac graIle = new ModelGryIleDodac();
                    graIle.LosujLiczbe();

                    while (true)
                    {

                        Console.WriteLine("Ile należy dodać do drugiej liczby, aby otrzymać pierwszą?");
                        foreach (var abc in graIle.WylosowaneLiczby())
                        {
                            Console.Write(abc);
                            Console.Write(" ");

                        }

                        Console.WriteLine();
                        graIle.CzasNaOdp(Przerwa);
                        Console.Clear();
                        Console.WriteLine("Wpisz wynik:");
                        try
                        {
                            int odpowiedz = int.Parse(Console.ReadLine());
                            if (graIle.Weryfikacja(odpowiedz))
                            {
                                Console.WriteLine("Dobra odpowiedź !");
                                Console.WriteLine($"Status gry: {graIle.Stan}");
                                Console.WriteLine($"Przeszedłeś/przeszłaś już: {graIle.Runda} rund.  Zdobyte punkty: {graIle.Punkty}");
                                
                                graIle.LosujLiczbe();

                            }
                            else
                            {
                                Console.WriteLine("Zła odpowiedź !");

                                Console.WriteLine($"Status gry: {graIle.Stan}");
                                Console.WriteLine($"Szukana liczba to: {graIle.LiczbaOdejmowana}");
                                Console.WriteLine($"Zdobyłeś/aś {graIle.Punkty} punktów.");
                                Console.WriteLine($"Przeszedłeś/przeszłaś {graIle.Runda} rund.");

                                break;
                            }
                        }
                        catch (FormatException)
                        {
                            Console.WriteLine("Należy podać jedną liczbę");

                            Console.WriteLine($"Status gry: {graIle.Stan}");
                            break;

                        }

                    }
                    PrintAction3();

                }
                else if (wersjaGry == "0")
                {
                    Console.WriteLine("Zamykanie ...");
                    Environment.Exit(0);
                }
                else
                {
                    Console.WriteLine("Proszę wpisać 0, 1 lub 2");
                }

            }
            //Rysowanie paska upływu czasu na konsoli.
            void Przerwa()
            {
                Console.BackgroundColor = ConsoleColor.Red;
                for (int i = 0; i < Console.WindowWidth / 20; i++)
                    Console.Write(' ');
                Console.ResetColor();
            }

        }
        //Metoda wyświetlająca napisy na konsoli.
        //Menu umożliwiające wybór opcji.
        private static void PrintAction()
        {
            Console.WriteLine("Witaj !");
            Console.WriteLine("Wybierz wersję gry:");
            Console.WriteLine("1 - zapamiętaj liczby");
            Console.WriteLine("2 - ile dodać?");
            Console.WriteLine("0 - wyjście");
        }
        //Metoda wyświetlająca napisy na konsoli i czekająca na naciśnięcie klawisza przez użytkownika.
        //Zasady gry - ile dodać?
        private static void PrintAction1()
        {
            Console.WriteLine("Witamy w grze - ile dodać");
            Console.WriteLine("Zostaną wyświetlone dwie liczby i po pewnym czasie znikną. \nMusisz ustalić ile należy dodać do drugiej liczby, aby otrzymać pierwszą. \nNastępnie wpisz wynik.");
            Console.WriteLine("Naciśnij dowolny klawisz, aby rozpocząć grę.");
            Console.ReadKey();
        }
        //Metoda wyświetlająca napisy na konsoli.
        //Zasady gry - zapamiętaj liczby.
        private static void PrintAction2()
        {
            Console.WriteLine("Witaj w grze - zapamiętaj liczby");
            Console.WriteLine("Zostanie wyświetlony ciąg liczb i po pewnym czasie zniknie. \nZapamiętaj ten ciąg liczb, a następnie przepisz go w takiej samej kolejności. ");
            Console.Write("Podaj przedział losowania liczb oddzielony spacją, a następnie wciśnij enter. ");
            Console.WriteLine();
        }
        //Metoda wyświetlająca napis na konsoli i czyszcząca zawartość konsoli po nasiśnięciu klawisza przez użytkownika.
        //Przejście wymagające akcji użytkownika.
        private static void PrintAction3()
        {
            Console.WriteLine("Naciśnij dowolny klawisz, aby kontynuować.");
            Console.ReadKey();
            Console.Clear();
        }



    }
}
