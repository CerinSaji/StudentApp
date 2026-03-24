namespace StudentApp.Services;
using StudentApp.Models;
using StudentApp.Repositories;

public class EnrollmentService : IEnrollmentService
{
    private readonly IEnrollmentRepository _repo;
    public EnrollmentService(IEnrollmentRepository repo) => _repo = repo;

    public Enrollment Enroll(int studentId, string courseCode)
    {
        var enrollment = new Enrollment { StudentId = studentId, CourseCode = courseCode};
        return _repo.Add(enrollment);
    }

    public Enrollment? GetEnrollment(int studentId, string courseCode) => _repo.Get(studentId, courseCode);
    public IReadOnlyList<Enrollment> ListAll() => _repo.GetAll();
}
