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
            Console.WriteLine("Input Data Path: (Press Enter to use current directory)");
            string path = @Console.ReadLine();
            if (path == @"")
            {
                path = System.IO.Directory.GetCurrentDirectory();
            }
            GlueUtils GlueUtils = new GlueUtils();
            GlueUtils.CheckFile(UV_cycle, path);
            GlueUtils.Merge(UV_cycle, path);
            Console.WriteLine("Successfully written to " + path);
            Console.WriteLine("Press Enter to Continue...");
            Console.ReadLine();
        }
    }

    class GlueUtils
    {
        public void CheckFile(int n, string p) // Check all files, if not there, end program
        {
            int counter = 0;
            if (p.EndsWith("\\")){
                p.TrimEnd('\\');
            }
            for(int i = 0; i <= n+1; i++)
            {
                if (!System.IO.File.Exists(p + "\\vis" + i.ToString() + ".csv"))
                {
                    Console.WriteLine(p + "\\vis" + i.ToString() + ".csv does not exist");
                    counter++;
                }
                if (!System.IO.File.Exists(p + "\\uv" + i.ToString() + ".csv") && i!=0 && i!=n+1)
                {
                    Console.WriteLine(p + "\\uv" + i.ToString() + ".csv does not exist");
                    counter++;
                }
            }
            if (counter > 0)
            {
                Console.WriteLine("Fatal Error: Lack File");
                Console.ReadLine();
                Environment.Exit(0);
            }
            else
            {
                Console.WriteLine("Files Discovered! Processing...");
            }
        }

        public void Merge(int n, string p) // Merge all *.csv and output a combined one in path
        {
            using (System.IO.StreamWriter Output = new System.IO.StreamWriter(p + "\\output.csv"))
            {
                Output.WriteLine("Sample7_Ex350_Em520,,Sample7_Ex350_Em563,,Sample7_Ex350_Em668,,");
                double DeltaT = 0.0;
                if (p.EndsWith("\\"))
                {
                    p.TrimEnd('\\');
                }
                for (int i = 0; i <= n + 1; i++)
                {
                    using (System.IO.StreamReader FileVis = new System.IO.StreamReader(p + "\\vis" + i.ToString() + ".csv"))
                    {
                        FileVis.ReadLine();
                        FileVis.ReadLine();
                        while (!FileVis.EndOfStream)
                        {
                            var line = FileVis.ReadLine();
                            var values = line.Split(',');
                            if (values[0] == "")
                            {
                                break;
                            }
                            double t1 = Convert.ToDouble(values[0]);
                            double t2 = Convert.ToDouble(values[2]);
                            double t3 = Convert.ToDouble(values[4]);
                            t1 = t1 + DeltaT;
                            t2 = t2 + DeltaT;
                            t3 = t3 + DeltaT;
                            Output.Write(t1.ToString() + ",");
                            Output.Write(values[1] + ",");
                            Output.Write(t2.ToString() + ",");
                            Output.Write(values[3] + ",");
                            Output.Write(t3.ToString() + ",");
                            Output.Write(values[5] + ",\n");
                        }
                    }

                    DeltaT += 2.0;
                    if (i != 0 && i != n + 1)
                    {
                        using (System.IO.StreamReader FileUV = new System.IO.StreamReader(p + "\\uv" + i.ToString() + ".csv"))
                        {
                            FileUV.ReadLine();
                            FileUV.ReadLine();
                            while(!FileUV.EndOfStream)
                            {
                                var line = FileUV.ReadLine();
                                string[] values = line.Split(',');
                                if (values[0] == "")
                                {
                                    break;
                                }
                                double t1 = Convert.ToDouble(values[0]);
                                double t2 = Convert.ToDouble(values[2]);
                                double t3 = Convert.ToDouble(values[4]);
                                t1 = t1 + DeltaT;
                                t2 = t2 + DeltaT;
                                t3 = t3 + DeltaT;
                                Output.Write(t1.ToString() + ",");
                                Output.Write(values[1] + ",");
                                Output.Write(t2.ToString() + ",");
                                Output.Write(values[3] + ",");
                                Output.Write(t3.ToString() + ",");
                                Output.Write(values[5] + ",\n");
                            }
                        }
                        DeltaT += 10.0;
                    }
                }
            
            }
            
        }
           
    }
}
