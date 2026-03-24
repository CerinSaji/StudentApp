namespace StudentApp.Repositories;
using StudentApp.Models;

public class InMemoryCourseRepository : ICourseRepository
{
    private readonly Dictionary<string, Course> _store = new();

    public Course Add(Course course)
    {
        _store[course.CourseCode] = course;
        return course;
    }

    public Course? Get(string courseCode) => _store.TryGetValue(courseCode, out var c) ? c : null;

    public IReadOnlyList<Course> GetAll() => _store.Values.ToList();

    public bool Update(Course course)
    {
        if (!_store.ContainsKey(course.CourseCode)) return false;
        _store[course.CourseCode] = course;
        return true;
    }

    public bool Remove(string courseCode) => _store.Remove(courseCode);
}
