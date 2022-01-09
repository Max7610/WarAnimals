
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
    class Map
    {
        char[,] map;
        int count_animals;
        int height;
        int widht;
        int countTakt;
        int countDead;
        int MaxLvl;
        int StartUnits;
        string News="";  
        public int NumberTakt=>countTakt;
        List<Animal> Anim = new List<Animal>();
        List<Animal> DeathList = new List<Animal>();
        List<Animal> LifeList = new List<Animal>();
        StatisticList Stat = new StatisticList();
        public Map(int n, int wid,int heig)
        {
            //Создаем стены, персонажей, указываем гобариты окна
            if (wid > Console.LargestWindowWidth-2) widht = Console.LargestWindowWidth - 3;else  widht = wid;
            if (heig > Console.LargestWindowHeight - 4) height = Console.LargestWindowHeight - 5; else height = heig;
            Console.WindowHeight = height+5;
            if (widht > 50) Console.WindowWidth = widht + 3; else Console.WindowWidth = 50;
            StartUnits = n;
            map = new char[widht+1, height+1];// координаты указываем   X Y
            MakeWall();
            count_animals = n-1;
            countTakt = 1;
            MaxLvl = 1;
            for(int i = 0; i < n; i++)
            {
                var A = new Animal(i, widht, height);
                map[A.X, A.Y] = A.Sprite();
                Anim.Add(A);
                count_animals++;
            }
            PrintMap();
        }
        void MakeWall()
        {
            for (int i=0;i<widht;i++)
            {
                map[i, 0] = '#';
                map[i, height] = '#';
            }
            for (int i = 1; i < height; i++)
            {
                map[0, i] = '#';
                map[widht, i] = '#';
            }
        }
        public void Takt()
        {
            // в одном такте должен быть 
            // шаг, удар, проверка умерших, проверка рождаемости, перерасчет карты, прорисовка персонажей 
            Go();//шаг
            Batle();//деремся
            CheckDeath();// убираем мертвых
            CheckNewLife();// добавляем рожденных
            CheckMap();// чистим карту и вставляем в карту текущие обьекты 
            PrintMap();//рисуем все это на экране 
            countTakt++;
        }
        void Go()
        {
            foreach(var A in Anim)
            {
                A.Go();
            }
            if (countTakt % 5 == 0)
            {
                Statistic st = new Statistic();
                st.count = Anim.Count();
                st.takt = countTakt;
                foreach (var a in Anim)
                {
                    st.lvl[a.Lvl]++;
                    if (a.Lvl > MaxLvl) MaxLvl = a.Lvl;
                }
                Stat.Add(st);
            }
           
        }
        void Batle()
        {
            foreach(var a in Anim)
            {
                if (a.CheckDeath() && a.CheckNewLife())
                {
                    count_animals++;
                    var A = new Animal(count_animals, widht, height);
                    LifeList.Add(A);
                }
                if (!a.CheckDeath() && !DeathList.Contains(a)) DeathList.Add(a);
                else
                foreach (var b in Anim)
                {
                    if (a != b && a.X==b.X && a.Y==b.Y)
                    {
                        a.SetExp(b.GetExpSetDm(a.GetAtt()));
                        b.SetExp(a.GetExpSetDm(b.GetAtt()));
                       
                        if (!b.CheckDeath() && !DeathList.Contains(b)) DeathList.Add(b);
                        if (!a.CheckDeath() && !DeathList.Contains(a))
                        {
                            DeathList.Add(a);
                            break;
                        }
                    }
                }
            }
        }
        void CheckDeath()
        {
            countDead += DeathList.Count;
           foreach(var d in DeathList)
            {
                Anim.Remove(d);
            }
            DeathList.Clear();
        }
        void CheckNewLife()
        {
            Anim.AddRange(LifeList);
            LifeList.Clear();
        }
        void CheckMap()
        {
            for (int x = 1; x < widht; x++)
            {
                for (int y = 1; y < height; y++)
                {
                    map[x, y] = ' ';
                }
            }
           foreach(var a in Anim)
            {
                map[a.X, a.Y] = a.Sprite();
            }

        }
        void PrintMap()
        {
            Console.SetCursorPosition(0, 0);
            for(int y = 0; y <= height;y++)
            {
                string s = "";
                for(int x = 0; x <= widht; x++)
                {
                    s += map[x, y];
                }
                Console.WriteLine(s);
            }
            Console.WriteLine($"Такт {countTakt} Народу {Anim.Count} номер послежнего {count_animals} умерло {countDead}  ");
            Console.WriteLine(News);
        }
        public void SaveExel()
        {
            Stat.SaveStatisticExel();
        }  
      
       
    }
}
