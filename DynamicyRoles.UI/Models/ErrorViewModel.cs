using System.ComponentModel;

namespace DynamicyRoles.UI.Models
{
    public class ErrorViewModel
    {
        [DisplayName("Exception Path")]
        public string ExceptionPath { get; set; }

        [DisplayName("Exception Message")]
        public string ExceptionMessage { get; set; }

        [DisplayName("Exception Stack Trace")]
        public string ExceptionStackTrace { get; set; }
    }
}