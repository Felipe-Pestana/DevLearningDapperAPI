namespace DevLearning.Api.Models
{
    public class StudentCourse
    {
        public Guid CourseId { get; private set; }
        public Guid StudentId { get; private set; }
        public int Progress { get; private set; }
        public bool Favorite { get; private set; }
        public DateTime StartDate { get; private set; }
        public DateTime LastUpdateDate { get; private set; }

        public StudentCourse(Guid courseId, Guid studentId, int progress, bool favorite)
        {
            CourseId = courseId;
            StudentId = studentId;
            Progress = progress;
            Favorite = favorite;
            StartDate = DateTime.Now;
            LastUpdateDate = DateTime.Now;
        }


    }
}
