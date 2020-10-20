using DomainModel;
using Mm.DataAccessLayer;
using System.Collections.Generic;

namespace Mm.BusinessLayer
{
    public class BuinessLayer : IBusinessLayer
    {
        private readonly ITeacherRepository _teacherRepository;
        private readonly ICourseRepository _courseRepository;

        public BuinessLayer()
        {
            _teacherRepository = new TeacherRepository();
            _courseRepository = new CourseRepository();
            _standardRepository = new StandardRepository();
            _studentRepository = new StudentRepository();
        }

        public BuinessLayer(ITeacherRepository teacherRepository, ICourseRepository courseRepository)
        {
            _teacherRepository = teacherRepository;
            _courseRepository = courseRepository;
            _standardRepository = new StandardRepository();
            _studentRepository = new StudentRepository();
        }

        //CRUD for teachers

        public IList<Teacher> GetAllTeachers()
        {
            return _teacherRepository.GetAll();
        }

        public Teacher GetTeacherByName(string teacherName)
        {
            return _teacherRepository.GetSingle(
                d => d.TeacherName.Equals(teacherName),
                d => d.Courses); //include related Courses
        }

        public Teacher GetTeacherById(int teacherID)
        {
            return _teacherRepository.GetSingle(
                d => d.TeacherId.Equals(teacherID),
                d => d.Courses); //include related Courses
        }

        public void AddTeacher(params Teacher[] teachers)
        {
            _teacherRepository.Add(teachers);
        }

        public void UpdateTeacher(params Teacher[] teachers)
        {
            _teacherRepository.Update(teachers);
        }

        public void RemoveTeacher(params Teacher[] teachers)
        {
            _teacherRepository.Remove(teachers);
        }

        public IList<Course> GetCoursesByTeacherId(int teacherId)
        {
            return _courseRepository.GetList(c => c.Teacher.TeacherId.Equals(teacherId));
        }

        //CRUD for courses

        public IList<Course> GetAllCourses()
        {
            return _courseRepository.GetAll();
        }

        public Course GetCourseByName(string courseName)
        {
            return _courseRepository.GetSingle(
                d => d.CourseName.Equals(courseName),
                d => d.Teacher); //include related teacher
        }

        public Course GetCourseById(int courseId)
        {
            return _courseRepository.GetSingle(
                d => d.CourseId.Equals(courseId),
                d => d.Teacher); //include related teacher
        }

        public void AddCourse(params Course[] courses)
        {
            _courseRepository.Add(courses);
        }

        public void UpdateCourse(params Course[] courses)
        {
            _courseRepository.Update(courses);
        }

        public void RemoveCourse(params Course[] courses)
        {
            _courseRepository.Remove(courses);
        }

        //NEED TO IMPLEMENT THESE 
        IList<Standard> IBusinessLayer.getAllStandards()
        {
            throw new System.NotImplementedException();
        }

        Standard IBusinessLayer.GetStandardByID(int id)
        {
            throw new System.NotImplementedException();
        }

        void IBusinessLayer.addStandard(Standard s)
        {
            throw new System.NotImplementedException();
        }

        void IBusinessLayer.updateStandard(Standard s)
        {
            throw new System.NotImplementedException();
        }

        void IBusinessLayer.removeStandard(Standard s)
        {
            throw new System.NotImplementedException();
        }

        IList<Student> IBusinessLayer.getAllStudents()
        {
            throw new System.NotImplementedException();
        }

        Student IBusinessLayer.GetStudentByID(int id)
        {
            throw new System.NotImplementedException();
        }

        void IBusinessLayer.addStudent(Student s)
        {
            throw new System.NotImplementedException();
        }

        void IBusinessLayer.UpdateStudent(Student s)
        {
            throw new System.NotImplementedException();
        }

        void IBusinessLayer.RemoveStudent(Student s)
        {
            throw new System.NotImplementedException();
        }

        private readonly IStandardRepository _standardRepository;
        private readonly IStudentRepository _studentRepository;

        //Implement other methods
    }
}