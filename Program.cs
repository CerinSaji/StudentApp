using StudentApp;

IStudentRepository studentRepo = new InMemoryStudentRepository();
IStudentService studentService = new StudentService(studentRepo);

RunMainMenu(studentService);

//switch case choice for user to select options
static void RunMainMenu(IStudentService studentService)
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
                //call course operations method
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
                {
                    Console.WriteLine("Full name and email are required.");
                }
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
