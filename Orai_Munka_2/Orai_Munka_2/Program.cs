using System;
using System.Text.RegularExpressions;
using System.IO;
using System.Collections.Generic;
using static System.Net.Mime.MediaTypeNames;
using System.Diagnostics;

namespace Orai_Munka_2
{
    class Program
    {
        class sourceHandler
        {
            string source, finalcode , orders = "";
            string content = "";

            Dictionary<string, string> replaces = new Dictionary<string, string>();
            public void openOrders() 
            {
                try
                {
                    StreamReader sr = new StreamReader(File.OpenRead(orders));
                    string s = sr.ReadLine();
                    string[] vs = s.Split("|");

                    replaces.Add(vs[0], vs[1]);
                    sr.Close();
                }
                catch (IOException IOE)
                {
                    Console.WriteLine(IOE.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            List<string> symbolTable = new List<string>();
            int symbolTableIndex = 0;
            string changeVariablesAndConstants(string varAndConstName)
            {
                symbolTable.Add(varAndConstName);
                symbolTableIndex += 1;
                string res = "00" + symbolTableIndex.ToString();
                return res.Substring(res.Length - 3);
            }
            public void replaceContet()
            {
                var blockComment = @"/[*][\w\d\s]+[*]";
                var lineComment = @"//.*?\n";
                string patternNumber = @"([0-9][2-4]+)";
                string patternVar = @"([a-z-_]+)";

                content = Regex.Replace(content, blockComment, " ");
                content = Regex.Replace(content, lineComment, " ");

                content = Regex.Replace(content, patternNumber, changeVariablesAndConstants("$1"));
                content = Regex.Replace(content, patternVar, changeVariablesAndConstants("$1"));

                foreach (var x in replaces)
                {
                    while (content.Contains(x.Key))
                        content = content.Replace(x.Key, x.Value);
                }
            }
            public void openFileToWrite()
            {
                try
                {
                    StreamWriter sw = new StreamWriter(File.Open(finalcode, FileMode.Create));
                    sw.WriteLine(content);
                    sw.Flush();
                    sw.Close();
                }
                catch (IOException IOE)
                {
                    Console.WriteLine(IOE.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            public void openFileToRead()
            {
                try
                {
                    StreamReader sr = new StreamReader(File.OpenRead(source));
                    content = sr.ReadToEnd();
                    sr.Close();
                }
                catch (IOException IOE)
                {
                    Console.WriteLine(IOE.Message);
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                }
            }
            public sourceHandler(string source, string finalcode, string orders)
            {
                this.source = source;
                this.finalcode = finalcode;
                this.orders = orders;
            }
        }

        static void Main(string[] args)
        {
            string source = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"\source.txt";
            string finalcode = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName) + @"\finalcode.txt";
            string orders = Path.GetDirectoryName(Process.GetCurrentProcess().MainModule.FileName)+ @"\orders.txt";
            sourceHandler sourceHandler = new sourceHandler(source,finalcode,orders);

            sourceHandler.openFileToRead();
            sourceHandler.openOrders();
            sourceHandler.replaceContet();
            sourceHandler.openFileToWrite();
            Console.WriteLine("Folyamat befejezve!");
        }
    }
}
