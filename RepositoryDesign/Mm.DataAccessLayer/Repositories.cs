using DomainModel;

namespace DataAccessLayer
{
    public interface ITeacherRepository : IGenericDataRepository<Teacher>
    {
    }

    public interface ICourseRepository : IGenericDataRepository<Course>
    {
    }

    public class TeacherRepository : GenericDataRepository<Teacher>, ITeacherRepository
    {
    }

    public class CourseRepository : GenericDataRepository<Course>, ICourseRepository
    {
    }
}
