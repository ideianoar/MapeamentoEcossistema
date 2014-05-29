﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace MapeamentoEcossistema.WebUI.Models
{
    public enum FormQuestionFieldType : short
    {
        Text = 0,
        MultilineText = 1,
        Date = 2,
        PhoneNumber = 3,
        EmailAddress = 4,
        YesNo = 5,
        Url = 6,
        Integer = 7,
        Currency = 8
    }
}