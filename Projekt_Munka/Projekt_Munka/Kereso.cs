using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;

namespace Projekt_Munka
{
    internal class Kereso
    {
        private string bemenet, seged;
        private Stack verem = new Stack();
        private bool statusz = true;
        private List<string> szabalyok = new List<string>();
        public Kereso(string v)
        {
            this.bemenet = v;
        }
        public void Kereses(List<List<string>> matrix, char c)
        {
            verem.Push("#");
            verem.Push(c);
            do
            {
                seged = verem.Pop().ToString();
                for (int j = 0; j < matrix[0].Count; j++)
                {
                    if (bemenet[0].ToString() == matrix[0][j])
                    {

                        for (int i = 0;  i < matrix.Count; i++)
                        {
                            if (seged == matrix[i][0])
                            {
                                elemVizsgalat(matrix[i][j]);
                            }
                        }
                    }
                }
            } while (statusz);
        }

        private void elemVizsgalat(string value)
        {
            /*
            d.Ha a cella egy zárójeles szabályt tartalmaz, akkor:
            If the cell contains a rule in parentheses, then:
            i.	el kell távolítani a zárójeleket.
            the parentheses must be removed.
            ii.	a vessző bal oldalán található szabályt és a jobb oldalán található sorszámot be kell tenni egy-egy változóba (pl.: a string[] elemek = String.Split(elemek, ”,”) metódussal).
            the rule to the left of the comma and the number to the right of the comma must be inserted into a variable (for example, with the string [] elements = String.Split (elements, “,”) method).
            iii.	A vessző bal oldalán található szabályt karakterenként a verembe kell helyezni.
            The rule to the left of the comma must be placed in the stack character by character.
            iv.	A szabály sorszámát el kell tárolni egy listába. Ezt az adatot nem használjuk, de a segítségével elő lehetne állítani a program szintaxisfáját.
            The sequence number of the rule must be stored in a list. We do not use this data, but it could be used to generate the syntax tree of the program.
*/
            if (value.Contains('('))
            {
                string seged = value.Substring(1).Split(',')[0];
                for (int j = seged.Length - 1; j >= 0; j--)
                {
                    verem.Push(seged[j].ToString());
                }
            }
            if (value.Contains(')'))
            {
                szabalyok.Add(value.Substring(0, value.Length - 1).Split(',')[1]);
                allapot("");

            }
            /*
             * b.	Ha a cella az elfogad szót tartalmazza, akkor a végére értünk az elemzésnek, és a kifejezés helyes.
                    If the cell contains the word „accept” (elfogad), we have reached the end of the analysis and the expression is correct.

             */
            if (value.Trim() == "elfogad")
            {
                Console.WriteLine("Elemzés véget ért. Kifejezés státusza: elfogad");
                statusz = false;
            }
            /*
             * c.	Ha a pop szó található a cellában, akkor el kell távolítani a verem tetején található elemet (egy karaktert, ami lehet terminális, vagy nemterminális jel), és az indexet léptetni kell, vagyis megnövelni az index változó értékét egyel.
                    If the word pop is in the cell, you must remove the element at the top of the stack (a character, which can be a terminal or non-terminal simbol) and eed to increase the value of the index variable by one.

             */
            if (value.Trim() == "pop")
            {
                bemenet = bemenet.Substring(1);
                allapot("pop");

            }
            /*
             * a.	Ha a cella üres, az azt jelenti, hogy a kifejezésben hibát találtunk.
                    If the cell is empty, we have found an error in the expression.

             */
            if (value.Length == 0)
            {
                Console.WriteLine("Hiba megadott érték hossza 0.");
                statusz = false;
            }
        }

        private void allapot(string v)
        {
            string a = "";
            string b = "";
            //Verem eleminek kigyűjtése
            foreach (string item in verem)
            {
                if (item != "e")
                {
                    a += item;
                }

            }
            //Szabalyok kigyűjtése
            foreach (string item in szabalyok)
            {
                b += item;
            }
            Console.WriteLine("({0}, {1}, {2}) {3}", bemenet, a, b, v);
        }
    }
}