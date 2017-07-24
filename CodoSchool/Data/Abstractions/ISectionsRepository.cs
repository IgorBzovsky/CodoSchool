using CodoSchool.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodoSchool.Data.Abstractions
{
    public interface ISectionsRepository : IRepository<Section>
    {
        IEnumerable<Section> GetAllSections();
        IEnumerable<Section> GetCategoriesAndCourses();
        IEnumerable<Section> GetCourseLessons(int id);
        IEnumerable<Section> GetParents(Section section);
        Section GetSectionIncluded(int id);
        IEnumerable<Question> GetQuestions(int quizID);
        Section GetSection(int id);
    }
}
