
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
            public Pagination(List<string> options)
            {
                this.options = options;
            }

            public void Paginate()
            {
             Console.Clear();

                int pointer = 0;
                string key = "";
                while (key != "Enter")
                {
                    ConsoleManager.DefaultColors();
                    Console.Clear();

                    for (int i = 0; i < (Console.WindowHeight - options.Count()) / 2; i++)
                    {
                        Console.WriteLine();
                    }
                    foreach (var item in options)
                    {
                     ConsoleManager.DefaultColors();
                    if (pointer == options.IndexOf(item))
                        {
                        ConsoleManager.SelectedOptionColor();
                        }
                        Console.WriteLine(new string(' ', (Console.WindowWidth - item.Length) / 2) + item + new string(' ', (Console.WindowWidth - item.Length) / 2));
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
                        pointer = 0;
                    }
                    if (pointer < 0)
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

