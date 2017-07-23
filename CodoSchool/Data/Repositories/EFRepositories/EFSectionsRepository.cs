using CodoSchool.Data.Abstractions;
using CodoSchool.EntityBase.Extensions;
using CodoSchool.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodoSchool.Data.Repositories.EFRepositories
{
    public class EFSectionsRepository : EFRecursiveRepository<Section>, ISectionsRepository
    {
        public EFSectionsRepository(ApplicationDbContext context) : base(context)
        {
        }

        //Implementation must be improved (issue with ordering and flattening hierarchies)
        public IEnumerable<Section> GetAllSections()
        {
            IEnumerable<Section> sections = Context.Set<Section>().Include(x => x.SectionType).ToList();
            IEnumerable<Section> orderedHierarchy = sections.OrderHierarchyBy(x => x.Name);
            IEnumerable<Section> rootSections = orderedHierarchy.Where(x => x.ParentId == null);
            return rootSections.FlattenHierarchy();
        }

        //Implementation must be improved (issue with ordering hierarchies)
        public IEnumerable<Section> GetCategoriesAndCourses()
        {
            List<Section> sections = Context.Set<Section>().Where(x=>x.SectionTypeId==SectionType.Category || x.SectionTypeId==SectionType.Course).Include(x=>x.SectionType).OrderHierarchyBy(x => x.Name).ToList();
            return sections.Where(x => x.ParentId == null);
        }

        public IEnumerable<Section> GetCourseLessons(int courseId)
        {
            Section section = Context.Set<Section>().Find(courseId);
            if (section == null || section.SectionTypeId != SectionType.Course)
                return null;
            return Context.Set<Section>().Include(x=>x.SectionType).ToList().Where(x => x.ParentId == section.Id);
        }

        public IEnumerable<Question> GetQuestions(int quizID)
        {
            var quizSection = Context.Set<Section>().Find(quizID);
            var questions = Context.Set<Question>().Where(x => x.SectionId == quizSection.Id).Include(x => x.Answers);

            return questions;
        }


    }
}
