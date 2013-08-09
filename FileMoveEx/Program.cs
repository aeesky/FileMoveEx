using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.IO;
using System.Diagnostics;

namespace FileMoveEx
{
    class Program
    {
        //todo 循环等待 
        static string temp = @"D:\temp";
        static void Main(string[] args)
        {
            if (args.Length > 1)
            {
                temp = args[0];
                Debug.Assert(Directory.Exists(temp), "目标文件夹不存在，请确认是否填写正确", "目标文件夹在快捷方式属性窗口的目标栏配置");
                try
                {
                    foreach (var item in args.Skip(1))
                    {
                        //Console.WriteLine(item);
                        if (File.Exists(item))
                        {
                            Console.WriteLine(item + "移动文件=>" + Path.Combine(temp, Path.GetFileName(item)));
                            File.Move(item, Path.Combine(temp, Path.GetFileName(item)));
                        }
                        else if (Directory.Exists(item))
                        {
                            Console.WriteLine(item + "移动目录=>" + temp);
                            //该方法不支持跨分区移动目录
                            //Directory.Move(item,Path.Combine(temp,Path.GetFileName(item)));
                            MoveDirectory(item,temp);
                        }
                    }
                }
                catch (Exception e)
                {
                    Console.WriteLine(e.Message);
                    Console.ReadKey();
                    //throw;
                }
            }
            else
            {
                Console.WriteLine("本程序只能通过快捷方式运行");
                Console.WriteLine("请在快捷方式属性窗口的目标栏中设置要移动到的目标文件夹");
                Console.ReadKey();
            }
#if Debug
             Console.ReadKey();
#endif

        }
        /// <summary>
        /// 移动目录，支持跨分区卷，不同根目录移动
        /// </summary>
        /// <param name="source">源地址</param>
        /// <param name="dest">目标地址</param>
        /// <example> source="E"\test" desc="D:\temp" 移动后的目录为"D:\temp\test"</example>
        public static void MoveDirectory(string source, string dest)
        {
            try
            {
                Console.WriteLine(source + "目录=>" + dest);
                if (!Directory.Exists(dest))
                    Directory.CreateDirectory(dest);
                var subdest = Path.Combine(dest, Path.GetFileName(source));
                if (!Directory.Exists(subdest))
                    Directory.CreateDirectory(subdest);
                foreach (var item in Directory.GetFileSystemEntries(source))
                {
                    Console.WriteLine(item);
                    if (File.Exists(item))
                    {
                        Console.WriteLine("移动文件:" + item + "=>" + Path.Combine(subdest, Path.GetFileName(item)));
                        File.Move(item, Path.Combine(subdest, Path.GetFileName(item)));
                    }
                    else if (Directory.Exists(item))
                    {
                        Console.WriteLine("创建目录:" + Path.Combine(subdest, Path.GetFileName(item)));
                        Directory.CreateDirectory(Path.Combine(subdest, Path.GetFileName(item)));
                        MoveDirectory(item,subdest);
                    }
                }
                Directory.Delete(source);
            }
            catch (Exception)
            {

                throw;
            }
        }
    }

}
