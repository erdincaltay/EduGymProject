using EduGymProject.Models.EntityFramework;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace EduGymProject.ViewModels
{
    public class PersonalFormViewModels
    {
        public IEnumerable<Departman> Departmanlar { get; set; }

        public Personal Personel { get; set; }
    }
}