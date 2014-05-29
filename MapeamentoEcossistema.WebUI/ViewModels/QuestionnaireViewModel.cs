using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using MapeamentoEcossistema.WebUI.Models;

namespace MapeamentoEcossistema.WebUI.ViewModels
{
    public class QuestionnaireViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public string Alias { get; set; }
        public IList<QuestionGroupViewModel> QuestionGroups { get; set; }
        public QuestionnaireViewModel()
        {
            QuestionGroups = new List<QuestionGroupViewModel>();
        }
    }
    public class QuestionGroupViewModel
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Description { get; set; }
        public short SortOrder { get; set; }
        public short Number { get; set; }
        public IList<QuestionViewModel> Questions { get; set; }
        public QuestionGroupViewModel()
        {
            this.Questions = new List<QuestionViewModel>();
        }
    }

    // Questions.
    public class QuestionViewModel
    {
        public int Id { get; set; }
        public int QuestionGroupId { get; set; }
        public string Title { get; set; }
        public short SortOrder { get; set; }
    }
    public class CheckpointQuestionViewModel : QuestionViewModel
    {
        public string PrimaryPlaceholderText { get; set; }
        public string SecondaryPlaceholderText { get; set; }
        public string ExampleText { get; set; }
        public string ProblemText { get; set; }
        public bool IsPrimaryQuestionMandatory { get; set; }
        public bool IsSecondaryQuestionMandatory { get; set; }
        public ResponseViewModel Response { get; set; }
    }
    public class FormQuestionViewModel : QuestionViewModel
    {
        public FormQuestionFieldType FieldType { get; set; }
        public bool IsMandatory { get; set; }
    }

    // Response.
    public class ResponseViewModel
    {
        public Guid SessionId { get; set; }
    }
    public class CheckpointResponseViewModel : ResponseViewModel
    {
        public bool YesNoFlag { get; set; }
        public string PrimaryResponseText { get; set; }
        public string SecondaryResponseText { get; set; }
    }
    public class FormResponseViewModel : ResponseViewModel
    {
        public string ResponseText { get; set; }
    }
}