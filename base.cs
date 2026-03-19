namespace StudentApp;

public class Student
{
    //fields
    private int _studentId;
    private string _studentName;
    private string _studentEmail;
    public string? EnrollmentDate; //can be null because not all students may have enrolled yet (i think)
    public List<string> Courses = new List<string>(); //list of course ids

    //properties
    public int StudentId
    {
        get => _studentId; init => _studentId = value;       
        //init because we want to set the value only at the time of object creation?
    }

    public string StudentName 
    {
        get => _studentName; init => _studentName = value;
    }

    public string StudentEmail
    {
        get => _studentEmail; set => _studentEmail = value;
    }

    //constructor 
    public Student(int StudentId, string StudentName, string StudentEmail, string? EnrollmentDate = null)
    {
        this.StudentId = StudentId;
        this.StudentName = StudentName;
        this.StudentEmail = StudentEmail;
        this.EnrollmentDate = EnrollmentDate;
    }

    //methods  
}

public class Course
{
    public string CourseName { get; init; } //names dont usually change so
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

public class Enrollment
{
    public int StudentId { get; init; }
    public string CourseCode { get; init; }
    public string CourseGrade { get; set; }
}