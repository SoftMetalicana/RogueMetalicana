
namespace RogueMetalicana.Pagination
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using RogueMetalicana.ConsoleCare;
    using RogueMetalicana.Visualization;

    public class Pagination
        {
            private List<string> options;
            private string selected;
            private int startIndex = 0;
            public Pagination(List<string> options)
            {
                this.options = options;
            }
            public Pagination(List<string> options,int startIndex)
            {
                this.options = options;
                this.startIndex = startIndex;
            }

            public void Paginate()
            {
             Console.Clear();

            options.Add("Exit");
                int pointer = startIndex;
                string key = "";
                while (key != "Enter")
                {
                    ConsoleManager.DefaultColors();
                    Console.Clear();

                    for (int i = 0; i < (Console.WindowHeight - options.Count()) / 2; i++)
                    {
                        Console.WriteLine();
                    }
                for (int i = 0; i < options.Count; i++)              
                    {
                     ConsoleManager.DefaultColors();
                    if (pointer == options.IndexOf(options[i]))
                        {
                        ConsoleManager.SelectedOptionColor();
                        }
                        Console.WriteLine(new string(' ', (Console.WindowWidth - options[i].Length) / 2) + options[i] + new string(' ', (Console.WindowWidth - options[i].Length) / 2));
                        selected = options[pointer];
                   }
                    key = Console.ReadKey().Key.ToString();

                    switch (key)
                    {
                        case "UpArrow": pointer--; break;
                        case "DownArrow": pointer++; break;
                        case "Enter": ReturnResult(); break;
                    case "Escape":
                        ;break;
                }
                    if (pointer > options.Count - 1)
                    {
                        pointer = startIndex;
                    }
                    if (pointer < startIndex)
                    {
                        pointer = options.Count - 1;
                    }
                ConsoleManager.DefaultColors();

            }

        }

            public string ReturnResult()
            {
                return selected;
            }
        }
   }

