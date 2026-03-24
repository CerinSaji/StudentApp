namespace StudentApp.Repositories;
using StudentApp.Models;

public class InMemoryEnrollmentRepository : IEnrollmentRepository
{
    private readonly List<Enrollment> _store = new();

    public Enrollment Add(Enrollment enrollment)
    {
        _store.Add(enrollment);
        return enrollment;
    }

    public Enrollment? Get(int studentId, string courseCode) => _store.FirstOrDefault(e => e.StudentId == studentId && e.CourseCode == courseCode);

    public IReadOnlyList<Enrollment> GetAll() => _store.ToList();  
}
