/*****************************************************************************
*   Project:        FileMoveEx
*
*   File:			ShortcunHelper.cs
*
*   Developed by:   zjeces Technology.
*                   Hangzhou, China
*
*   Developers:     Lin Binbin   2013/8/12 10:51:49
*
*   Copyright:      (c) 2013 zjeces Technology. All rights reserved.
*****************************************************************************/

using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using IWshRuntimeLibrary;
using System.Runtime.InteropServices;
using System.IO;
namespace FileMoveEx
{
    internal class ShortcutHelper
    {
        public static void CreateShortcut(string location,string args)
        {
            try
            {
                //实例化WshShell对象 
                WshShell shell = new WshShell();

                //通过该对象的 CreateShortcut 方法来创建 IWshShortcut 接口的实例对象 
                IWshShortcut shortcut = (IWshShortcut)shell.CreateShortcut(Path.Combine(location,
                    Path.GetFileNameWithoutExtension(System.Reflection.Assembly.GetExecutingAssembly().Location) + ".lnk"));

                //设置快捷方式的目标所在的位置(源程序完整路径) 
                shortcut.TargetPath = System.Reflection.Assembly.GetExecutingAssembly().Location;

                //应用程序的工作目录 
                //当用户没有指定一个具体的目录时，快捷方式的目标应用程序将使用该属性所指定的目录来装载或保存文件。 
                shortcut.WorkingDirectory = System.Environment.CurrentDirectory;

                //目标应用程序窗口类型(1.Normal window普通窗口,3.Maximized最大化窗口,7.Minimized最小化) 
                shortcut.WindowStyle = 1;

                //快捷方式的描述 
                shortcut.Description = shortcut.TargetPath;

                //可以自定义快捷方式图标.(如果不设置,则将默认源文件图标.) 
                //shortcut.IconLocation = System.Environment.SystemDirectory + "\\" + "shell32.dll, 165"; 

                //设置应用程序的启动参数(如果应用程序支持的话) 
                shortcut.Arguments = args;

                //设置快捷键(如果有必要的话.) 
                //shortcut.Hotkey = "CTRL+ALT+D"; 

                //保存快捷方式 
                shortcut.Save(); 
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
                //throw;
            }
           
        }

    }
}
