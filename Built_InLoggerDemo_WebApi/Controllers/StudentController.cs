using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace Built_InLoggerDemo_WebApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class StudentController : ControllerBase
    {
        private readonly ILogger<StudentController> _logger;

        public StudentController(ILogger<StudentController> logger)
        {
            _logger = logger;
        }

        // Simulated data
        private static readonly List<Student> Students = new()
        {
            new Student { Id = 1, Name = "Alice", Age = 20 },
            new Student { Id = 2, Name = "Bob", Age = 22 },
            new Student { Id = 3, Name = "Charlie", Age = 21 }
        };

        [HttpGet]
        public IActionResult GetAllStudents()
        {
            _logger.LogInformation("Fetching all students.");
            return Ok(Students);
        }

        [HttpGet("{id}")]
        public IActionResult GetStudentById(int id)
        {
            _logger.LogInformation("Fetching student with ID {StudentId}", id);
            var student = Students.FirstOrDefault(s => s.Id == id);

            if (student == null)
            {
                _logger.LogWarning("Student with ID {StudentId} not found.", id);
                return NotFound();
            }

            return Ok(student);
        }

        [HttpPost]
        public IActionResult AddStudent(Student newStudent)
        {
            _logger.LogInformation("Adding a new student: {@Student}", newStudent);

            newStudent.Id = Students.Max(s => s.Id) + 1;
            Students.Add(newStudent);

            _logger.LogInformation("Student added successfully with ID {StudentId}", newStudent.Id);
            return CreatedAtAction(nameof(GetStudentById), new { id = newStudent.Id }, newStudent);
        }

        [HttpDelete("{id}")]
        public IActionResult DeleteStudent(int id)
        {
            _logger.LogInformation("Attempting to delete student with ID {StudentId}", id);

            var student = Students.FirstOrDefault(s => s.Id == id);
            if (student == null)
            {
                _logger.LogError("Failed to delete student. ID {StudentId} not found.", id);
                return NotFound();
            }

            Students.Remove(student);
            _logger.LogInformation("Student with ID {StudentId} deleted successfully.", id);
            return NoContent();
        }
    }

    // Simple model
    public class Student
    {
        public int Id { get; set; }
        public string? Name { get; set; }
        public int Age { get; set; }
    }
}

