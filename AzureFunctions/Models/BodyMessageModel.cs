using System;
using System.Collections.Generic;
using System.Collections.ObjectModel;
using System.Text;

namespace AzureFunctions.Models
{
    public class BodyMessageModel
    {
        public string TargetDeviceID { get; set; }
        public string Message { get; set; }
    }
}