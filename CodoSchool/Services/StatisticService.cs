using CodoSchool.Data.Abstractions;
using CodoSchool.Models;
using System.Collections.Generic;
using System.Linq;

namespace CodoSchool.Services
{
    public class StatisticService
    {
        IUnitOfWork _context;
        public StatisticService(IUnitOfWork context)
        {
            _context = context;
        }

        public List<CourseProgressViewModel> GetStudentProgress(string studentId)
        {
            List<Section> studentQuizes = _context.StudentProgress.GetStudentQuizes(studentId).ToList();
            List<int> studentQuizesId = studentQuizes?.Select(x=>x.Id).ToList();
            List<Section> quizesIncluded = _context.Sections.GetAllSections().Where(x => x.SectionTypeId == SectionType.Quiz).ToList();
            List<Section> courses = quizesIncluded?.Select(x => x.Parent.Parent).Distinct().ToList();

            List<CourseProgressViewModel> coursesProgress = new List<CourseProgressViewModel>();
            foreach (var course in courses)
            {
                List<Section> courseTotalQuizes = quizesIncluded.Where(x => x.Parent.ParentId == course.Id).ToList();
                List<Section> coursePassedQuizes = studentQuizes.Intersect(courseTotalQuizes).ToList();

                int courseTotalQuizesCount = courseTotalQuizes.Count;
                int coursePassedQuizessCount = coursePassedQuizes.Count;
                int courseProgress = (int)((double)coursePassedQuizessCount / courseTotalQuizesCount * 100 + 0.5);
                if(courseProgress>0)
                    coursesProgress.Add(new CourseProgressViewModel { CourseName = course.Name, CourseProgress = courseProgress });
            }
            return coursesProgress;
        }
    }
}
