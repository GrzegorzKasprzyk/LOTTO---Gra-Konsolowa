﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace LOTTO___Gra_Konsolowa
{
    class Program
    {
        static int KUMULACJA;
        static int START = 40;
        static Random rnd = new Random();
        static void Main(string[] args)
        {
            int pieniądze = START;
            int dzien = 0;

            do
            {

                pieniądze = START;
                dzien = 0;
                ConsoleKey wybor;
                do
                {
                    KUMULACJA = rnd.Next(2, 37) * 1000000;
                    dzien++;
                    int losow = 0;
                    List<int[]> kupon = new List<int[]>();
                   
                    do
                    {
                        Console.Clear();
                        Console.WriteLine("DZIEŃ: {0} ", dzien);
                        Console.ForegroundColor = ConsoleColor.Red;
                        Console.WriteLine("\nWitaj w grze LOTTO, dziś do wygrania jest mnóstwo siana - {0} zł. \nKto wie, może to Tobie się poszcześci i Twojemu szefowi z zazdrości szczena zachrzęści:)", KUMULACJA);
                        Console.ForegroundColor = ConsoleColor.DarkBlue;
                        Console.WriteLine("\nStan Konta: {0} zł", pieniądze);

                        WyswietlKupon(kupon);
                        Console.ResetColor();

                        //MENU

                        if (pieniądze >= 3 && losow < 8)
                        Console.ForegroundColor = ConsoleColor.DarkGreen;
                        {
                            Console.WriteLine("\n1. - Postaw nowy los - 3zł [{0}/8]", losow + 1);

                        }

                        Console.WriteLine("2. - Sprawdź kupon - losowanie");
                        Console.WriteLine("3. - Zakończ grę");
                        Console.ResetColor();
                        //MENU
                        wybor = Console.ReadKey().Key;
                        if (wybor == ConsoleKey.D1 && pieniądze >= 3 && losow < 8)
                        {
                            kupon.Add(PostawLos());
                            pieniądze -= 3;
                            losow++;
                        }
                        
                    } while (wybor == ConsoleKey.D1);
                    Console.Clear();
                    if (kupon.Count > 0)
                    {
                        int wygrana = Sprawdz(kupon);
                        if (wygrana > 0)
                        {
                            Console.ForegroundColor = ConsoleColor.Green;
                            Console.WriteLine("\nBrawo wygrałeś {0}zł w tym losowaniu!", wygrana);
                            Console.ResetColor();
                            pieniądze += wygrana;
                        }

                        else
                        {
                            Console.ForegroundColor = ConsoleColor.Red;
                            Console.WriteLine("\nNiestety nic nie wygrałeś");
                            Console.ResetColor();
                        }
                    }
                    else
                    {
                        Console.WriteLine("Nie miałeś losów w tym losowaniu.");

                    }
                    Console.WriteLine("Enter - kontynuuj.");
                    Console.ReadKey();

                } while (pieniądze >= 3 && wybor != ConsoleKey.D3);

                Console.Clear();
                Console.WriteLine("Dzień {0}.\nKoniec gry,Twój wynik to {1} zł", dzien, pieniądze - START);
                Console.WriteLine("ENTER - graj od nowa.");

            } while (Console.ReadKey().Key == ConsoleKey.Enter);
        }

        private static int Sprawdz(List<int[]> kupon)
        {
            //int wygrana = 0;
            int[] wylosowane = new int[6];
            for (int i = 0; i < wylosowane.Length; i++)
            {
                int los = rnd.Next(1, 50);
                if (!wylosowane.Contains(los))
                {
                    wylosowane[i] = los;
                }
                else
                {
                    i--;
                }
            }

            Array.Sort(wylosowane);
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("Wylosowane liczby to: ");
            Console.ResetColor();

            foreach (int liczba in wylosowane)
            {
                Console.Write(liczba + ", ");

            }
            int[] trafione = SprawdzKupon(kupon, wylosowane);
            int wartosc = 0;
            int wygrana = 0;

            Console.WriteLine();

            if (trafione[0] > 0)
            {
                wartosc = trafione[0] * 24;
                Console.WriteLine("3 Trafienia: {0} + {1}zł ", trafione[0], wartosc);
                wygrana += wartosc;
            }

            if (trafione[1] > 0)
            {
                wartosc = trafione[1] * rnd.Next(100, 301);
                Console.WriteLine("4 Trafienia: {0} + {1}zł ", trafione[1], wartosc);
                wygrana += wartosc;
            }

            if (trafione[2] > 0)
            {
                wartosc = trafione[2] * rnd.Next(4000, 8001);
                Console.WriteLine("5 Trafienia: {0} + {1}zł ", trafione[2], wartosc);
                wygrana += wartosc;
            }

            if (trafione[3] > 0)
            {
                wartosc = (trafione[3] * KUMULACJA) / (trafione[3] + rnd.Next(0, 5));
                Console.WriteLine("6 Trafień: {0} + {1}zł ", trafione[3], wartosc);
                wygrana += wartosc;
            }
            return wygrana;
        }

        private static int[] SprawdzKupon(List<int[]> kupon, int[] wylosowane)
        {
            int[] wygrane = new int[4];
            int i = 0;
            Console.ForegroundColor = ConsoleColor.DarkYellow;
            Console.WriteLine("\n\nTWÓJ KUPON: ");
            Console.ResetColor();
            foreach (int[] los in kupon)
            {
                i++;
                Console.Write(i + ": ");
                int trafien = 0;
                foreach (int liczba in los)
                {
                    if (wylosowane.Contains(liczba))
                    {
                        Console.ForegroundColor = ConsoleColor.Green;
                        Console.Write(liczba + ", ");
                        Console.ResetColor();
                        trafien++;
                    }
                    else
                    {
                        Console.Write(liczba + ", ");
                    }
                }
                switch (trafien)
                {
                    case 3:
                        wygrane[0]++;
                        break;

                    case 4:
                        wygrane[1]++;
                        break;

                    case 5:
                        wygrane[2]++;
                        break;

                    case 6:
                        wygrane[3]++;
                        break;
                }
                Console.ForegroundColor = ConsoleColor.DarkYellow;
                Console.WriteLine(" - Trafiono {0}/6 liczb", trafien);
                Console.ResetColor();

            }
            return wygrane;
        }

        private static int[] PostawLos()
        {
            int[] liczby = new int[6];
            int liczba = -1;
            for (int i = 0; i < liczby.Length; i++)
            {
                liczba = -1;
                Console.Clear();
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("Postawione liczby: ");
                Console.ResetColor();

                foreach (int l in liczby)
                {
                    if (l > 0)
                    {
                        Console.WriteLine(l + ", ");
                    }        
                }
                Console.ForegroundColor = ConsoleColor.Red;
                Console.WriteLine("\nWybierz liczbę od 1 do 49:");
                Console.ResetColor();
                Console.WriteLine("{0}/6: ", i + 1);
                bool prawidlowa = int.TryParse(Console.ReadLine(), out liczba);
                if (prawidlowa && liczba >= 1 && liczba <= 49 && !liczby.Contains(liczba))
                {
                    liczby[i] = liczba;
                }
                else
                {
                    Console.ForegroundColor = ConsoleColor.Red;
                    Console.WriteLine("Niestety, błędna liczba.");
                    Console.ResetColor();
                    i--;
                    Console.ReadKey();
                }

            }
            Array.Sort(liczby);
            return liczby;
        }
        //return null;
        private static void WyswietlKupon(List<int[]> kupon)
        {
            if (kupon.Count == 0)
            {
                Console.ForegroundColor = ConsoleColor.DarkBlue;
                Console.WriteLine("Nie postawiłeś jeszcze żadnych losów.");
                Console.ResetColor();
            }
            else
            {
                int i = 0;
                Console.ForegroundColor = ConsoleColor.DarkGreen;
                Console.WriteLine("\nTWÓJ KUPON:  ");
                Console.ResetColor();
                foreach (int[] los in kupon)
                {
                    i++;
                    Console.WriteLine(i + ": ");
                    foreach (int liczba in los)
                    {
                        Console.Write(liczba + ", ");
                    }
                    Console.WriteLine();

                }

            }
        }
    }
}
