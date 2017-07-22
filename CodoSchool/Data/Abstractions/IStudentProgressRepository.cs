using CodoSchool.Models;
using System.Collections.Generic;

namespace CodoSchool.Data.Abstractions
{
    public interface IStudentProgressRepository : IRepository<StudentProgress>
    {
        IEnumerable<Section> GetStudentQuizes(string studentId);
    }
}
