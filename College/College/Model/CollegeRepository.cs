namespace College.Model
{
    public static class CollegeRepository
    {
        public static List<Student> Students { get; set; } = new List<Student>()
        {
                new Student
            {
                Id = 1,
                StudentName = "Student 1",
                Email = "student1@gmail.com",
                Address = "Hyd, India"
            },
            new Student
            {
                Id = 2,
                StudentName = "Student 2",
                Email = "student2@gmail.com",
                Address = "Vij, India"
            }
        };
    }
}
