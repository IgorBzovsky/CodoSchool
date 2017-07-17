using CodoSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodoSchool.Data.Abstractions
{
    public interface IQuestionsRepository : IRepository<Question>
    {
        Question GetAnswersIncluded(int id);
    }
}
