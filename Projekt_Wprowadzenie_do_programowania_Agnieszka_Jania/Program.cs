using Model_Gry;
using System;
using System.Threading;

namespace Projekt_Wprowadzenie_do_programowania_Agnieszka_Jania
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Witaj w grze - zapamiętaj liczby");
            Console.Write("Podaj przedział losowania oddzielony spacją ");
            string[] dane = Console.ReadLine().Split(' ');
            int[] przedzial = Array.ConvertAll(dane, int.Parse);
            int powtorka = 3;
            for(int i = 0; i < powtorka; i++)
            {
                Console.WriteLine("Zapamiętaj w czasie wylosowane poniżej liczby, a następnie przepisz je");
                ModelGry gra = new ModelGry(przedzial[0], przedzial[1]);

                gra.Przerwa();
                Console.WriteLine("Przepisz liczby w kolejności");
                string[] odpowiedz = Console.ReadLine().Split(' ');
                int[] odp = Array.ConvertAll(odpowiedz, int.Parse);
                var Odp = gra.Propozycja(odp);
                Console.WriteLine(Odp);
            }


            
           
            
            




        }
    }
}
