using CodoSchool.Data.Abstractions;
using CodoSchool.Data.Repositories.EFRepositories;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodoSchool.Data
{
    public class UnitOfWork : IUnitOfWork
    {
        private readonly ApplicationDbContext _context;
        public UnitOfWork(ApplicationDbContext context)
        {
            _context = context;
            Sections = new EFSectionsRepository(_context);
            SectionTypes = new EFSectionTypesRepository(_context);
        }
        public ISectionsRepository Sections { get; private set; }

        public ISectionTypesRepository SectionTypes { get; private set; }


        public int Complete()
        {
            return _context.SaveChanges();
        }

        public void Dispose()
        {
            _context.Dispose();
        }
    }
}
