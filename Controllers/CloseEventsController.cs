using E_Invitation.Models;
using E_Invitation.Repository;
using Microsoft.AspNetCore.Mvc;
using System.Linq;

namespace E_Invitation.Controllers
{
    public class CloseEventsController : Controller
    {
        public readonly RepositoryOcassion _repositoryOcassion;
        public readonly RepositoryGestList _repositoryGestList;
        public readonly RepositoryUser _repositoryUser;
        public CloseEventsController(RepositoryOcassion repositoryOcassion, RepositoryGestList repositoryGestList, RepositoryUser repositoryUser)
        {

            _repositoryOcassion = repositoryOcassion;
            _repositoryGestList = repositoryGestList;
            _repositoryUser = repositoryUser;

        }
        public IActionResult Index()
        {
            ViewBag.AllOcassion = _repositoryOcassion.GetAllDeActive().Where(c => c.Id != 0);
            var AllData = _repositoryUser.GetAll(1);
            AllData.Insert(0, new User { Id = 0, UserName = "--All --" });
            ViewBag.AllUser = AllData;

            return View();
        }
    }
}
