using System;
using System.Data.Entity;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using MapeamentoEcossistema.WebUI.Models;
using MapeamentoEcossistema.WebUI.ViewModels;
using MapeamentoEcossistema.WebUI.Code;

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
            // O paraâmetro passado é na verdade o alias... recupera o id.
            var questionnaireId = _context.Questionnaires
                .Where(q => q.Alias == id).Select(q => q.Id)
                .FirstOrDefault();
            
            // Se não foi encontrado, retorna para a home.
            if (questionnaireId == 0)
            {
                return RedirectToAction("Select", "Access");
            }

            // Recupera o questionário ordenado.
            var questionnaire = GetQuestionnaire(questionnaireId);

            // Cria um objeto de resposta para cada pergunta.
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
            var dbQuestionnaire = GetQuestionnaire(questionnaireId);

            ModelState.Clear();
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

        // Private methods.
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

                    // Adiciona a resposta.
                    response.CreatedDateTime = DateTime.UtcNow;
                    response.Question = q;
                    response.SessionId = sessionId;
                    response.UserId = Int32.Parse(User.Identity.Name);
                    q.Responses.Add(response);

                    // Valida a resposta e adiciona a ModelState.
                    int groupIndex, questionIndex, responseIndex;
                    GetIndexes(myQuestionnaire, g, q, response, out groupIndex, out questionIndex, out responseIndex);
                    SanitizeAndValidateReponse(response, q, groupIndex, questionIndex, responseIndex);
                }
            }
        }
        private void SanitizeAndValidateReponse(Response response, Question question, int groupIndex, int questionIndex, int responseIndex)
        {
            var basePropName = String.Format("QuestionGroups[{0}].Questions[{1}].Responses[{2}].",
                groupIndex,
                questionIndex,
                responseIndex);

            if (response is FormResponse)
            {
                var formQuestion = question as FormQuestion;
                var formResponse = response as FormResponse;

                if (String.IsNullOrWhiteSpace(formResponse.ResponseText))
                {
                    // Validação de campo vazio.
                    if (formQuestion.IsMandatory)
                    {
                        ModelState.AddModelError(basePropName + "ResponseText",
                            String.Format("Este campo é obrigatório.", formQuestion.Title));
                    }
                }
                else
                {
                    formResponse.ResponseText = formResponse.ResponseText.Trim();

                    switch (formQuestion.FieldType)
                    {
                        case FormQuestionFieldType.Text:
                        case FormQuestionFieldType.MultilineText:
                        {
                            if (formResponse.ResponseText.Length > 4000)
                                ModelState.AddModelError(basePropName + "ResponseText",
                                    "Este campo não pode conter mais de 4 mil caracteres.");
                            break;
                        }

                        case FormQuestionFieldType.Date:
                        {
                            DateTime dt;
                            if (!DateTime.TryParse(formResponse.ResponseText, out dt))
                                ModelState.AddModelError(basePropName + "ResponseText",
                                    "Informe uma data no formato dd/mm/aaaa.");
                            break;
                        }

                        case FormQuestionFieldType.PhoneNumber:
                            // TODO: adicionar validação.
                            break;

                        case FormQuestionFieldType.EmailAddress:
                            if (!EmailValidator.IsValid(formResponse.ResponseText))
                                ModelState.AddModelError(basePropName + "ResponseText",
                                    "Informe um e-mail válido.");
                            break;
                    }
                }
            }
            
            else if (response is CheckpointResponse)
            {
                var checkpointQuestion = question as CheckpointQuestion;
                var checkpointResponse = response as CheckpointResponse;

                // TODO: Validação aqui.
            }
        }
        private void GetIndexes(Questionnaire questionnaire, QuestionGroup group, Question question, Response response, out int groupIndex, out int questionIndex, out int responseIndex)
        {
            groupIndex = questionnaire.QuestionGroups.IndexOf(group);
            questionIndex = group.Questions.IndexOf(question);
            responseIndex = question.Responses.IndexOf(response);
        }
        private Questionnaire GetQuestionnaire(int id)
        {
            var questionnaire = _context.Questionnaires
                .Include(q => q.QuestionGroups)
                .Include(q => q.QuestionGroups.Select(g => g.Questions))
                .First(item => item.Id == id);

            questionnaire.QuestionGroups = questionnaire.QuestionGroups
                .OrderBy(g => g.SortOrder).ToList();

            questionnaire.QuestionGroups.Select(g => { g.Questions = g.Questions
                .OrderBy(q => q.SortOrder).ToList(); return 0; });

            return questionnaire;
        }
    }
}
