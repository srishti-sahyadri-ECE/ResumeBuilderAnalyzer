namespace ResumeBuilderAnalyzer.Models
{
    public class Resume
    {
        public string FullName { get; set; }
        public string Email { get; set; }
        public string Phone { get; set; }
        public string Skills { get; set; } // comma-separated
        public string Experience { get; set; }
        public string Education { get; set; }
    }
}
