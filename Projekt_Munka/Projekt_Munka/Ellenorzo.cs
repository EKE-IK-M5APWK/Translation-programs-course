using System;
using System.Linq;
using System.Text.RegularExpressions;

namespace Projekt_Munka
{
    internal class Ellenorzo
    {
        private string bemenet;
        public void setBemenet(string bemenet)
        {
            this.bemenet = bemenet;
        }
        public string getBemenet()
        {
            return bemenet;
        }
        public bool bemenetBekeres()
        {
                if (bemenet.Length == 0)
                {
                    System.Console.WriteLine("A megadott bemenet hossza {0} karakter", bemenet.Length);
                    return false;
                }
                else
                {
                    bemenet = bemenetMegfelel(bemenet);
                    if (bemenet == "0")
                    {
                        System.Console.WriteLine("A megadott bemenet az ellenörzés után nem felel meg!\n Kérem javítsa a kifejezést.");
                        return false;
                    }
                }
            return true;
        }
        public string bemenetMegfelel(string bemenet)
        {
            string seged = Regex.Replace(bemenet, "([0-9]{1,2})+", "i");
            #region Zárójel szabály. Ell kell távolítani a zárójeleket.
            if (bemenet.Contains('('))
            {
                for (int i = 0; i < bemenet.Length; i++)
                {
                    if (bemenet[i] == '(')
                    {
                        bemenet = bemenet.Substring(i + 1, bemenet.Length - 1);
                        seged = bemenet;
                    }

                }
            }
            if (bemenet.Contains(')'))
            {
                for (int i = 0; i < bemenet.Length; i++)
                {
                    if (bemenet[i] == ')')
                    {
                        bemenet = bemenet.Remove(i, 1);
                        seged = bemenet;
                    }
                }

            }
            #endregion
            if (bemenet[bemenet.Length - 1] != '#')
            {
                seged += '#';
            }
            //Szabályok ellenörzése
            if (seged.Contains("i") && seged.Contains("*") && !seged.Contains("(")
                && !seged.Contains(")") && seged.Contains("#") && seged.Contains("+"))
            {
                return seged;
            }
            return "0";
        }
    }
}