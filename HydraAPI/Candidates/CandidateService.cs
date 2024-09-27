using HydraAPI.Candidates.DTO;
using HydraAPI.Interfaces;

namespace HydraAPI.Candidates
{
    public class CandidateService
    {
        private readonly ICandidateRepository _candidateRepository;

        public CandidateService(ICandidateRepository candidateRepository)
        {
            _candidateRepository = candidateRepository;
        }


        public List<CandidateDTO> Get()
        {
            var model = _candidateRepository.Get()
                .Select(
                    candidate => new CandidateDTO()
                    {
                        CandidateId = candidate.Id,
                        BatchBootcamp = candidate.BootcampClassId,
                        FullName = candidate.FirstName + " " + candidate.LastName,
                        ContactCandidate = candidate.PhoneNumber,
                        Domicile = candidate.Domicile
                    }
                 );
            return model.ToList();
        }
    }
}
