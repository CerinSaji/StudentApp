namespace StudentApp.Repositories;
using StudentApp.Models;

public interface ICourseRepository
{
    Course Add(Course course);
    Course? Get(string courseCode);
    bool Update(Course course);
    bool Remove(string courseCode);
    IReadOnlyList<Course> GetAll();
}
