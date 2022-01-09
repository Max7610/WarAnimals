using Microsoft.Office.Interop.Excel;
using OfficeOpenXml;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Excel = Microsoft.Office.Interop.Excel;


namespace TestMakeTable
{
    class Program
    {
        static void Main(string[] args)
        {
            try
            {
                using (BO.ExelHelper helper = new BO.ExelHelper())
                {
                    if (helper.Open(filePath: Path.Combine(Environment.CurrentDirectory,"TestLt.xlsx")))
                    {
                        helper.Set(column:"A",row: 1,data: "TestText"); ;

                        helper.Save();
                    }
                };
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
    }
    public class Statistic
    {
        int[] S;
        int n;
        public Statistic(int a)
        {
            n = a;
            S = new int[a];
            Random r = new Random();
            for(int i = 0; i < a; i++)
            {
                S[i] = r.Next(100);
                Console.WriteLine($"Создано число {i}");
            }
            Console.WriteLine("Отчет готов");
        }
    }
   

}
