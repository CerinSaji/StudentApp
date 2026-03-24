namespace StudentApp.Services;
using StudentApp.Models;
using StudentApp.Repositories;

public class StudentService : IStudentService
{
    private readonly IStudentRepository _repo;
    public StudentService(IStudentRepository repo) => _repo = repo;

    public Student Create(string name, string email, string? enrollmentDate = null)
    {
        var student = new Student(0, name, email, enrollmentDate);
        return _repo.Add(student);
    }

    public bool UpdateEmail(int studentId, string newEmail)
    {
        var existing = _repo.Get(studentId);
        if (existing == null) return false;
        var updated = new Student(existing.StudentId, existing.StudentName, newEmail, existing.EnrollmentDate);
        return _repo.Update(updated);
    }

    public bool Delete(int studentId) => _repo.Remove(studentId);
    public Student? Get(int studentId) => _repo.Get(studentId);
    public IReadOnlyList<Student> ListStudentInfo(int studentId) => _repo.GetAll().Where(s => s.StudentId == studentId).ToList();
    public IReadOnlyList<Student> ListAll() => _repo.GetAll();

    public bool AddCourse(int studentId, string courseCode)
    {
        var student = _repo.Get(studentId);
        if (student == null) return false;
        student.Courses.Add(courseCode);
        return _repo.Update(student);
    }
}
