namespace StudentApp.UI;
using StudentApp.Services;

public class MainMenu : IMenu
{
    private readonly IStudentService _studentService;
    private readonly ICourseService _courseService;
    private readonly IEnrollmentService _enrollmentService;

    public MainMenu(IStudentService studentService, ICourseService courseService, IEnrollmentService enrollmentService)
    {
        _studentService = studentService;
        _courseService = courseService;
        _enrollmentService = enrollmentService;
    }

    public void Display()
    {
        Console.WriteLine("Welcome to the Student Management System!");

        bool exit = false;
        while (!exit)
        {
            Console.WriteLine("\nPlease select an option:");
            Console.WriteLine("1. Student operations");
            Console.WriteLine("2. Course operations");
            Console.WriteLine("3. Enrollment operations");
            Console.WriteLine("4. Reports");
            Console.WriteLine("5. Exit");

            /*if (!int.TryParse(Console.ReadLine(), out int choice))
            {
                Console.WriteLine("Invalid input. Number required.");
                return;
            }*/
            
            int choice = int.TryParse(Console.ReadLine(), out int parsedChoice) ? parsedChoice : 0;
            switch (choice)
            {
                case 1:
                    Console.WriteLine("You selected Student operations.");
                    var studentMenu = new StudentMenu(_studentService);
                    studentMenu.Display();
                    break;
                case 2:
                    Console.WriteLine("You selected Course operations.");
                    var courseMenu = new CourseMenu(_courseService);
                    courseMenu.Display();
                    break;
                case 3:
                    Console.WriteLine("You selected Enrollment operations.");
                    var enrollmentMenu = new EnrollmentMenu(_enrollmentService, _studentService);
                    enrollmentMenu.Display();
                    break;
                case 4:
                    Console.WriteLine("You selected Reports.");
                    var reportsMenu = new ReportsMenu(_courseService, _enrollmentService);
                    reportsMenu.Display();
                    break;
                case 5:
                    Console.WriteLine("You will exit the application. Goodbye!");
                    exit = true;
                    break;
                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }
        }
    }
}
