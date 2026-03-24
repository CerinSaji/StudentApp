namespace StudentApp.Services;
using StudentApp.Models;
using StudentApp.Repositories;

public class CourseService : ICourseService
{
    private readonly ICourseRepository _repo;
    public CourseService(ICourseRepository repo) => _repo = repo;

    public Course Create(string name, string code, int credits, string instructor)
    {
        var course = new Course(name, code, credits, instructor);
        return _repo.Add(course);
    }

    public bool UpdateInstructor(string courseCode, string newInstructor)
    {
        var existing = _repo.Get(courseCode);
        if (existing == null) return false;
        var updated = new Course(existing.CourseName, existing.CourseCode, existing.CourseCredits, newInstructor);
        return _repo.Update(updated);
    }

    public bool UpdateCredits(string courseCode, int newCredits)
    {
        var existing = _repo.Get(courseCode);
        if (existing == null) return false;
        var updated = new Course(existing.CourseName, existing.CourseCode, newCredits, existing.CourseInstructor);
        return _repo.Update(updated);
    }

    public bool Delete(string courseCode) => _repo.Remove(courseCode);
    public Course? Get(string courseCode) => _repo.Get(courseCode);
    public IReadOnlyList<Course> ListAll() => _repo.GetAll();
}
