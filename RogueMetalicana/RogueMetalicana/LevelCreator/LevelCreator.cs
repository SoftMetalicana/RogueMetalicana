
namespace RogueMetalicana.LevelCreator
{
    using System;
    using System.Collections.Generic;
    using System.IO;
    using System.Linq;
    using System.Text;
    using System.Diagnostics;
    using System.Threading;

    class LevelCreator
    {
        
        private  static void CreateFile(string levelName)
        {
            string patternPath = @"..\..\CustomLevels\pattern.txt";
            string fileName = levelName;
            string extension = $".txt";
            string folder = @"..\..\CustomLevels\";

            StringBuilder fullPath = new StringBuilder();
            fullPath.Append(folder);
            fullPath.Append(fileName);
            fullPath.Append(extension);
            fullPath.ToString();


            File.Copy(patternPath, fullPath.ToString());
            OpenFile(fullPath.ToString());
        } 
        private static void OpenFile(string fullPath)
        {
            Process openFile = Process.Start(fullPath);
            while (openFile.HasExited==false)
            {
                Thread.SpinWait(1);

            }
          
        }
        public static void CreateLevel()
        {
            CreateFile(ChooseName());

        }
        private static  string ChooseName()
        {
            var LevelsNames = new HashSet<string>(Directory.GetFiles(@"..\..\CustomLevels", "*.txt").Select(Path.GetFileName));
            while (true)
            {
                Console.Clear();
                Console.CursorVisible = true;
                Console.Write("Choose Level Name: ");
                string levelName = Console.ReadLine();
                if (LevelsNames.Contains($"{levelName}.txt"))
                {
                    Console.WriteLine("This name is busy");
                    Thread.Sleep(500);
                }
                else
                {
                    return levelName;
                }
            }
         
        }
            


    }
   
}
