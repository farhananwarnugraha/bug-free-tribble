using HydraAPI.Candidates.DTO;
using HydraAPI.Interfaces;
using HydraAPI.Models;
using HydraAPI.Shared;

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

        public CandidateIndexDTO GetCandidate(int pageNumber, int pageSize, string fullName, int batchBootacamp){
            var model = _candidateRepository.Get(pageNumber, pageSize, fullName, batchBootacamp)
            .Select(
                candidate => new CandidateDTO(){
                    CandidateId = candidate.Id,
                    BatchBootcamp = candidate.BootcampClassId,
                    FullName = candidate.FirstName + " " + candidate.LastName,
                    ContactCandidate = candidate.PhoneNumber,
                    Domicile = candidate.Domicile,
                }
            ).ToList();
            return new CandidateIndexDTO(){
                Candidates = model,
                Paginations = new PaginationDTO(){
                    PageNumber = pageNumber,
                    PageSize = pageSize,
                    TotalRows = _candidateRepository.Count(fullName, batchBootacamp)
                },
                FullNames = fullName,
                BootcampBatch = batchBootacamp
            };
        }

        public void Insert(CandidateReqDTO request) => 
            _candidateRepository.Insert(new Candidate{
                BootcampClassId = request.BootcampClass,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                BirthDate = request.BirthDate, 
                Address = request.Address,
                Domicile = request.Domicile,
                PhoneNumber = request.PhoneNumber,
                HasPassed = false,
                IsActive = true
            });

        public CandidateUpdateDTO GetById(int candidateId){
            var model = _candidateRepository.Get(candidateId);
            return new CandidateUpdateDTO(){
                CandidateId = model.Id,
                BootcampClass = model.BootcampClassId,
                FirstName = model.FirstName,
                LastName = model.LastName,
                Gender = model.Gender,
                BirthDate = model.BirthDate,
                Address = model.Address,
                Domicile = model.Domicile,
                PhoneNumber = model.PhoneNumber,
                IsActive = model.IsActive
            };
        }

        public void Update(CandidateUpdateDTO request){
            _candidateRepository.Update(new Candidate{
                Id = request.CandidateId,
                BootcampClassId = request.BootcampClass,
                FirstName = request.FirstName,
                LastName = request.LastName,
                Gender = request.Gender,
                BirthDate = request.BirthDate,
                Address = request.Address,
                Domicile = request.Domicile,
                PhoneNumber = request.PhoneNumber,
                IsActive = request.IsActive
            });
        }
    }
}
