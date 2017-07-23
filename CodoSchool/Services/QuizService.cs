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
            var question = _context.Sections.GetQuestions(quizID).ToList()[questionIndex];

            return _mapper.Map<Question, QuestionDto>(question);
        }

        public int GetQuestionsCount(int quizID)
        {

            return _context.Sections.GetQuestions(quizID).Count();
            
        }
    }
}
