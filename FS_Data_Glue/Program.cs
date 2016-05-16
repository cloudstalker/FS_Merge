using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FS_Data_Glue
{
    class Program
    {
        static void Main(string[] args)
        {
            Console.WriteLine("Default:10min UV & 2min Vis");
            Console.WriteLine("Input UV cycles:");
            string n = Console.ReadLine();
            int UV_cycle = Convert.ToInt32(n);
            Console.WriteLine("Input Data Path:");
            string path = @Console.ReadLine();
            GlueUtils GlueUtils = new GlueUtils();
            GlueUtils.CheckFile(UV_cycle, path);
            GlueUtils.Merge(UV_cycle, path);
        }
    }

    class GlueUtils
    {
        public void CheckFile(int n, string p)
        {
            int counter = 0;
            if (p.EndsWith("\\")){
                p.TrimEnd('\\');
            }
            for(int i = 0; i <= n+1; i++)
            {
                if (!System.IO.File.Exists(p + "vis" + i.ToString() + ".csv"))
                {
                    Console.WriteLine(p + "vis" + i.ToString() + ".csv does not exist");
                    counter++;
                }
                if (!System.IO.File.Exists(p + "uv" + i.ToString() + ".csv") && i!=0 && i!=10)
                {
                    Console.WriteLine(p + "uv" + i.ToString() + ".csv does not exist");
                    counter++;
                }
            }
            if (counter > 0)
            {
                Console.WriteLine("Fatal Error: Lack File");
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Files Discovered! Processing...");
            }
        }

        public void Merge(int n, string p)
        {

        }
    }
}
