using System.Globalization;

namespace StudentApp.UI;

public class CourseMenu : IMenu
{
    private readonly ICourseService _courseService;

    public CourseMenu(ICourseService courseService)
    {
        _courseService = courseService;
    }

    public void Display()
    {
        bool exitCourseMenu = false;
        while (!exitCourseMenu)
        {
            Console.WriteLine("\nPlease select a course operation:");
            Console.WriteLine("1. Add a course");
            Console.WriteLine("2. Update a course");
            Console.WriteLine("3. Remove a course");
            Console.WriteLine("4. List all courses");
            Console.WriteLine("5. Search course");
            Console.WriteLine("6. Back to main menu");

            if (!int.TryParse(Console.ReadLine(), out int courseChoice))
            {
                Console.WriteLine("Invalid choice. Number required.");
                return;
            }

            switch (courseChoice)
            {
                case 1:
                    AddCourse();
                    break;

                case 2:
                    UpdateCourse();
                    break;

                case 3:
                    RemoveCourse();
                    break;
                
                case 4:
                    ListAllCourses();
                    break;

                case 5:
                    SearchCourse();
                    break;

                case 6:
                    Console.WriteLine("Returning to main menu.");
                    exitCourseMenu = true;
                    break;

                default:
                    Console.WriteLine("Invalid choice. Please select a valid option.");
                    break;
            }
        }
    }

    private void AddCourse()
    {
        Console.WriteLine("You selected Add a course.");
        Console.Write("Course name: ");
        string? courseName = Console.ReadLine();
        Console.Write("Course code: ");
        string? courseCode = Console.ReadLine();
        Console.Write("Course credits: ");
        if (!int.TryParse(Console.ReadLine(), out int courseCredits))
        {
            Console.WriteLine("Invalid number of credits.");
            return;
        }
        Console.Write("Instructor: ");
        string? instructor = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(courseName) || string.IsNullOrWhiteSpace(courseCode) || string.IsNullOrWhiteSpace(instructor))
            Console.WriteLine("All fields are required.");
        else
        {
            var added = _courseService.Create(courseName.Trim(), courseCode.Trim(), courseCredits, instructor.Trim());
            Console.WriteLine($"Added Course {added.CourseCode}: {added.CourseName}");
        }
    }

    private void UpdateCourse()
    {
        Console.WriteLine("You selected Update a course.");
        Console.Write("Enter Course Code: ");

        string? courseCode = Console.ReadLine();

        if (string.IsNullOrWhiteSpace(courseCode))
        {
            Console.WriteLine("Invalid Course Code.");
            return;
        }

        // Implementation for updating a course would go here
        Console.WriteLine("Choose what to update:");
        Console.WriteLine("1. Update instructor");
        Console.WriteLine("2. Update credits");
        if (!int.TryParse(Console.ReadLine(), out int updateChoice))
        {
            Console.WriteLine("Invalid choice. Number required.");
            return;
        }
        switch (updateChoice)
        {
            case 1:
                Console.Write("Enter new instructor name: ");
                string? newInstructor = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(newInstructor))
                {
                    Console.WriteLine("Instructor name cannot be empty.");
                    return;
                }
                if (_courseService.UpdateInstructor(courseCode.Trim(), newInstructor.Trim()))
                    Console.WriteLine("Instructor updated successfully.");
                else
                    Console.WriteLine("Course not found.");
                break;

            case 2:
                Console.Write("Enter new credits: ");
                if (!int.TryParse(Console.ReadLine(), out int newCredits))
                {
                    Console.WriteLine("Invalid number of credits.");
                    return;
                }
                if (_courseService.UpdateCredits(courseCode.Trim(), newCredits))
                    Console.WriteLine("Credits updated successfully.");
                else
                    Console.WriteLine("Course not found.");
                break;

            default:
                Console.WriteLine("Invalid choice. Please select a valid option.");
                break;
        }
    }

    private void RemoveCourse()
    {
        Console.WriteLine("You selected Remove a course.");
        Console.Write("Enter Course Code: ");
        string? removeCourseCode = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(removeCourseCode))
        {
            Console.WriteLine("Invalid Course Code.");
            return;
        }
        if (_courseService.Delete(removeCourseCode.Trim()))
            Console.WriteLine("Course removed successfully.");
        else
            Console.WriteLine("Course not found.");
    }

    private void ListAllCourses()
    {
        Console.WriteLine("You selected List all courses.");
        var courses = _courseService.ListAll();
        if (courses.Count == 0)
            Console.WriteLine("No courses available.");
        else
        {
            Console.WriteLine("Available courses:");
            foreach (var course in courses)
            {
                Console.WriteLine($"{course.CourseCode}: {course.CourseName} ({course.CourseCredits} credits) - Instructor: {course.CourseInstructor}");
            }
        }
    }

    private void SearchCourse()
    {
        Console.WriteLine("You selected Search course.");
        
        //search by instructor name or credits using LINQ
        Console.Write("Enter instructor name or credits to search: ");
        string? searchTerm = Console.ReadLine();
        if (string.IsNullOrWhiteSpace(searchTerm))
        {
            Console.WriteLine("Search term cannot be empty.");
            return;
        }
        var courses = _courseService.ListAll();
        var results = courses.Where(c => c.CourseInstructor.Contains(searchTerm.Trim(), StringComparison.OrdinalIgnoreCase) || c.CourseCredits.ToString() == searchTerm.Trim()).ToList();
        if (results.Count == 0)
            Console.WriteLine("No courses found.");
        else
        {
            Console.WriteLine("Search results:");
            foreach (var course in results)
            {
                Console.WriteLine($"{course.CourseCode}: {course.CourseName} ({course.CourseCredits} credits) - Instructor: {course.CourseInstructor}");
            }
        }
    }
}
