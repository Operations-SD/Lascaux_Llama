using System;
using System.Reflection.Emit;
using IntelChat.Models;

namespace IntelChat.Services
{
    public class QuestionService
    {
        public Question _currentQuestion;
        public String Label;

        public Question GetCurrentQuestion()
        {
            return _currentQuestion;
        }

        public void SendCurrentQuestion(Question question)
        {
            _currentQuestion = question;
            Console.WriteLine($"Sent QuestioDHSJDHSJDHSJHDJSHDJSHDJSHDJSJS: {_currentQuestion.QuestionText} to the service.");



        }

        public void SendLabel(String label)
        {
            Label = label;



        }
    }
}

