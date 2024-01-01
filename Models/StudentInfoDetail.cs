namespace  ReactWeb.Models
{
    public class StudentInfoDetail
    {
        public long ID { get; set; }
        public long GuardianID { get; set; }
        public string? StudentFatherName { get; set; }
        public string? StudentMotherName { get; set; }
        public string? StudentGuardianName { get; set; }
        public string? StudentFatherMobile { get; set; }
        public string? StudentMotherMobile { get; set; }
        public string? StudentGuardianMobile { get; set; }
        public string? GuardianPresentAddress { get; set; }
        
        
    }
}
