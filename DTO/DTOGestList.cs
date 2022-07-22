using E_Invitation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Invitation.DTO
{
    public class DTOGestList
    {
        public List<Vacancy> vacancies { get; set; }
        public List<AddGestList> addGestLists { get; set; }
        public Boolean ocassionStatuses { get; set; }

    }
}
