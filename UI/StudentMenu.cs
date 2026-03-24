namespace StudentApp.UI;

public class StudentMenu : IMenu
{
    private readonly IStudentService _studentService;

    public StudentMenu(IStudentService studentService)
    {
        _studentService = studentService;
    }

    public void Display()
    {
        bool exitStudentMenu = false;

        while (!exitStudentMenu)
        {
            Console.WriteLine("\nPlease select a student operation:");
            Console.WriteLine("1. Add a student");
            Console.WriteLine("2. Update a student");
            Console.WriteLine("3. Remove a student");
            Console.WriteLine("4. Filter and sort students");
            Console.WriteLine("5. Find and display student details");
            Console.WriteLine("6. Back to main menu");

            if (!int.TryParse(Console.ReadLine(), out int studentChoice))
            {
                Console.WriteLine("Invalid choice. Number required.");
                return;
            }

            switch (studentChoice)
            {
                case 1:
                    AddStudent();
                    break;

                case 2:
                    UpdateStudent();
                    break;

                case 3:
                    RemoveStudent();
                    break;
                case 4:
                    FilterAndSortStudents();
                    break;
                case 5:
                    DisplayStudentDetails();
                    break;
                case 6:
                    Console.WriteLine("Returning to main menu.");
                    exitStudentMenu = true;
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }
        }
    }

    private void AddStudent()
    {
        Console.WriteLine("You selected Add a student.");
        Console.Write("Full name: ");
        string? fullName = Console.ReadLine();
        Console.Write("Email: ");
        string? emailId = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(emailId))
            Console.WriteLine("Full name and email are required.");
        else
        {
            var added = _studentService.Create(fullName.Trim(), emailId.Trim());
            Console.WriteLine($"Added ID {added.StudentId} {added.StudentName}");
        }
    }

    private void UpdateStudent()
    {
        Console.WriteLine("You selected Update a student.");
        Console.Write("Enter Student ID: ");

        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var students = _studentService.ListStudentInfo(id);
            if (students == null || students.Count == 0)
            {
                Console.WriteLine("Student not found.");
                return;
            }
            var student = students[0];
            Console.WriteLine($"Current details for Student ID {id}:");
            Console.WriteLine($"Name: {student.StudentName}");
            Console.WriteLine($"Email: {student.StudentEmail}");

            Console.Write("Enter new email: ");
            string? newEmail = Console.ReadLine();

            if (string.IsNullOrWhiteSpace(newEmail))
                Console.WriteLine("New email is required.");
            else
            {
                if (_studentService.UpdateEmail(id, newEmail.Trim()))
                    Console.WriteLine("Student email updated successfully.");
                else
                    Console.WriteLine("Student not found.");
            }
        }
        else
            Console.WriteLine("Invalid Student ID.");
    }

    private void RemoveStudent()
    {
        Console.WriteLine("You selected Remove a student.");
        Console.Write("Enter Student ID: ");
        if (int.TryParse(Console.ReadLine(), out int removeId))
        {
            if (_studentService.Delete(removeId))
                Console.WriteLine("Student removed successfully.");
            else
                Console.WriteLine("Student not found.");
        }
        else
            Console.WriteLine("Invalid Student ID.");
    }

    private void DisplayStudentDetails()
    {
        Console.WriteLine("You selected Display student details.");
        Console.Write("Enter Student ID: ");
        if (int.TryParse(Console.ReadLine(), out int id))
        {
            var students = _studentService.ListStudentInfo(id);
            if (students == null || students.Count == 0)
            {
                Console.WriteLine("Student not found.");
                return;
            }
            var student = students[0];
            Console.WriteLine($"Details for Student ID {id}:");
            Console.WriteLine($"Name: {student.StudentName}");
            Console.WriteLine($"Email: {student.StudentEmail}");
            if (student.CourseList.Count > 0)
            {
                Console.WriteLine("Enrolled courses:");
                foreach (var course in student.CourseList)
                {
                    Console.WriteLine($"- {course}");
                }
            }
            else
                Console.WriteLine("No enrolled courses.");
        }
        else
            Console.WriteLine("Invalid Student ID.");
    }

    private void FilterAndSortStudents()
    {
        Console.WriteLine("\n=== Student Filter & Sort ===");

        // Get filter criteria
        Console.Write("Search by name (leave empty for all): ");
        string? nameFilter = Console.ReadLine();
        nameFilter = string.IsNullOrWhiteSpace(nameFilter) ? null : nameFilter.Trim();

        Console.Write("Minimum courses (leave empty for no minimum): ");
        int? minCourses = null;
        string? minInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(minInput) && int.TryParse(minInput, out int min))
            minCourses = min;

        Console.Write("Maximum courses (leave empty for no maximum): ");
        int? maxCourses = null;
        string? maxInput = Console.ReadLine();
        if (!string.IsNullOrWhiteSpace(maxInput) && int.TryParse(maxInput, out int max))
            maxCourses = max;

        // Get sort options
        Console.WriteLine("\nSort by:");
        Console.WriteLine("1. Name");
        Console.WriteLine("2. Student ID");
        Console.WriteLine("3. Number of courses");
        Console.Write("Choose sort field (1-3): ");
        int sortField = 1;
        if (!int.TryParse(Console.ReadLine(), out sortField) || sortField < 1 || sortField > 3)
            sortField = 1;

        Console.Write("Sort direction (A)scending or (D)escending [A]: ");
        string? direction = Console.ReadLine();
        bool ascending = string.IsNullOrWhiteSpace(direction) || !direction.Trim().ToUpper().StartsWith("D");

        // Apply filters and sorting
        var students = _studentService.ListAll();

        var filtered = students
            .Where(s =>
                (nameFilter == null ||
                 s.StudentName.Contains(nameFilter, StringComparison.OrdinalIgnoreCase))
                && (!minCourses.HasValue || s.Courses.Count >= minCourses.Value)
                && (!maxCourses.HasValue || s.Courses.Count <= maxCourses.Value));

        // Apply sorting
        IOrderedEnumerable<Student> sorted;
        switch (sortField)
        {
            case 1: // Name
                sorted = ascending ? filtered.OrderBy(s => s.StudentName) : filtered.OrderByDescending(s => s.StudentName);
                break;
            case 2: // Student ID
                sorted = ascending ? filtered.OrderBy(s => s.StudentId) : filtered.OrderByDescending(s => s.StudentId);
                break;
            case 3: // Course count
                sorted = ascending ? filtered.OrderBy(s => s.Courses.Count) : filtered.OrderByDescending(s => s.Courses.Count);
                break;
            default:
                sorted = filtered.OrderBy(s => s.StudentName);
                break;
        }

        var results = sorted.ToList();

        // Display results
        Console.WriteLine($"\n=== Results ({results.Count} students) ===");
        if (!results.Any())
        {
            Console.WriteLine("No students match the specified criteria.");
        }
        else
        {
            Console.WriteLine("ID".PadRight(5) + "Name".PadRight(20) + "Email".PadRight(30) + "Courses");
            Console.WriteLine(new string('-', 80));

            foreach (var student in results)
            {
                Console.WriteLine($"{student.StudentId.ToString().PadRight(5)}{student.StudentName.PadRight(20)}{student.StudentEmail.PadRight(30)}{student.Courses.Count}");
            }
        }

        Console.WriteLine("\nPress Enter to continue...");
        Console.ReadLine();
    }
}
