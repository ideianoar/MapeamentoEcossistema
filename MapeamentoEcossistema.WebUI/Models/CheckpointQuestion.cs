//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated from a template.
//
//     Manual changes to this file may cause unexpected behavior in your application.
//     Manual changes to this file will be overwritten if the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace MapeamentoEcossistema.WebUI.Models
{
    using System;
    using System.Collections.Generic;
    
    public partial class CheckpointQuestion : Question
    {
        public string PrimaryPlaceholderText { get; set; }
        public string SecondaryPlaceholderText { get; set; }
        public bool IsPrimaryQuestionMandatory { get; set; }
        public bool IsSecondaryQuestionMandatory { get; set; }
        public string ExampleText { get; set; }
        public string ProblemText { get; set; }
    }
}