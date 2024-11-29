using BCrypt.Net;
using HydraAPI.Interfaces;
using HydraAPI.Models;
using Microsoft.EntityFrameworkCore;
using System.Reflection.Metadata.Ecma335;

namespace HydraAPI.Repository
{
    public class CandidateRepository : ICandidateRepository
    {
        private readonly HydraContext _candidateContext;

        public CandidateRepository(HydraContext candidateContext)
        {
            _candidateContext = candidateContext;
        }

        public int Count(string fullName, int batchBootcamp)
        {
            return _candidateContext.Candidates
                .Where(
                    candidate => (candidate.FirstName + candidate.LastName).ToLower().Contains(fullName ?? "".ToLower()) &&
                    (batchBootcamp == 0 || candidate.BootcampClassId == batchBootcamp)
                )
            .Count();
        }

        public int Count(int candidateId)
        {
            throw new NotImplementedException();
        }

        public Candidate Delete(Candidate candidate)
        {
            _candidateContext.Remove(candidate);
            _candidateContext.SaveChanges();
            return candidate;
        }

        public List<Candidate> Get()
        {
            return _candidateContext.Candidates.ToList();
        }

        public List<Candidate> Get(int pageNumber, int pageSize, string fullName, int batchBootacamp)
        {
            var candidateModel = _candidateContext.Candidates
                .Where(
                    candidate => (candidate.FirstName + candidate.LastName).ToLower().Contains(fullName ?? "".ToLower()) &&
                    (batchBootacamp == 0 || candidate.BootcampClassId == batchBootacamp)
                )
                .Skip((pageNumber - 1) * pageSize)
                .Take(
                    pageSize
                );
            return candidateModel.ToList();
        }

        public Candidate Get(int candidateId)
        {
            return _candidateContext.Candidates.Find(candidateId) ?? throw new Exception("Candidate Not Found");
        }

        public List<Candidate> GetCandidate(string courseId, int batchBootcamp)
        {
            return _candidateContext.Candidates
                .Include(c => c.BootcampClass)
                    .ThenInclude(bc => bc.Courses)
                .Where(c => c.BootcampClassId == batchBootcamp && c.BootcampClass.Courses.Any(c => c.Id == courseId))
                .ToList();
        }

        public Candidate Insert(Candidate candidate)
        {
            _candidateContext.Add(candidate);
            _candidateContext.SaveChanges();
            return candidate;

        }

        public Candidate Update(Candidate candidate)
        {
            _candidateContext.Update(candidate);
            _candidateContext.SaveChanges();
            return candidate;
        }
    }
}
