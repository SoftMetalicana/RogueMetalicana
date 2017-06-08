namespace RogueMetalicana.Menu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using RogueMetalicana.Pagination;
    public class Menu
    {
        public static void StartMenu()
        {
            List<string> options = new List<string>();
            options.Add("New Game");
            options.Add("Load Game");
            options.Add("Options");
            options.Add("Exit");


            Pagination menuPagination = new Pagination(options);
            menuPagination.Paginate();

            switch (menuPagination.ReturnResult())
            {
                case "New Game": break;
                case "Load Game": break;
                case "Options": break;
                case "Exit": Environment.Exit(0); break;
            }
        }
    }
}
