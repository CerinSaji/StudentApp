namespace StudentApp.UI;

public class ReportsMenu : IMenu
{
    private readonly ICourseService _courseService;
    private readonly IEnrollmentService _enrollmentService;

    public ReportsMenu(ICourseService courseService, IEnrollmentService enrollmentService)
    {
        _courseService = courseService;
        _enrollmentService = enrollmentService;
    }

    public void Display()
    {
        bool exitReportsMenu = false;

        while (!exitReportsMenu)
        {
            Console.WriteLine("\nPlease select a report:");
            Console.WriteLine("1. Students per course");
            Console.WriteLine("2. Back to main menu");

            if (!int.TryParse(Console.ReadLine(), out int reportChoice))
            {
                Console.WriteLine("Invalid choice. Number required.");
                return;
            }

            switch (reportChoice)
            {
                case 1:
                    DisplayStudentsPerCourse();
                    break;
                case 2:
                    Console.WriteLine("Returning to main menu.");
                    exitReportsMenu = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }
        }
    }

    private void DisplayStudentsPerCourse()
    {
        Console.WriteLine("\n=== Students Per Course Report ===");

        var courses = _courseService.ListAll();
        var enrollments = _enrollmentService.ListAll();

        if (courses.Count == 0)
        {
            Console.WriteLine("No courses available.");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            return;
        }

        // Group enrollments by course code
        var studentsByCourse = enrollments
            .GroupBy(e => e.CourseCode)
            .OrderBy(g => g.Key)
            .ToList();

        if (studentsByCourse.Count == 0)
        {
            Console.WriteLine("No enrollments found.");
            Console.WriteLine("Press Enter to continue...");
            Console.ReadLine();
            return;
        }

        // Display report
        foreach (var courseGroup in studentsByCourse)
        {
            var course = courses.FirstOrDefault(c => c.CourseCode == courseGroup.Key);
            if (course == null)
                continue;

            Console.WriteLine($"\n{course.CourseCode}: {course.CourseName} ({course.CourseCredits} credits)");
            Console.WriteLine($"Instructor: {course.CourseInstructor}");
            Console.WriteLine($"Enrolled Students ({courseGroup.Count()}):");
            Console.WriteLine("─────────────────────────────────────────────");

            var students = courseGroup
                .OrderBy(e => e.StudentId)
                .ToList();

            if (students.Count == 0)
            {
                Console.WriteLine("No students enrolled.");
            }
            else
            {
                Console.WriteLine("Student ID | Grade");
                Console.WriteLine("─────────────────────────────────────────────");
                foreach (var enrollment in students)
                {
                    string grade = string.IsNullOrWhiteSpace(enrollment.CourseGrade) ? "Not assigned" : enrollment.CourseGrade;
                    Console.WriteLine($"{enrollment.StudentId.ToString().PadRight(10)} | {grade}");
                }
            }
        }

        Console.WriteLine("\n=== End of Report ===");
        Console.WriteLine("Press Enter to continue...");
        Console.ReadLine();
    }
}
