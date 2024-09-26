using System;
using System.Collections.Generic;

namespace FinalAPI2024_HTTPS.Models
{
    public partial class Login
    {
        public int IdLog { get; set; }
        public string CorreoLog { get; set; }
        public string PasswdLog { get; set; }
        public DateTime? Fecha { get; set; }
        public string Rol { get; set; }
    }
}
