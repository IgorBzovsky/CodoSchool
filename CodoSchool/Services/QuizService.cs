using AutoMapper;
using CodoSchool.Data;
using CodoSchool.Data.Abstractions;
using CodoSchool.Models;
using CodoSchool.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.EntityFrameworkCore;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CodoSchool.Models.DTOs;


namespace CodoSchool.Services
{
    public class QuizService
    {
        private readonly IUnitOfWork _context;
        private readonly IMapper _mapper;
        public QuizService(IUnitOfWork context, IMapper mapper)
        {
            _context = context;
            _mapper = mapper;
        }

        public QuestionDto ProvideQuestion(int quizID, int questionIndex)
        {
            if (questionIndex < 0)
                questionIndex = 0;
            var question = _context.Sections.GetQuestions(quizID).ToList()[questionIndex];

            return _mapper.Map<Question, QuestionDto>(question);
        }

        public int GetQuestionsCount(int quizID)
        {

            return _context.Sections.GetQuestions(quizID).Count();

        }

        public List<object> GetQuizResult(int id, string userId, int[] answers)
        {
            var questions = _context.Sections.GetQuestions(id).ToList();
            int correctAnswersCount = 0;

            for (int i = 0; i < answers.Length; i++)
            {

                if (questions[i].Answers[answers[i]].IsCorrect == true)
                {
                    correctAnswersCount++;
                }
            }
            
            float grade = ((float)correctAnswersCount / (float)questions.Count() * 100);
            bool quizCompleted = grade >= 75;
            if (quizCompleted)
            {
                var _studentProgress = _context.StudentProgress.Find(x => x.SectionId == id && x.ApplicationUserId == userId).FirstOrDefault();
                if (_studentProgress != null)
                {
                    _studentProgress.Completed = quizCompleted;
                }
                else
                {
                    _context.StudentProgress.Add(new StudentProgress { ApplicationUserId = userId, SectionId = id });
                }
                _context.Complete();
            }



            return new List<object> { correctAnswersCount, questions.Count(), grade };
        }



        public SectionDto GetQuiz(int id)
        {
            return _mapper.Map<Section, SectionDto>(_context.Sections.GetSection(id));
        }


    }
}
