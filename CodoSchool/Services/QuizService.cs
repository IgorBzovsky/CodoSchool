﻿using AutoMapper;
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

        public object GetQuizResult(int id,int userId , int [] answers)
        {
            var questions = _context.Sections.GetQuestions(id)  as Question [];
            int correctAnswersCount = 0;
            for (int i = 0; i < questions.Count(); i++)
            {
                if (questions[i].Answers[answers[i]].IsCorrect == true)
                {
                    correctAnswersCount++;
                }
            }
            int grade = (correctAnswersCount / questions.Count() * 100);
            bool quizCompleted = grade >= 75;
            if (quizCompleted)
            {
                var _studentProgress = _context.StudentProgress.Find(x => x.SectionId == id && x.ApplicationUserId == userId.ToString()).FirstOrDefault();
                if (_studentProgress != null)
                {
                    _studentProgress.Completed = quizCompleted;
                }
                else
                {
                    _context.StudentProgress.Add(new StudentProgress { ApplicationUserId = userId.ToString(), SectionId = id });
                }
                _context.Complete();
            }
            

             
            return new int [correctAnswersCount, questions.Count(), grade];
        }

        

        public SectionDto GetQuiz(int id)
        {
            return _mapper.Map<Section, SectionDto>(_context.Sections.GetSection(id));
        }
            

    }
}
