using CodoSchool.Data.Abstractions;
using CodoSchool.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodoSchool.Data.Repositories.EFRepositories
{
    public class EFQuestionsRepository : EFRepository<Question>, IQuestionsRepository
    {
        public EFQuestionsRepository(ApplicationDbContext context) : base(context)
        {
        }

        public Question GetAnswersIncluded(int id)
        {
            return Context.Set<Question>().Include(x => x.Answers).SingleOrDefault(x => x.Id == id);
        }
    }
}
