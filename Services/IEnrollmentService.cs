namespace StudentApp.Services;
using StudentApp.Models;

public interface IEnrollmentService
{
    Enrollment Enroll(int studentId, string courseCode);
    Enrollment? GetEnrollment(int studentId, string courseCode);
    IReadOnlyList<Enrollment> ListAll();
}
