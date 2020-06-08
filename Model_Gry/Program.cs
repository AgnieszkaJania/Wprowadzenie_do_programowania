using System;
using System.Linq.Expressions;
using System.Security.Cryptography.X509Certificates;
using System.Threading;
using System.Threading.Tasks;

namespace Model_Gry
{   
    public class ModelGry
    {
        public int liczbaWylosowana;
        public int ileCyfr = 4;
        public int[] cyfry;
        public bool flaga = true;
        
        public ModelGry(int min, int max)
        {   
            
            if (min < 0 || max < 0)
                Console.WriteLine("Proszę podać liczby nieujemne");


            if (min > max)
                throw new ArgumentException("zły przedział do losowania");
            
            cyfry = new int[ileCyfr];
            for (int i = 0; i < ileCyfr; i++)
            {
                liczbaWylosowana = (new Random()).Next(min, max + 1);
                cyfry[i] = liczbaWylosowana;

            }
            for(int j = 0; j < cyfry.Length; j++)
            {
                Console.Write($"{cyfry[j]} ");
            }
            Console.WriteLine();
            

            
        }
        public void Przerwa()
        {
            for (int i = 0; i < 20; i++)
            {
                Console.BackgroundColor = ConsoleColor.Red;
                Console.Write(' ');
                Thread.Sleep(100);

            }
            Console.BackgroundColor = ConsoleColor.Black;
            Console.Clear();
        }



        public Odpowiedz Propozycja(int[] propzycja)
        {
            Odpowiedz odp;
            if (cyfry.Length != propzycja.Length)
                throw new ArgumentException("Podano błędne cyfry");
            for(int i = 0; i < cyfry.Length; i++)
            {
                if(cyfry[i] == propzycja[i])
                {
                    flaga = true;
                }
                else
                {
                    flaga = false;
                    break;
                }
            }
            if (flaga)
            {
                odp = Odpowiedz.dobrze;
            }
            else
            {
                odp = Odpowiedz.źle;
            }
            return odp;



        }
        //public void wynikGry(Odpowiedz odp)
        //{
        //    if(odp == -1)
        //    {

        //    }
        //}
        public enum StanGry
        {
            wTrakcie,
            przegrana,



        }

        public enum Odpowiedz
        {
            źle = -1,
            dobrze = 1
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
