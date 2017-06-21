namespace RogueMetalicana.Menu
{
    using System;
    using System.Collections.Generic;
    using RogueMetalicana.Pagination;
    using RogueMetalicana.LevelCreator;
    using RogueMetalicana.Constants.Potions;
    using RogueMetalicana.Visualization;
    using RogueMetalicana.Potion;
    using RogueMetalicana.GameEngine;
    public class Menu
    {
        public static void StartMenu()
        {
            List<string> options = new List<string>();
            options.Add("New Game");
            options.Add("Create Level"); 
            options.Add("Options");
            


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
    }
}
