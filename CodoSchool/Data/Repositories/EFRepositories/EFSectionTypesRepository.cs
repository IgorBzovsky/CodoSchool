using CodoSchool.Data.Abstractions;
using CodoSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodoSchool.Data.Repositories.EFRepositories
{
    public class SectionTypesEFRepository : EFRepository<SectionType>, ISectionTypesRepository
    {
        public SectionTypesEFRepository(ApplicationDbContext context) : base(context)
        {
        }
    }
}
