using Model_Gry;
using System;

namespace Projekt_Wprowadzenie_do_programowania_Agnieszka_Jania
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Wybierz wersję gry:");
            Console.WriteLine("1 - zapamiętaj liczby");
            Console.WriteLine("2 - zapamiętaj liczby w odwrotnej kolejności");
            string wersjaGry;

            while (true)
            {


                wersjaGry = Console.ReadLine();

                if (wersjaGry == "1")
                {
                    while (true)
                    {

                        Console.WriteLine("Witaj w grze - zapamiętaj liczby");
                        Console.Write("Podaj przedział losowania oddzielony spacją ");
                        string[] dane = Console.ReadLine().Split(' ');

                        int[] przedzial = Array.ConvertAll(dane, int.Parse);
                        //if(przedzial.Length != 2)
                        //{
                        //    Console.WriteLine("Podaj przedział");
                        //    return;
                        //}


                        try
                        {
                            ModelGry gra = new ModelGry(przedzial[0], przedzial[1]);
                            gra.Losuj();
                            Console.WriteLine("Zapamiętaj w czasie wylosowane poniżej liczby, a następnie przepisz je");

                            while (gra.ileCyfr < 6)
                            {

                                foreach (var tmp in gra.Wylosowane())
                                {
                                    Console.Write(tmp);
                                    Console.Write(" ");
                                }
                                Console.WriteLine();
                                gra.Przerwa(Przerwa);
                                Console.Clear();
                                Console.WriteLine("Przepisz liczby w kolejności");
                                string[] odpowiedz = Console.ReadLine().Split(' ');
                                int[] odp = Array.ConvertAll(odpowiedz, int.Parse);

                                if (gra.Sprawdzenie(odp))
                                {
                                    Console.WriteLine("Odpowiedziano poprawnie.");
                                    Console.WriteLine($"Status gry: {gra.stangry}");
                                    gra.Losuj();
                                }
                                else
                                {
                                    Console.WriteLine("Odpowiedziano źle.");
                                    Console.WriteLine("Gra zakończona.");
                                    Console.WriteLine($"Status gry: {gra.stangry}");

                                    return;
                                }


                            }
                            if (gra.ileCyfr == 6)
                            {
                                Console.WriteLine("Gratulacje! Wygrałeś!");

                                return;
                            }
                        }
                        catch (ArgumentException e)
                        {
                            Console.WriteLine(e.Message);
                        }
                    }
                }
                else if (wersjaGry == "2")
                {
                    Console.WriteLine("Jeszcze nad tym pracuje");
                    break;
                }
                else
                {
                    Console.WriteLine("Proszę wpisać 1 lub 2");
                }


            }
            void Przerwa()
            {
                Console.BackgroundColor = ConsoleColor.Red;
                for (int i = 0; i < Console.WindowWidth / 20; i++)
                    Console.Write(' ');
                Console.ResetColor();
            }

        }

    }
}
