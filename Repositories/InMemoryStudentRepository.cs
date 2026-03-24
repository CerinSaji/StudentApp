namespace StudentApp.Repositories;
using StudentApp.Models;

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
