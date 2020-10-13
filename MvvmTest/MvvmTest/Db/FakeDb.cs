using MvvmTest.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace MvvmTest.Db
{
    public class FakeDb
    {
        public FakeDb()
        {
            Init();
        }
        private List<Student> Students;

        // 初始化生成数据
        private void Init()
        {
            Students = new List<Student>();
            for(int i = 0; i < 30; i++)
            {
                Students.Add(new Student()
                {
                    Id = i,
                    Name = $"Sample{i}"
                });
            }
        }

        public List<Student> GetStudents()
        {
            return Students;
        }
        // CUDR
        public void AddStudent(Student stu)
        {
            Students.Add(stu);
        }

        public void DelStudent(int id)
        {
            var model = Students.FirstOrDefault(t => t.Id == id);
            if (model != null)
            {
                Students.Remove(model);
            }

        }

        public List<Student> GetStudentByName(string name)
        {
            return Students.Where(q => q.Name.Contains(name)).ToList();
        }

        public Student GetStudentById(int id)
        {
            var model = Students.FirstOrDefault(t => t.Id == id);
            if (model != null)
            {
                return new Student()
                {
                    Id = model.Id,
                    Name = model.Name
                };
            }
            return null;
        }
    }
}
