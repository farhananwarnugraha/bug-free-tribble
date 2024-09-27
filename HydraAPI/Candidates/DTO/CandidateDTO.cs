namespace HydraAPI.Candidates.DTO
{
    public class CandidateDTO
    {
        public int CandidateId { get; set; }
        public string FullName { get; set; } = null!;
        public int BatchBootcamp { get; set; }
        public string ContactCandidate { get; set; } = null!;
        public string Domicile { get; set; } = null!;
    }
}
