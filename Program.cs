using StudentApp;

IStudentRepository studentRepo = new InMemoryStudentRepository();
IStudentService studentService = new StudentService(studentRepo);
ICourseRepository courseRepo = new InMemoryCourseRepository();
ICourseService courseService = new CourseService(courseRepo);
//The service doesn't know or care about the storage implementation

RunMainMenu(studentService, courseService);

//switch case choice for user to select options
static void RunMainMenu(IStudentService studentService, ICourseService courseService)
{
    Console.WriteLine("Welcome to the Student Management System!");

    bool exit = false;
    while (!exit)
    {
        Console.WriteLine("Please select an option:");
        Console.WriteLine("1. Student operations");
        Console.WriteLine("2. Course operations");
        Console.WriteLine("3. Enrollment operations");
        Console.WriteLine("4. Exit");

        if (!int.TryParse(Console.ReadLine(), out int choice))
        {
            Console.WriteLine("Invalid input. Number required.");
            return;
        }
        
        switch (choice)
        {
            case 1:
                Console.WriteLine("You selected Student operations.");
                StudentMenu(studentService);
                break;
            case 2:
                Console.WriteLine("You selected Course operations.");
                CourseMenu(courseService);
                break;
            case 3:
                Console.WriteLine("You selected Enrollment operations.");
                //call enrollment operations method
                break;
            case 4:
                Console.WriteLine("You will exit the application. Goodbye!");
                exit = true;
                break;
            default:
                Console.WriteLine("Invalid choice. Please select a valid option.");
                break;
        }
    }
}


static void StudentMenu(IStudentService studentService)
{
    bool exitStudentMenu = false;

    while (!exitStudentMenu)
    {
        Console.WriteLine("Please select a student operation:");
        Console.WriteLine("1. Add a student");
        Console.WriteLine("2. Update a student");
        Console.WriteLine("3. Remove a student");
        Console.WriteLine("4. Back to main menu");

        if (!int.TryParse(Console.ReadLine(), out int studentChoice))
        {
            Console.WriteLine("Invalid choice. Number required.");
            return;
        }

        switch (studentChoice)
        {
            case 1:
                Console.WriteLine("You selected Add a student.");
                Console.Write("Full name: ");
                string? fullName = Console.ReadLine();
                Console.Write("Email: ");
                string? emailId = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(fullName) || string.IsNullOrWhiteSpace(emailId))
                    Console.WriteLine("Full name and email are required.");
                else
                {
                    var added = studentService.Create(fullName.Trim(), emailId.Trim());
                    Console.WriteLine($"Added ID {added.StudentId} {added.StudentName}");
                }

                break;

            case 2:
                Console.WriteLine("You selected Update a student.");
                //call update student method
                Console.Write("Enter Student ID: ");

                if (int.TryParse(Console.ReadLine(), out int id))
                {
                    //display current details for reference
                    var students = studentService.ListAllInfo(id);
                    if (students == null || students.Count == 0)
                    {
                        Console.WriteLine("Student not found.");
                        break;
                    }
                    var student = students[0]; //returns in array/list format so get item at index 0?
                    Console.WriteLine($"Current details for Student ID {id}:");
                    Console.WriteLine($"Name: {student.StudentName}");
                    Console.WriteLine($"Email: {student.StudentEmail}");

                    Console.Write("Enter new email: ");
                    string? newEmail = Console.ReadLine();

                    if (string.IsNullOrWhiteSpace(newEmail))
                        Console.WriteLine("New email is required.");
                    else
                    {
                        if (studentService.UpdateEmail(id, newEmail.Trim()))
                            Console.WriteLine("Student email updated successfully.");
                        else
                            Console.WriteLine("Student not found.");
                    }
                }

                else
                    Console.WriteLine("Invalid Student ID.");

                break;

            case 3:
                Console.WriteLine("You selected Remove a student.");
                //call remove student method
                Console.Write("Enter Student ID: ");
                if (int.TryParse(Console.ReadLine(), out int removeId))
                {
                    if (studentService.Delete(removeId))
                        Console.WriteLine("Student removed successfully.");
                    else
                        Console.WriteLine("Student not found.");
                }
                else
                    Console.WriteLine("Invalid Student ID.");
                break;
            
            case 4:
                Console.WriteLine("Returning to main menu.");
                exitStudentMenu = true;
                break;

            default:
                Console.WriteLine("Invalid choice. Please select a valid option.");
                break;

        }
    }
}

static void CourseMenu(ICourseService courseService)
{
    //similar to student menu but for courses   
    bool exitCourseMenu = false;
    while (!exitCourseMenu)
    {
        Console.WriteLine("Please select a course operation:");
        Console.WriteLine("1. Add a course");
        Console.WriteLine("2. Update a course");
        Console.WriteLine("3. Remove a course");
        Console.WriteLine("4. List all courses");
        Console.WriteLine("5. Back to main menu");

        if (!int.TryParse(Console.ReadLine(), out int courseChoice))
        {
            Console.WriteLine("Invalid choice. Number required.");
            return;
        }

        switch (courseChoice)
        {
            case 1:
                Console.WriteLine("You selected Add a course.");
                Console.Write("Course name: ");
                string? courseName = Console.ReadLine();
                Console.Write("Course code: ");
                string? courseCode = Console.ReadLine();
                Console.Write("Course credits: ");
                if (!int.TryParse(Console.ReadLine(), out int courseCredits))
                {
                    Console.WriteLine("Invalid number of credits.");
                    break;
                }
                Console.Write("Instructor: ");
                string? instructor = Console.ReadLine();

                if (string.IsNullOrWhiteSpace(courseName) || string.IsNullOrWhiteSpace(courseCode) || string.IsNullOrWhiteSpace(instructor))
                    Console.WriteLine("All fields are required.");
                else
                {
                    var added = courseService.Create(courseName.Trim(), courseCode.Trim(), courseCredits, instructor.Trim());
                    Console.WriteLine($"Added Course {added.CourseCode}: {added.CourseName}");
                }

                break;

            case 2:
                Console.WriteLine("You selected Update a course.");
                //call update course method
                Console.Write("Enter Course Code: ");

                if (string.IsNullOrWhiteSpace(Console.ReadLine()))
                {
                    Console.WriteLine("Invalid Course Code.");
                    break;
                }

                // Implementation for updating a course would go here

                break;

            case 3:
                Console.WriteLine("You selected Remove a course.");
                //call remove course method
                Console.Write("Enter Course Code: ");
                string? removeCourseCode = Console.ReadLine();
                if (string.IsNullOrWhiteSpace(removeCourseCode))
                {
                    Console.WriteLine("Invalid Course Code.");
                    break;
                }
                if (courseService.Delete(removeCourseCode.Trim()))
                    Console.WriteLine("Course removed successfully.");
                else
                    Console.WriteLine("Course not found.");

                break;
            
            case 4:
                Console.WriteLine("You selected List all courses.");
                var courses = courseService.ListAll();
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
                break;

            case 5:
                Console.WriteLine("Returning to main menu.");
                exitCourseMenu = true;
                break;

            default:
                Console.WriteLine("Invalid choice. Please select a valid option.");
                break;

        }
    }

}
