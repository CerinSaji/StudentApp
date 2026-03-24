namespace StudentApp.Models;

public class Student
{
    //fields
    private int _studentId;
    private string _studentName;
    private string _studentEmail;
    public string? EnrollmentDate; //can be null because not all students may have enrolled yet
    public List<string> Courses = new List<string>(); //list of course ids

    //properties
    public int StudentId
    {
        get => _studentId; init => _studentId = value;       
        //init because we want to set the value only at the time of object creation
    }

    public string StudentName 
    {
        get => _studentName; init => _studentName = value;
    }

    public string StudentEmail
    {
        get => _studentEmail; set => _studentEmail = value;
    }

    public List<string> CourseList
    {
        get => Courses; set => Courses = value;
    }

    //constructor 
    public Student(int StudentId, string StudentName, string StudentEmail, string? EnrollmentDate = null)
    {
        this.StudentId = StudentId;
        this.StudentName = StudentName;
        this.StudentEmail = StudentEmail;
        this.EnrollmentDate = EnrollmentDate;
    }
}
