namespace StudentApp.Repositories;
using StudentApp.Models;

public interface IEnrollmentRepository
{
    Enrollment Add(Enrollment enrollment);
    Enrollment? Get(int studentId, string courseCode);
    IReadOnlyList<Enrollment> GetAll();
}
