namespace RogueMetalicana.Menu
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using RogueMetalicana.Pagination;
    using RogueMetalicana.LevelCreator;
    using RogueMetalicana.PlayerUnit;
    using RogueMetalicana.Potion;
    using RogueMetalicana.Constants.Potions;
    public class Menu
    {
        public static void StartMenu()
        {
            List<string> options = new List<string>();
            options.Add("New Game");
            options.Add("Load Game");
            options.Add("Create Level"); 
            options.Add("Options");
            
            options.Add("Exit");


            Pagination menuPagination = new Pagination(options);
            menuPagination.Paginate();

            switch (menuPagination.ReturnResult())
            {
                case "New Game": break;
                case "Load Game":LoadMenu(); break;
                case "Create Level": LevelCreator.CreateLevel(); Menu.StartMenu(); break;
                case "Options":  break;
              
                case "Exit": Environment.Exit(0); break;
            }
        }
        public static void LoadMenu()
        {
            List<string> options = new List<string>();
            options.Add("Story Mode");
            options.Add("Custom Levels");
           
            Pagination menuPagination = new Pagination(options);
            menuPagination.Paginate();

            switch (menuPagination.ReturnResult())
            {
                case "Story Mode": break;
                case "Custom Levels": break;             
            }
        }
        public static void OpenShop()
        {
            var values = Enum.GetValues(typeof(PotionType));
            var options = new List<string>();
            foreach (var value in values)
            {
                options.Add(value.ToString());
            }
            options.RemoveAt(options.Count - 1);

            Pagination menuPagination = new Pagination(options);
            menuPagination.Paginate();

            switch (menuPagination.ReturnResult())
            {
                case "HealthPotion":  break;
                case "XpPotion": break;
                case "BonusDamagePotion": break;
            }
        }
    }
}
