using StudentApp;
using StudentApp.UI;
using StudentApp.Repositories;
using StudentApp.Services;

// Initialize repositories
IStudentRepository studentRepo = new InMemoryStudentRepository();
IStudentService studentService = new StudentService(studentRepo);
ICourseRepository courseRepo = new InMemoryCourseRepository();
ICourseService courseService = new CourseService(courseRepo);
IEnrollmentRepository enrollmentRepo = new InMemoryEnrollmentRepository();
IEnrollmentService enrollmentService = new EnrollmentService(enrollmentRepo);

// Display main menu
var mainMenu = new MainMenu(studentService, courseService, enrollmentService);
mainMenu.Display();