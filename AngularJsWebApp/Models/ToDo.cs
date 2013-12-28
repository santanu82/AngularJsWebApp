﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace AngularJsWebApp.Models
{
    public class ToDo
    {
        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime? DueDate { get; set; }

        public int Priority { get; set; }
    }
}