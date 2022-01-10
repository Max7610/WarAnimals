
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.Office.Interop.Excel;
using System;
using System.IO;
using Excel = Microsoft.Office.Interop.Excel;

namespace WarAnimals.Classes
{
    class Statistic
    {
        public int takt;
        public int count;
        public int[] lvl = new int[100];
        public int maxLvl()
        {
            int max = 0;
            for(int i = 0; i < 100; i++)
            {
                if (lvl[i] > 0) max = i;
            }
            return max;
        }
    }
    class StatisticList
    {
        List<Statistic> StatList = new List<Statistic>();
        int Count => StatList.Count;
        public void Add(Statistic s)
        {
            StatList.Add(s);
        }

        public int MaxLvlList()
        {
            int m = 0;
            foreach (var a in StatList)
            {
                
                if (a.maxLvl()>m) m = a.maxLvl();
            }
            return m;
        }
       
        public void SaveStatisticExel()
        {
            string DireciriName;
            DireciriName ="day_"+ DateTime.Now.Day.ToString() +"_month_"+ DateTime.Now.Month.ToString();
            DireciriName = DireciriName.Replace('.', '_');
            DireciriName = DireciriName.Replace(':', '_');
            DireciriName = DireciriName.Replace(' ', '_');
            string currentPath = Directory.GetCurrentDirectory();
            if (!Directory.Exists(Path.Combine(currentPath, DireciriName)))
                Directory.CreateDirectory(Path.Combine(currentPath, DireciriName));
            var ABC = MakeABC();

            if (!Directory.Exists(Path.Combine(currentPath, DireciriName)))
                Directory.CreateDirectory(Path.Combine(currentPath, DireciriName));
            string ExelName = "WarTackt-" + StatList.Count.ToString() + DateTime.Now.ToString();
            ExelName = ExelName.Replace('.', '_');
            ExelName = ExelName.Replace(':', '_');
            ExelName = ExelName.Replace(' ', '_');
            try
            {
                using (ExelHelper helper = new ExelHelper())
                {
                    if (helper.Open(filePath: Path.Combine(currentPath + "\\" + DireciriName, ExelName + ".xlsx")))
                    {
                        helper.Set(column: ABC[0].ToString(), row: 1, data: "Номер Такта");
                        helper.Set(column: ABC[1].ToString(), row: 1, data: "Количество объектов");
                        for(int i = 2; i < MaxLvlList() + 2; i++)
                        {
                            helper.Set(column: ABC[i].ToString(), row: 1, data: $"LVL {i-1}");
                        }
                        int n = 2;
                        int p = StatList.Count;
                        while (StatList.Count > 0)
                        {
                            helper.Set(column: ABC[0].ToString(), row: n, data: StatList[0].takt);
                            helper.Set(column: ABC[1].ToString(), row: n, data: StatList[0].count);
                            for (int i = 1; i <= StatList[0].maxLvl(); i++)
                            {

                                helper.Set(column: ABC[i+1].ToString(), row: n, data: StatList[0].lvl[i]);
                            }
                            n++;
                            StatList.RemoveAt(0);
                            if (n * 100 % p == 0)
                            {
                                Console.SetCursorPosition(0, Console.WindowHeight-3);
                                Console.WriteLine($"сохранено {n * 100 / p} %  ");
                            }
                        }
                        Console.SetCursorPosition(0, Console.WindowHeight - 2);
                        Console.WriteLine($"Статистика сохранена в  " + currentPath + "\\" + DireciriName+ "\\"+ExelName+ ".xlsx");
                        helper.Save();
                    }
                };
            }
            catch (Exception ex) { Console.WriteLine(ex.Message); }
        }
        string[] MakeABC()
        {
            string[] _ABC = {
                "A", "B","C","D","E","F","G","H","I","J","K","L","M","N","O",
                "P","Q","R","S","T","U","V","W","X","Y","Z"};
            string[] ABC = new string[MaxLvlList()+ 3];
            for (int i = 0; i < MaxLvlList() + 3; i++)
            {
                if (i < _ABC.Length)
                {
                    ABC[i] = _ABC[i];
                }
                else
                {
                    ABC[i] = _ABC[i / _ABC.Length-1] + _ABC[i % _ABC.Length];
                }
            }

            return ABC;
        }

    }
}
