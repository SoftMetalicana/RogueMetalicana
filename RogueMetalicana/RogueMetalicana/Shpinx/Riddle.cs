

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

        private  string SelectQuestion()
        {
            DirectoryInfo d = new DirectoryInfo(@"../../Questions");
            FileInfo[] Files = d.GetFiles("*.txt");
            Random rnd = new Random();
            var fileIndex = rnd.Next(0, Files.Length - 1);
            return Files[fileIndex].ToString();
        }
        public  void LoadQuestion()
        {
            var directory = $@"../../Questions/{SelectQuestion()}";
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
            else if (this.selectedAnswer=="Exit")
            {
                return false;
            }
            else
            {
                return false;
            }
        }

    }
}