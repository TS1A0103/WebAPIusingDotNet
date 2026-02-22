using Microsoft.EntityFrameworkCore;

namespace College.Data.Repository
{
    public class StudentRepository : IStudentRepository
    {
        private readonly CollegeDBContext _dbContext;
        public StudentRepository(CollegeDBContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> CreateAsync(Student student)
        {
            await _dbContext.Students.AddAsync(student);
            await _dbContext.SaveChangesAsync();
            return student.Id;
        }

        public async Task<bool> DeleteAsync(int id)
        {
            var studentToDelete =await _dbContext.Students.Where(student => student.Id == id).FirstOrDefaultAsync();
            if (studentToDelete == null)
                throw new ArgumentException($"No Student Found with {id}");

            _dbContext.Students.Remove(studentToDelete);
            await _dbContext.SaveChangesAsync();

            return true;
        }

        public async Task<List<Student>> GetAllAsync()
        {
            return await _dbContext.Students.ToListAsync();
        }

        public async Task<Student> GetByIdAsync(int id)
        {
            return await _dbContext.Students.Where(student => student.Id == id).FirstOrDefaultAsync();
        }

        public async Task<Student> GetByNameAsync(string name)
        {
            return await _dbContext.Students.Where(student => student.StudentName.ToLower().Equals(name.ToLower())).FirstOrDefaultAsync();
        }

        public async Task<int> UpdateAsync(Student student)
        {
            var studentToUpdate = await _dbContext.Students
                .FirstOrDefaultAsync(s => s.Id == student.Id);

            if (studentToUpdate == null)
                throw new ArgumentException($"No Student Found with {student.Id}");

            studentToUpdate.StudentName = student.StudentName;
            studentToUpdate.Email = student.Email;
            studentToUpdate.Address = student.Address;
            studentToUpdate.DOB = student.DOB;

            await _dbContext.SaveChangesAsync();
            return student.Id;
        }
    }
}
