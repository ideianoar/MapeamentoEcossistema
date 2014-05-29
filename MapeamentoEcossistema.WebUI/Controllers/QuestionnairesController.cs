using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MapeamentoEcossistema.WebUI.Models;
using MapeamentoEcossistema.WebUI.ViewModels;

namespace MapeamentoEcossistema.WebUI.Controllers
{
    [Authorize]
    public class QuestionnairesController : Controller
    {
        // Fields.
        private readonly MapeamentoEntities _context;

        // Constructors.
        public QuestionnairesController(MapeamentoEntities context)
        {
            _context = context;
        }

        // Actions.
        public ActionResult Index(string id)
        {
            var questionnaire = _context.Questionnaires
                .Include(q => q.QuestionGroups)
                .Include(q => q.QuestionGroups.Select(g => g.Questions))
                .FirstOrDefault(q => q.Alias == id);

            if (questionnaire == null)
            {
                return RedirectToAction("Select", "Access");
            }

            foreach (var item in questionnaire.QuestionGroups.Select(g => g.Questions))
            {
                foreach (var q in item)
                {
                    var response = q is CheckpointQuestion
                        ? new CheckpointResponse() { YesNoFlag = true } as Response
                        : new FormResponse() as Response;

                    response.Question = q;
                    response.QuestionId = q.Id;
                    q.Responses.Add(response);
                }
            }

            return View(questionnaire);
        }
        [HttpPost, Authorize]
        public ActionResult Index(Questionnaire questionnaire)
        {
            var sessionId = Guid.NewGuid();
            var questionnaireId = Int32.Parse(Request["QuestionnaireId"]);

            var dbQuestionnaire = _context.Questionnaires
                .Include(q => q.QuestionGroups)
                .Include(q => q.QuestionGroups.Select(g => g.Questions))
                .First(item => item.Id == questionnaireId);

            MapResponses(dbQuestionnaire, questionnaire, sessionId);

            if (!ModelState.IsValid)
            {
                return View(dbQuestionnaire);
            }

            if (_context.SaveChanges() == 0)
            {
                ModelState.AddModelError("", "Erro ao salvar os dados no banco de dados.");
                return View(dbQuestionnaire);
            }

            return RedirectToAction("Confirmation");
        }

        private void MapResponses(Questionnaire myQuestionnaire, Questionnaire questionnaire, Guid sessionId)
        {
            // Faz o mapeamento das respostas.
            foreach (var g in myQuestionnaire.QuestionGroups)
            {
                var correspondingGroup = questionnaire.QuestionGroups.Single(cg => cg.Id == g.Id);

                foreach (var q in g.Questions)
                {
                    var correspondingQuestion = correspondingGroup.Questions.Single(cq => cq.Id == q.Id);
                    var response = correspondingQuestion.Responses.First();

                    response.CreatedDateTime = DateTime.UtcNow;
                    response.Question = q;
                    response.SessionId = sessionId;
                    response.UserId = Int32.Parse(User.Identity.Name);

                    ValidateReponse(response, q);
                    q.Responses.Add(response);
                }
            }
        }
        private void ValidateReponse(Response response, Question question)
        {
            if (response is FormResponse)
            {
                var formQuestion = question as FormQuestion;
                var formResponse = response as FormResponse;

                if (formQuestion.IsMandatory && String.IsNullOrWhiteSpace(formResponse.ResponseText))
                {
                    ModelState.AddModelError("", String.Format("A pergunta '{0}' é obrigatória.", formQuestion.Title));
                }
            }
            
            else if (response is CheckpointResponse)
            {
                var checkpointQuestion = question as CheckpointQuestion;
                var checkpointResponse = response as CheckpointResponse;

                // TODO: Validação aqui.
            }
        }
    }
}
