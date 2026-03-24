namespace StudentApp.UI;

public class EnrollmentMenu : IMenu
{
    private readonly IEnrollmentService _enrollmentService;
    private readonly IStudentService _studentService;

    public EnrollmentMenu(IEnrollmentService enrollmentService, IStudentService studentService)
    {
        _enrollmentService = enrollmentService;
        _studentService = studentService;
    }

    public void Display()
    {
        bool exitEnrollmentMenu = false;

        while (!exitEnrollmentMenu)
        {
            Console.WriteLine("\nPlease select an enrollment operation:");
            Console.WriteLine("1. Enroll a student in a course");
            Console.WriteLine("2. List all enrollments");
            Console.WriteLine("3. Assign grade");
            Console.WriteLine("4. Back to main menu");

            if (!int.TryParse(Console.ReadLine(), out int enrollmentChoice))
            {
                Console.WriteLine("Invalid choice. Number required.");
                return;
            }

            switch (enrollmentChoice)
            {
                case 1:
                    EnrollStudent();
                    break;

                case 2:
                    ListAllEnrollments();
                    break;

                case 3:
                    AssignGrade();
                    break;
                
                case 4:
                    Console.WriteLine("Returning to main menu.");
                    exitEnrollmentMenu = true;
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }
        }
    }

    private void EnrollStudent()
    {
        Console.WriteLine("You selected Enroll a student in a course.");
        Console.Write("Enter Student ID: ");
        if (!int.TryParse(Console.ReadLine(), out int studentId))
        {
            Console.WriteLine("Invalid Student ID.");
            return;
        }
        Console.Write("Enter Course Code: ");
        string? courseCode = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(courseCode))
        {
            Console.WriteLine("Invalid Course Code.");
            return;
        }
        var enrollment = _enrollmentService.Enroll(studentId, courseCode.Trim());
        //add to student course list too
        var studentAdded = _studentService.AddCourse(studentId, courseCode.Trim());
        if (enrollment != null || studentAdded != false)
            Console.WriteLine("Student enrolled successfully.");
        else
            Console.WriteLine("Enrollment failed. Check if student ID and course code are correct.");
    }

    private void ListAllEnrollments()
    {
        Console.WriteLine("You selected List all enrollments.");
        var enrollments = _enrollmentService.ListAll();
        if (enrollments.Count == 0)
            Console.WriteLine("No enrollments available.");
        else
        {
            Console.WriteLine("Current enrollments:");
            foreach (var e in enrollments)
            {
                Console.WriteLine($"Student ID {e.StudentId} is enrolled in Course {e.CourseCode} with grade {e.CourseGrade ?? "(Not assigned)"}");
            }
        }
    }

    private void AssignGrade()
    {
        Console.WriteLine("You selected Assign grade to an enrollment.");
        Console.Write("Enter Student ID: ");
        if (!int.TryParse(Console.ReadLine(), out int studentId))
        {
            Console.WriteLine("Invalid Student ID.");
            return;
        }
        Console.Write("Enter Course Code: ");
        string? courseCode = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(courseCode))
        {
            Console.WriteLine("Invalid Course Code.");
            return;
        }
        var enrollment = _enrollmentService.GetEnrollment(studentId, courseCode.Trim());
        if (enrollment == null)
        {
            Console.WriteLine("Enrollment not found. Check if student ID and course code are correct.");
            return;
        }
        Console.Write("Enter Grade: ");
        string? grade = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(grade))
        {
            Console.WriteLine("Invalid Grade.");
            return;
        }
        enrollment.CourseGrade = grade.Trim();
        Console.WriteLine("Grade assigned successfully.");
    }
}
