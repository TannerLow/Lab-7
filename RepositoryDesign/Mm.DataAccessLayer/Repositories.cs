using DomainModel;

namespace Mm.DataAccessLayer
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

    public interface IStandardRepository : IRepository<Standard>
    {
    }

    public interface IStudentRepository : IRepository<Student>
    {
    }

    public class StandardRepository : Repository<Standard>, IStandardRepository
    { 
    }

    public class StudentRepository : Repository<Student>, IStudentRepository
    {

        public StudentRepository()
        : base(new SchoolDBEntities())
        {
        }
    }
}
