namespace StudentApp.Repositories;
using StudentApp.Models;

public interface IStudentRepository
{
    Student Add(Student student);
    Student? Get(int id);
    bool Update(Student student);
    bool Remove(int id);
    IReadOnlyList<Student> GetAll();
}
