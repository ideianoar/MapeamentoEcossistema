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
    
    public partial class Response
    {
        public int Id { get; set; }
        public int QuestionId { get; set; }
        public int UserId { get; set; }
        public System.Guid SessionId { get; set; }
        public System.DateTime CreatedDateTime { get; set; }
    
        public virtual Question Question { get; set; }
        public virtual User User { get; set; }
    }
}
