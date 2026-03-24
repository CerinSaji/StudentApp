namespace StudentApp.Models;

public class Course
{
    public string CourseName { get; init; } //names dont usually change
    public string CourseCode { get; init; } //course code is also unique and doesnt change
    public int CourseCredits { get; set; }
    public string CourseInstructor { get; set; }

    public Course(string CourseName, string CourseCode, int CourseCredits, string CourseInstructor)
    {
        this.CourseName = CourseName;
        this.CourseCode = CourseCode;
        this.CourseCredits = CourseCredits;
        this.CourseInstructor = CourseInstructor;
    }
}
