using StudentApp;

//add, update or remove students
//switch case choice for user to select options

Console.WriteLine("Welcome to the Student Management System!");
Console.WriteLine("Please select an option:");
Console.WriteLine("1. Student operations");
Console.WriteLine("2. Course operations");
Console.WriteLine("3. Enrollment operations");
Console.WriteLine("4. Main Menu");

if (!int.TryParse(Console.ReadLine(), out int choice))
{
    Console.WriteLine("Invalid input. Number required.");
    return;
}

switch (choice)
{
    case 1:
        Console.WriteLine("You selected Student operations.");
        studentManagement();
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
        Console.WriteLine("You selected Main Menu.");
        //call main menu method
        break;
    default:
        Console.WriteLine("Invalid choice. Please select a valid option.");
        break;
}

static void studentManagement()
{
    Console.WriteLine("Please select a student operation:");
    Console.WriteLine("1. Add a student");
    Console.WriteLine("2. Update a student");
    Console.WriteLine("3. Remove a student");

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
                Student newStudent = new Student(1, fullName.Trim(), emailId.Trim());
                Console.WriteLine($"Created student {newStudent.StudentName} with ID {newStudent.StudentId}.");
            }

            break;
        case 2:
            Console.WriteLine("You selected Update a student.");
            //call update student method
            break;
        case 3:
            Console.WriteLine("You selected Remove a student.");
            //call remove student method
            break;
        default:
            Console.WriteLine("Invalid choice. Please select a valid option.");
            break;
    }
}