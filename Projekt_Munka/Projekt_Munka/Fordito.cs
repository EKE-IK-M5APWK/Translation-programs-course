using System;
using System.Collections.Generic;
using System.IO;

namespace Projekt_Munka
{
    internal class Fordito
    {
        private string p;
        public Fordito(string p)
        {
            this.p = p;
        }
        /// <summary>
        /// openOders függvény - order.txt megnyitása után létrehoz egy mátrixot a benne lévő adatokról. Az elválasztó jel minden esetben a '|' karakter. 
        /// </summary>
        /// <returns> Vissza adja a beolvasott szabályokból készített mátrixot</returns>
        public List<List<string>> szabalyokBeszerzese(string p)
        {
            List<List<string>> matrix = new List<List<string>>();
            List<string> sor = new List<string>();
            string[] adat = new string[7];
            StreamReader sr = new StreamReader(p);
            string s = "";
            int index = 0;
            while (!sr.EndOfStream)
            {
                s += sr.ReadLine();
                for (int i = 0; i < adat.Length; i++)
                {
                    adat[i] = s.Split('|')[i];
                }
                sor = new List<string>();
                for (int j = 0; j < adat.Length; j++)
                {
                    sor.Add(adat[j]);

                }
                matrix.Add(sor);
                s = "";
                index++;
            }
            return matrix;
        }
        /// <summary>
        /// Beadandó feladat indítása
        /// </summary>
        internal void Inditas()
        {

            Ellenorzo ellenorzo = new Ellenorzo();
            bool statusz = false;
            bool preset = true;
            List<List<string>> szabalyok = null;
            while (!statusz)
            {
                Console.WriteLine("Szabályok betöltése...." + "\n");
                if (File.Exists(p))
                {
                    szabalyok = szabalyokBeszerzese(p); // Szabályok bekérése a fájlból.
                    while (preset)
                    {
                        Console.WriteLine("Szeretné használólni az előre meghatározott bemenetet? (Y/N) ");
                        string presetValue = "(i+i*i#,E#,e)";
                        Console.WriteLine("Bemenet: {0}",presetValue);
                        ConsoleKeyInfo result = Console.ReadKey();
                        if (result.Key == ConsoleKey.Y)
                        {

                            Console.WriteLine("\n________Folyamat________");
                            ellenorzo.setBemenet(presetValue);
                            if (ellenorzo.bemenetBekeres())
                            {
                                Kereso kereses = new Kereso(ellenorzo.getBemenet());
                                kereses.Kereses(szabalyok, 'E');
                                statusz = true;
                            }
                        }
                        else if (result.Key == ConsoleKey.N)
                        {
                            preset = false;
                        }

                    }
                    Console.WriteLine("\nKérem adjon meg egy kifejezést:");
                    ellenorzo.setBemenet(Console.ReadLine());
                    if (ellenorzo.bemenetBekeres())
                    {
                        Kereso kereses = new Kereso(ellenorzo.getBemenet());
                        kereses.Kereses(szabalyok, 'E');
                        statusz = true;
                    }

                }
                else
                {
                    Console.WriteLine("Szabály fájl nem létezik.");
                    statusz = true;
                }
                
            }
            Console.WriteLine("Fordítás befejeződött....");
            
        }

    }
}