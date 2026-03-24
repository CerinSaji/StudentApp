namespace StudentApp;

public class Student
{
    //fields
    private int _studentId;
    private string _studentName;
    private string _studentEmail;
    public string? EnrollmentDate; //can be null because not all students may have enrolled yet (i think)
    public List<string> Courses = new List<string>(); //list of course ids

    //properties
    public int StudentId
    {
        get => _studentId; init => _studentId = value;       
        //init because we want to set the value only at the time of object creation?
    }

    public string StudentName 
    {
        get => _studentName; init => _studentName = value;
    }

    public string StudentEmail
    {
        get => _studentEmail; set => _studentEmail = value;
    }

    //constructor 
    public Student(int StudentId, string StudentName, string StudentEmail, string? EnrollmentDate = null)
    {
        this.StudentId = StudentId;
        this.StudentName = StudentName;
        this.StudentEmail = StudentEmail;
        this.EnrollmentDate = EnrollmentDate;
    }

    //methods  
}

public class Course
{
    public string CourseName { get; init; } //names dont usually change so
    public string CourseCode { get; init; } //course code is also unique and doesnt change
    public int CourseCredits { get; set; }
    public string CourseInstructor { get; set; }

    public Course(string CourseName, string CourseCode, int CourseCredits, string CourseInstructor)
    {
        this.CourseName = CourseName;
        this.CourseCode = CourseCode;
        this.CourseCredits = CourseCredits;
        this.CourseInstructor = CourseInstructor;
    }
}

public class Enrollment
{
    public int StudentId { get; init; }
    public required string CourseCode { get; init; }
    public required string CourseGrade { get; set; }
}

public interface IStudentRepository
{
    Student Add(Student student);
    Student? Get(int id);
    bool Update(Student student);
    bool Remove(int id);
    IReadOnlyList<Student> GetAll();
}

public class InMemoryStudentRepository : IStudentRepository
{
    private readonly Dictionary<int, Student> _store = new();
    static int _nextId = 1;

    public Student Add(Student student)
    {
        student = new Student(_nextId++, student.StudentName, student.StudentEmail, student.EnrollmentDate);
        _store[student.StudentId] = student;
        return student;
    }

    public Student? Get(int id) => _store.TryGetValue(id, out var s) ? s : null;

    public IReadOnlyList<Student> GetAll() => _store.Values.ToList();

    public bool Update(Student student)
    {
        if (!_store.ContainsKey(student.StudentId)) return false;
        _store[student.StudentId] = student;
        return true;
    }

    public bool Remove(int id) => _store.Remove(id);
}

public interface IStudentService
{
    Student Create(string name, string email, string? enrollmentDate = null);
    bool UpdateEmail(int studentId, string newEmail);
    bool Delete(int studentId);
    Student? Get(int studentId);
    IReadOnlyList<Student> ListAllInfo(int studentId); 
    IReadOnlyList<Student> ListAll(); //added this method to list all students, not just by id
}

public class StudentService : IStudentService
{
    private readonly IStudentRepository _repo;
    public StudentService(IStudentRepository repo) => _repo = repo;

    public Student Create(string name, string email, string? enrollmentDate = null)
    {
        var student = new Student(0, name, email, enrollmentDate); // id assigned in repo
        return _repo.Add(student);
    }

    public bool UpdateEmail(int studentId, string newEmail)
    {
        var existing = _repo.Get(studentId);
        if (existing == null) return false;
        var updated = new Student(existing.StudentId, existing.StudentName, newEmail, existing.EnrollmentDate);
        return _repo.Update(updated);
    }

    public bool Delete(int studentId) => _repo.Remove(studentId);
    public Student? Get(int studentId) => _repo.Get(studentId);
    public IReadOnlyList<Student> ListAllInfo(int studentId) => _repo.GetAll().Where(s => s.StudentId == studentId).ToList();

    public IReadOnlyList<Student> ListAll() => _repo.GetAll();
}

public interface ICourseRepository
{
    Course Add(Course course);
    Course? Get(string courseCode);
    bool Update(Course course);
    bool Remove(string courseCode);
    IReadOnlyList<Course> GetAll();
}

public class InMemoryCourseRepository : ICourseRepository
{
    private readonly Dictionary<string, Course> _store = new();

    public Course Add(Course course)
    {
        _store[course.CourseCode] = course;
        return course;
    }

    public Course? Get(string courseCode) => _store.TryGetValue(courseCode, out var c) ? c : null;

    //what is the appropriate access modifier for this method
    public IReadOnlyList<Course> GetAll() => _store.Values.ToList();

    public bool Update(Course course)
    {
        if (!_store.ContainsKey(course.CourseCode)) return false;
        _store[course.CourseCode] = course;
        return true;
    }

    public bool Remove(string courseCode) => _store.Remove(courseCode);
}

public interface ICourseService
{
    Course Create(string name, string code, int credits, string instructor);
    bool UpdateInstructor(string courseCode, string newInstructor);
    bool Delete(string courseCode);
    Course? Get(string courseCode);
    IReadOnlyList<Course> ListAll();
}

public class CourseService : ICourseService
{
    private readonly ICourseRepository _repo;
    public CourseService(ICourseRepository repo) => _repo = repo;

    public Course Create(string name, string code, int credits, string instructor)
    {
        var course = new Course(name, code, credits, instructor);
        return _repo.Add(course);
    }

    public bool UpdateInstructor(string courseCode, string newInstructor)
    {
        var existing = _repo.Get(courseCode);
        if (existing == null) return false;
        var updated = new Course(existing.CourseName, existing.CourseCode, existing.CourseCredits, newInstructor);
        return _repo.Update(updated);
    }

    public bool Delete(string courseCode) => _repo.Remove(courseCode);
    public Course? Get(string courseCode) => _repo.Get(courseCode);
    public IReadOnlyList<Course> ListAll() => _repo.GetAll();
}