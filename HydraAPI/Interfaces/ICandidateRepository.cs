using HydraAPI.Models;

namespace HydraAPI.Interfaces
{
    public interface ICandidateRepository
    {
        List<Candidate> Get();
        List<Candidate> Get(int pageNumber, int pageSize, string fullName, int batchBootacamp);
        List<Candidate> GetCandidate(string courseId, int batchBootcamp);
        Candidate Get(int candidateId);
        int Count(string fullName, int batchBootcamp);
        int Count(int candidateId);
        Candidate Insert(Candidate candidate);
        Candidate Update(Candidate candidate);
        Candidate Delete(Candidate candidate);
    }
}
