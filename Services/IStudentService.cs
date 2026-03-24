namespace StudentApp.Services;
using StudentApp.Models;

public interface IStudentService
{
    Student Create(string name, string email, string? enrollmentDate = null);
    bool UpdateEmail(int studentId, string newEmail);
    bool Delete(int studentId);
    Student? Get(int studentId);
    IReadOnlyList<Student> ListStudentInfo(int studentId); 
    IReadOnlyList<Student> ListAll();
    bool AddCourse(int studentId, string courseCode);
}
