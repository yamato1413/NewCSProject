using System;
using System.IO;
using System.Text;

namespace newcs
{
    public class Program
    {
        public static void Main(string[] args)
        {
            string projectName = (args.Length == 0) ? "newCSProject" : args[0];
            string desktopPath = Environment.GetFolderPath(Environment.SpecialFolder.Desktop);
            string newProjectPath = Directory.CreateDirectory(desktopPath + @"\" + projectName).FullName;

            StreamWriter buildbat = new StreamWriter(
                newProjectPath + @"\build.bat",
                false,
                Encoding.GetEncoding("shift_jis")
            );

            buildbat.WriteLine(@"
@echo off
set PATH=C:\Windows\Microsoft.NET\Framework64\v4.0.30319;%PATH%

csc -out:{0}.exe %~dp0\*.cs 

if %ERRORLEVEL% equ 0 (
    echo コンパイル完了.
    .\{0}.exe
)
pause"
            , projectName);
            buildbat.Close();

            StreamWriter Maincs = new StreamWriter(
                newProjectPath + @"\Main.cs",
                false,
                Encoding.UTF8
            );
            Maincs.WriteLine(@"
using System;

namespace {0}
{{
    public class Program 
    {{
        public static void Main(string[] args)
        {{
            
        }}
    }}
}}"
            , projectName);
            Maincs.Close();

            Console.WriteLine("作成完了");
        }
    }
}