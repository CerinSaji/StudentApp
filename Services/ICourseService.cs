namespace StudentApp.Services;
using StudentApp.Models;

public interface ICourseService
{
    Course Create(string name, string code, int credits, string instructor);
    bool UpdateInstructor(string courseCode, string newInstructor);
    bool UpdateCredits(string courseCode, int newCredits);
    bool Delete(string courseCode);
    Course? Get(string courseCode);
    IReadOnlyList<Course> ListAll();
}
