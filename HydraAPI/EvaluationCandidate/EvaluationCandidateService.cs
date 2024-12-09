using System;
using HydraAPI.EvaluationCandidate.DTO;
using HydraAPI.Interfaces;
using HydraAPI.Models;
using HydraAPI.Shared;

namespace HydraAPI.EvaluationCandidate;

public class EvaluationCandidateService
{
    private readonly IEvaluationCandidate _evaluationCandidateRepository;
    private readonly ICandidateRepository _candidateRepository;

    public EvaluationCandidateService(IEvaluationCandidate evaluationCandidateRepository, ICandidateRepository candidateRepository)
    {
        _evaluationCandidateRepository = evaluationCandidateRepository;
        _candidateRepository = candidateRepository;
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

    public void Inserts(AddEvaliationCandidateDTO request){
        if(request.CourseId == null || request.CandidateId == null || request.Marks == null ){
            throw new ArgumentException("All fields are required");
        }

        // maping dto to model
        var evaluationCandidate = request.CandidateId.Select((courseId, index) => new CandidateEvaluation{
            CourseId = courseId.ToString(),
            CandidateId = request.CandidateId[index],
            Mark = request.Marks[index],
            Notes = request.Notes[index],
            Passed = request.Marks[index] >= 50 ? true : false

        }).ToList();

        _evaluationCandidateRepository.Insert(evaluationCandidate);
        // _evaluationCandidateRepository.Insert(new List<CandidateEvaluation>{
        //     foreach (var item in request.CandidateId)
        //     {
                
        //     }
        //     CandidateId = request.CandidateId[0],
        //     CourseId = request.CourseId[0],
        //     Mark = request.Marks[0],
        // });

            // foreach (var item in new List<AddEvaliationCandidateDTO>()){
            //     item.Marks = request.Marks;
            //     item.CandidateId = request.CandidateId;
            //     item.CourseId = request.CourseId;
            // }

            // foreach (var item in new List<CandidateEvaluation>())
            // {
            //     item.CandidateId = request.CandidateId[0];
            //     item.CourseId = request.CourseId[0];
            //     item.Mark = request.Marks[0];
            // }
            
    }

    public void Insert (EvaluationCandidateReqDTO modelRequest){
        var model = new CandidateEvaluation{
            CourseId = modelRequest.CourseId,
            CandidateId = modelRequest.CandidateId,
            Mark = modelRequest.Mark,
            Notes = modelRequest.Note,
            Passed = modelRequest.Mark >= 50 ? true : false
        };

        if( model.Mark >=50 ){
            _evaluationCandidateRepository.Insert(model);
        }
        else{
            _evaluationCandidateRepository.Insert(model);
            var candidateModel = _candidateRepository.Get(model.CandidateId);
            candidateModel.IsActive = false;
            _candidateRepository.Update(candidateModel);
        }
        // _evaluationCandidateRepository.Insert(new CandidateEvaluation{
        //     CourseId = modelRequest.CourseId,
        //     CandidateId = modelRequest.CandidateId,
        //     Mark = modelRequest.Mark,
        //     Notes = modelRequest.Note,
        //     Passed = modelRequest.Mark >= 50 ? true : false
        // });

        
    }
}
