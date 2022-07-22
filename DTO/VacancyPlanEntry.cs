using E_Invitation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Invitation.DTO
{
    public class VacancyPlanEntry
    {
        public List<Vacancy> vacancieslist { get; set; }
        public List<User> Userlist { get; set; }
        public List<VacancyPlan> vacancyPlanslist { get; set; }

        public List<Vacancy> vacancieslistGest { get; set; }
        public List<OcassionStatus> OcassionStatus { get; set; }

    }
}
