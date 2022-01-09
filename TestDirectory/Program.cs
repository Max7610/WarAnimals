using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.IO;

namespace TestDirectory
{
    class Program
    {
        static void Main(string[] args)
        {
            DateTime date = new DateTime();
            string DireciriName = DateTime.Now.ToString();
            string currentPath = Directory.GetCurrentDirectory();
            Console.WriteLine(DireciriName);
            Console.ReadKey();
            DireciriName = DireciriName.Replace('.', '_');
            DireciriName = DireciriName.Replace(':', '_');
            DireciriName = DireciriName.Replace(' ', '_');
            Console.WriteLine(DireciriName);
            Console.ReadKey();
            if (!Directory.Exists(Path.Combine(currentPath, DireciriName)))
                Directory.CreateDirectory(Path.Combine(currentPath, DireciriName));
            Console.WriteLine(currentPath);
            Console.ReadKey();
        }
    }
}
