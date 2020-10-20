using DomainModel;
using System.Collections.Generic;

namespace Mm.BusinessLayer
{
    public interface IBusinessLayer
    {
        IList<Teacher> GetAllTeachers();
        Teacher GetTeacherByName(string teacherName);
        Teacher GetTeacherById(int teacherID);
        void AddTeacher(params Teacher[] teachers);
        void UpdateTeacher(params Teacher[] teachers);
        void RemoveTeacher(params Teacher[] teachers);

        IList<Course> GetCoursesByTeacherId(int teacherId);

        IList<Course> GetAllCourses();
        Course GetCourseByName(string courseName);
        Course GetCourseById(int courseId);
        void AddCourse(params Course[] courses);
        void UpdateCourse(params Course[] courses);
        void RemoveCourse(params Course[] courses);

        IList<Standard> getAllStandards();
        Standard GetStandardByID(int id);
        void addStandard(Standard s);
        void updateStandard(Standard s);
        void removeStandard(Standard s);
        IList<Student> getAllStudents();
        Student GetStudentByID(int id);
        void addStudent(Student s);
        void UpdateStudent(Student s);
        void RemoveStudent(Student s);
    }
}