

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
        private string selectedAnswer;
        //public bool isItRight;


        public  void LoadQuestion()
        {
            var directory = @"../../Questions/1.txt";
            string[] lines = File.ReadAllLines(directory);
            var question = lines[1];
            var rightAnswer = lines[lines.Length - 1];
            this.rightAnswer = rightAnswer;
            var answers = new List<string>();

            for (int i = 0; i < lines.Length - 1; i++)
            {
                answers.Add(lines[i]);
            }
            Pagination sphinxPagination = new Pagination(answers,1);
            sphinxPagination.Paginate();
            this.selectedAnswer = sphinxPagination.ReturnResult();
           
        }
        public  bool IsItRight()
        {
            if (this.selectedAnswer==this.rightAnswer)
            {
                return true;

            }
            else
            {
                return false;
            }
        }

    }
}