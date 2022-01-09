using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace WarAnimals
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WindowLeft = 0;
            Console.WindowTop = 0;
            Console.WriteLine("Введите количесто юнитов на старте");
            string s = Console.ReadLine();
            int n = ChekInput(s,100);
            Console.WriteLine("Введите ширину поля (если рзначение будет больше возможного,\n то будет установлено максимально допустимое)");
             s = Console.ReadLine();
            int w = ChekInput(s,100);
            Console.WriteLine("Введите высоту поля (если рзначение будет больше возможного,\n то будет установлено максимально допустимое)");
            s = Console.ReadLine();
            int h = ChekInput(s, 50);
            Console.WriteLine("Укажите количество тактов которые будет жить ваш мир ))");
            s = Console.ReadLine();
            int t = ChekInput(s, 5000);
            Console.WriteLine("Укажите  в млсек задержку между кадрами ))");
            s = Console.ReadLine();
            int dt = ChekInput(s, 0);
            Console.Clear();
            var map = new Classes.Map(n,w,h);
            while (map.NumberTakt<=t)
            {
                map.Takt();
                System.Threading.Thread.Sleep(dt);
            }
            map.SaveExel();
            dt = 10;
            do
            {
                Console.SetCursorPosition(0, Console.WindowHeight );
                Console.WriteLine($"Программа будет закрыта через {dt} сек");
                System.Threading.Thread.Sleep(1000);
                dt--;
            } while (dt > 0);
           
        }
        static int ChekInput(string s,int byDefault)
        {
            int i;
            if(int.TryParse(s, out i))
            {
                return i;
            }
            else
            {
                Console.WriteLine($"Введенное значение невозможно преоразовать в целое число выбрано значение по умолчанию {byDefault}");
                return byDefault;
            }
        }
    }
}
