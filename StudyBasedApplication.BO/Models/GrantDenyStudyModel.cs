using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace StudyBasedApplication.BO.Models
{
    public class GrantDenyStudyModel
    {
        public List<StudyModel> studymodelGranted { get; set; }
        public List<StudyModel> studymodelDenied { get; set; }
    }
}