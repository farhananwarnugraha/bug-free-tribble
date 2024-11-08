using System;
using HydraAPI.EvaluationCandidate.DTO;
using HydraAPI.Interfaces;
using HydraAPI.Shared;

namespace HydraAPI.EvaluationCandidate;

public class EvaluationCandidateService
{
    private readonly IEvaluationCandidate _evaluationCandidateRepository;

    public EvaluationCandidateService(IEvaluationCandidate evaluationCandidateRepository)
    {
        _evaluationCandidateRepository = evaluationCandidateRepository;
    }

    public EvaluationCandidateResponseDTO GetEvaluation(int pageNumber, int pageSize, string fullName){
        var data = _evaluationCandidateRepository.GetCandidateEavaluations(pageNumber, pageSize, fullName)
                    .Select(ec => new EvaluationCandidateDTO{
                        FullName = ec.Candidate.FirstName + " " + ec.Candidate.LastName,
                        CourseName = ec.Course.TrainerSkillDetail.Skill.Name,
                        Mark = ec.Mark,
                        IsPassed = ec.Passed == true ? "Passed" : "Failed"
                    }).ToList();

        return new EvaluationCandidateResponseDTO(){
            EvaluationCandidates = data,
            Paginations = new PaginationDTO(){
                PageNumber = pageNumber,
                PageSize = pageSize,
                TotalRows = _evaluationCandidateRepository.Count(fullName)
            },
            FullName = fullName,
        };
    }
}
