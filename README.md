# StudentApp

## Overview
StudentApp is a console-based student management system built in C# .NET 10.0. The app uses in-memory repositories for students, courses, and enrollments and exposes a clean menu-driven CLI for operations.

## Features implemented

### 1. Main navigation
- Main menu with options to access:
  - Student operations
  - Course operations
  - Enrollment operations
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

### 5. Architecture updates (refactor)
- `Program.cs` minimal initialization only
- UI layer moved into `UI/` folder:
  - `IMenu` interface with `Display()`
  - `MainMenu`, `StudentMenu`, `CourseMenu`, `EnrollmentMenu`
- services accessed through interfaces:
  - `IStudentService`, `ICourseService`, `IEnrollmentService`
  - `IStudentRepository`, `ICourseRepository`, `IEnrollmentRepository`

### 6. Error handling improvements
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
