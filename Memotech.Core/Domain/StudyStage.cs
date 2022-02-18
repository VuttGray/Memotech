namespace Memotech.Core.Domain
{
    public class StudyStage
    {
        public DateTime Date { get; set; } = DateTime.Now;
        public bool IsSuccessful { get; set; } = false;
    }
}
