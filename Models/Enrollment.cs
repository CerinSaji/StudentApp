namespace StudentApp.Models;

public class Enrollment
{
    public int StudentId { get; init; }
    public required string CourseCode { get; init; }
    public string CourseGrade { get; set; }
}
