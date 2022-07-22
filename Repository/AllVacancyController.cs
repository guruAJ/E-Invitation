using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using E_Invitation.DTO;
using E_Invitation.Models;
using Microsoft.AspNetCore.Mvc;

namespace E_Invitation.Repository
{
    public class AllVacancyController : Controller
    {
        
        public readonly RepositoryOcassion _repositoryOcassion;
        public readonly RepositoryGestList _repositoryGestList;
        public readonly RepositoryUser _repositoryUser;
        public AllVacancyController(RepositoryOcassion repositoryOcassion, RepositoryGestList repositoryGestList, RepositoryUser repositoryUser)
        {

            _repositoryOcassion = repositoryOcassion;
            _repositoryGestList = repositoryGestList;
            _repositoryUser = repositoryUser;

        }
        public IActionResult Index()
        {
            ViewBag.AllOcassion = _repositoryOcassion.GetAllActive().Where(c => c.Id != 0);
            var AllData= _repositoryUser.GetAll(1);
            AllData.Insert(0, new User { Id = 0, UserName = "--All --" });
            ViewBag.AllUser = AllData;

            return View();
        }
        public IActionResult CloseEvents()
        {
            ViewBag.AllOcassion = _repositoryOcassion.GetAllDeActive().Where(c => c.Id != 0);
            var AllData = _repositoryUser.GetAll(1);
            AllData.Insert(0, new User { Id = 0, UserName = "--All --" });
            ViewBag.AllUser = AllData;

            return View();
        }
        public IActionResult GetGestByUserId(int OcassionId,int UserId)
        {

            DTOGestList dTOGestList = new DTOGestList();





            List<AddGestList> addGestList = new List<AddGestList>();
            AddGestList Db = new AddGestList();


            addGestList = _repositoryGestList.GetAllOcassionby(OcassionId, UserId);


            dTOGestList.addGestLists = addGestList.OrderByDescending(x => x.UserId).ToList();




            return Json(dTOGestList);

        }
    }
}
