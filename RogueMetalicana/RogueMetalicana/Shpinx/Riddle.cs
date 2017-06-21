

namespace RogueMetalicana.Shpinx
{
    using System;
    using System.Collections.Generic;
    using System.Linq;
    using System.Text;
    using System.Threading.Tasks;
    using System.IO;
    using RogueMetalicana.Pagination;
    class Riddle
    {
        private string question;
        private List<string> answers;
        private string rightAnswer;

        
      
        public Riddle(string question,List<string> answers,string rightanswer)
        {
            this.answers = answers;
            this.question = question;
            this.rightAnswer = rightAnswer;
        }
        public string Question
        {
            get { return this.question; }
            set { this.question = value; }
        }

        public List<string> Answers
        {
            get { return this.answers; }
            set { this.answers = value; }
        }

        public string RightAnswer
        {
            get { return this.rightAnswer; }
            set { this.rightAnswer = value; }
        }
        public static void GenerateQuestion()
        {
            LoadQuestion();
            SelectAnswer();
        }
        public static Riddle LoadQuestion()
        {
            var directory = @"../../Questions/1.txt";
            string[] lines = File.ReadAllLines(directory);
            var question = lines[1];
            var rightAnswer = lines[lines.Length - 1];
            var answers = new List<string>();

            for (int i = 1; i < lines.Length-1; i++)
            {
                answers.Add(lines[i]);
            }

            var riddle = new Riddle(question,answers,rightAnswer);
            return riddle;    
        }
        public static void SelectAnswer()
        {
            Pagination sphinxPagination = new Pagination(LoadQuestion().Answers);
            sphinxPagination.Paginate();
            if (sphinxPagination.ReturnResult()==LoadQuestion().RightAnswer)
            {

            }
            else
            {

            }
        }
        
        
    }
}
