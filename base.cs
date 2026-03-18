namespace StudentApp;

public class Base
{
    //fields
    private int studentId;
    private string studentName;
    private string studentEmail;
    public string enrollmentDate;
    public string[] courses = new string[5];

    //properties
    public int StudentId
    {
        get; init; 
        //init because we want to set the value only at the time of object creation?
        }

    public required string StudentName
    {
        get; private set;
        //private set because we want to set the value only within the class?
    }

}