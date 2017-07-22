using System;
using System.Collections.Generic;
using CodoSchool.Data.Abstractions;
using CodoSchool.Models;
using System.Linq;
using Microsoft.EntityFrameworkCore;

namespace CodoSchool.Data.Repositories.EFRepositories
{
    public class EFStudentProgressRepository : EFRepository<StudentProgress>, IStudentProgressRepository
    {
        public EFStudentProgressRepository(ApplicationDbContext context) : base(context)
        {
        }

        public IEnumerable<Section> GetStudentQuizes(string studentId)
        {
            return Context.Set<StudentProgress>().Where(x => x.ApplicationUserId == studentId).Select(x => x.Section).ToList();
        }
    }
}
