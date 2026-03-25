## Project Structure

```
StudentApp/
├── Models/                    # Domain entities
│   ├── Student.cs
│   ├── Course.cs
│   └── Enrollment.cs
├── Repositories/              # Data access layer
│   ├── IStudentRepository.cs
│   ├── InMemoryStudentRepository.cs
│   ├── ICourseRepository.cs
│   ├── InMemoryCourseRepository.cs
│   ├── IEnrollmentRepository.cs
│   └── InMemoryEnrollmentRepository.cs
├── Services/                  # logic layer
│   ├── IStudentService.cs
│   ├── StudentService.cs
│   ├── ICourseService.cs
│   ├── CourseService.cs
│   ├── IEnrollmentService.cs
│   └── EnrollmentService.cs
├── UI/                        # Presentation layer
│   ├── IMenu.cs
│   ├── MainMenu.cs
│   ├── StudentMenu.cs
│   ├── CourseMenu.cs
│   ├── EnrollmentMenu.cs
│   └── ReportsMenu.cs
├── Program.cs                 # Application entry point
├── base.cs                    # Shared utilities (currently minimal)
├── README.md                  # This file
└── StudentApp.csproj          # Project configuration
```

## Features implemented

### 1. Main navigation
- Main menu with options to access:
  - Student operations
  - Course operations
  - Enrollment operations
  - Reports
  - Exit

### 2. Student operations
- Add a student (name + email)
- Update student email
- Remove a student
- Display student details
- Filter and sort students (added later):
  - Filter by name (partial, case-insensitive)
  - Optional minimum and maximum enrolled courses
  - Sort by Name / Student ID / Number of courses
  - Sort direction ascending/descending
- View list of student entries with ID, name, email, and course count

### 3. Course operations
- Add a course (name, code, credits, instructor)
- Update course details
- Remove a course
- List all courses and their information

### 4. Enrollment operations
- Enroll a student in a course
- List all enrollments (including course grades display)
- Assign grade to enrollment (implemented in code path)
- Integrated student-course addition to student records

### 5. Reports
- **Students per course** report:
  - Groups all enrollments by course
  - Displays course details (code, name, credits, instructor)
  - Lists enrolled students with grades
  - Uses LINQ grouping for efficient data aggregation
  - Reports can be extended with additional views

### 6. Architecture updates (refactor)
- **Modular structure** with clear separation of concerns:
  - `Models/` - Domain entities (Student, Course, Enrollment)
  - `Repositories/` - Data access layer (interfaces + in-memory implementations)
  - `Services/` - Business logic layer (interfaces + implementations)
  - `UI/` - Presentation layer (menu classes implementing IMenu)
- `Program.cs` minimal initialization only
- Clean namespace organization with proper using directives
- Dependency injection pattern throughout

### 7. Error handling improvements
- Input parsing validation on numeric selections
- Checking null / whitespace for required fields
- User-friendly messages for not found / invalid entries

## Build & run
```bash
cd StudentApp
dotnet build
dotnet run
```

## Notes
- Application uses in-memory data stores, so data is not persisted across runs.
- There are a small number of existing warnings for nullable initialization in `base.cs` (non-nullable properties/fields).
