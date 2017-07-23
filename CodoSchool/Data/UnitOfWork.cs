using CodoSchool.Data.Abstractions;
using CodoSchool.Data.Repositories.EFRepositories;

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
            Questions = new EFQuestionsRepository(_context);
            Answers = new EFAnswersRepository(_context);
            StudentProgress = new EFStudentProgressRepository(_context);
        }
        public ISectionsRepository Sections { get; private set; }
        public ISectionTypesRepository SectionTypes { get; private set; }
        public IQuestionsRepository Questions { get; private set; }
        public IAnswersRepository Answers { get; private set; }
        public IStudentProgressRepository StudentProgress { get; private set; }


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
