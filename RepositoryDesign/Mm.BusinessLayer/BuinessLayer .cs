using DomainModel;
using DataAccessLayer;
using System.Collections.Generic;

namespace BusinessLayer
{
    public class BuinessLayer : IBusinessLayer
    {
        // Private members used for object construction
        private readonly ITeacherRepository _teacherRepository;
        private readonly ICourseRepository _courseRepository;

        // Base Constructor
        public BuinessLayer()
        {
            _teacherRepository = new TeacherRepository();
            _courseRepository = new CourseRepository();
        }
        // Overloaded Constructor
        public BuinessLayer(ITeacherRepository teacherRepository, ICourseRepository courseRepository)
        {
            _teacherRepository = teacherRepository;
            _courseRepository = courseRepository;
        }

        //CRUD for teachers

        public IList<Teacher> GetAllTeachers()
        {
            return _teacherRepository.GetAll();
        }

        /// <summary>
        /// Gets the Teacher object's information by name
        /// </summary>
        /// <param name="teacherName">Name of teacher</param>
        /// <returns></returns>
        public Teacher GetTeacherByName(string teacherName)
        {
            return _teacherRepository.GetSingle(
                d => d.TeacherName.Equals(teacherName),
                d => d.Courses); //include related Courses
        }

        /// <summary>
        /// Get's Teacher object's information by ID
        /// </summary>
        /// <param name="teacherID">ID of teacher</param>
        /// <returns></returns>
        public Teacher GetTeacherById(int teacherID)
        {
            return _teacherRepository.GetSingle(
                d => d.TeacherId.Equals(teacherID),
                d => d.Courses); //include related Courses
        }

        /// <summary>
        /// Adds a new teacher(s) to the repository
        /// </summary>
        /// <param name="teachers">Teacher(s) to be added</param>
        public void AddTeacher(params Teacher[] teachers)
        {
            _teacherRepository.Add(teachers);
        }

        /// <summary>
        /// Updates Teacher object
        /// </summary>
        /// <param name="teachers"></param>
        public void UpdateTeacher(params Teacher[] teachers)
        {
            _teacherRepository.Update(teachers);
        }

        /// <summary>
        /// Remove Teacher object
        /// </summary>
        /// <param name="teachers"></param>
        public void RemoveTeacher(params Teacher[] teachers)
        {
            _teacherRepository.Remove(teachers);
        }

        /// <summary>
        /// Gets a list of coursed by a Teacher's ID
        /// </summary>
        /// <param name="teacherId">ID of Teacher</param>
        /// <returns></returns>
        public IList<Course> GetCoursesByTeacherId(int teacherId)
        {
            return _courseRepository.GetList(c => c.Teacher.TeacherId.Equals(teacherId));
        }

        //CRUD for courses

        public IList<Course> GetAllCourses()
        {
            return _courseRepository.GetAll();
        }

        /// <summary>
        /// Get's a Course object's information by name
        /// </summary>
        /// <param name="courseName">Name of course</param>
        /// <returns></returns>
        public Course GetCourseByName(string courseName)
        {
            return _courseRepository.GetSingle(
                d => d.CourseName.Equals(courseName),
                d => d.Teacher); //include related teacher
        }

        /// <summary>
        /// Get's a Course object information by ID
        /// </summary>
        /// <param name="courseId">ID of course</param>
        /// <returns></returns>
        public Course GetCourseById(int courseId)
        {
            return _courseRepository.GetSingle(
                d => d.CourseId.Equals(courseId),
                d => d.Teacher); //include related teacher
        }
        /// <summary>
        /// Adds course(s) to the course repository
        /// </summary>
        /// <param name="courses">Course(s) to be added</param>
        public void AddCourse(params Course[] courses)
        {
            _courseRepository.Add(courses);
        }
        /// <summary>
        /// Update Course information
        /// </summary>
        /// <param name="courses"></param>
        public void UpdateCourse(params Course[] courses)
        {
            _courseRepository.Update(courses);
        }
        /// <summary>
        /// Remove Course from repository
        /// </summary>
        /// <param name="courses"></param>
        public void RemoveCourse(params Course[] courses)
        {
            _courseRepository.Remove(courses);
        }
    }
}