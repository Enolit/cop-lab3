using Microsoft.EntityFrameworkCore;

namespace WinFormsApp3
{
    public class StudentRepository
    {
        private readonly StudentContext _context;

        public StudentRepository(StudentContext context)
        {
            _context = context;
        }

        // Получить всех студентов
        public List<Student> GetAll()
        {
            return _context.Students.ToList();
        }

        // Добавить студента
        public void Add(Student student)
        {
            _context.Students.Add(student);
            _context.SaveChanges();
        }

        // Удалить студента
        public void Delete(int studentId)
        {
            var student = _context.Students.Find(studentId);
            if (student != null)
            {
                _context.Students.Remove(student);
                _context.SaveChanges();
            }
        }

        // Редактировать студента
        public void Update(Student student)
        {
            _context.Students.Update(student);
            _context.SaveChanges();
        }
    }
}
