using Grpc.Dal.Entities;
using Grpc.Dal.Interfaces;
using Microsoft.EntityFrameworkCore;

namespace Grpc.Dal.Repositories
{
    public class StudentRepository : IStudentRepository
    {
        private Context _context;

        public StudentRepository(Context context)
        {
            _context = context;
        }

        public async Task<StudentEntity> AddStudent(StudentEntity student)
        {
            await _context.Students.AddAsync(student);

            _context.SaveChanges();

            return student;
        }

        public async Task<List<StudentEntity>> GetStudentsByCourse(int course)
        {
            var students = await _context.Students.Where(s => s.Course == course).ToListAsync();

            return students;
        }
    }
}
