using E_Invitation.Data;
using E_Invitation.DTO;
using E_Invitation.Helpers;
using E_Invitation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
 
namespace E_Invitation.Repository
{
    public class RepositoryVacancyReport
    {
        public readonly ApplicationDbContext _context;
        
        public RepositoryVacancyReport(ApplicationDbContext context)
        {
            _context = context;
            
        }

        //public List<AddGestList> GetAll(AddGestList db)
        //{
        //    var AllData = _context.addGestLists.Where(i => i.UserId == db.UserId && i.EnclosureId==db.EnclosureId && i.RankId==db.RankId && i.IsActive==true).OrderByDescending(x => x.EnclosureId).ToList();


        //    return AllData;
        //}
       
        public VacancyPlanEntry GetAll(int Id, int UserId)
        {
            //var AllData = _context.vacancies.Where(i => i.IsActive == 1).OrderByDescending(x => x.CreatedOn).ToList();

            var result = from VacancyPlan in _context.vacancyPlans
                         join Ocassion in _context.Ocassions on VacancyPlan.OcassionId equals Ocassion.Id into Oc
                         from Ocassion in Oc.DefaultIfEmpty()
                         join Enclosure in _context.enclosures on VacancyPlan.EnclosureId equals Enclosure.Id into En
                         from Enclosure in En.DefaultIfEmpty()
                         join Rank in _context.ranks on VacancyPlan.RankId equals Rank.Id into Rnk
                         from Rank in Rnk.DefaultIfEmpty()
                             //join Category in _context.categories on Vacancy.CategoryId equals Category.Id into cat
                             //from Category in cat.DefaultIfEmpty()
                         where Ocassion.Id == Id && VacancyPlan.UserId == UserId
                         select new
                         {
                             Id = VacancyPlan.Id,
                             OcassionId = VacancyPlan.OcassionId,
                             OcassionName = Ocassion.OcassionName,
                             EnclosureId = VacancyPlan.EnclosureId,
                             EnclosureName = Enclosure.Title,
                             EnclosureColor = Enclosure.ColorCode,
                             //CategoryId = Vacancy.CategoryId,
                             //CategoryName = Category.Title,
                             RankId = VacancyPlan.RankId,
                             RankName = Rank.Title,
                             Total = VacancyPlan.Total,
                             //CreatedOn = Vacancy.CreatedOn,
                             //UpdatedOn = Vacancy.UpdatedOn,
                             //IsActive = Vacancy.IsActive,
                             //CreatedBy = Vacancy.CreatedBy

                         };

            VacancyPlanEntry mainlst = new VacancyPlanEntry();
            List<Vacancy> lst = new List<Vacancy>();
            foreach (var db in result)
            {
                Vacancy vacancy = new Vacancy();


                vacancy.Id = db.Id;
                vacancy.OcassionId = db.OcassionId;
                vacancy.OcassionName = db.OcassionName;
                vacancy.EnclosureId = db.EnclosureId;
                vacancy.EnclosureName = db.EnclosureName;
                vacancy.EnclosureColor = db.EnclosureColor;
                //vacancy.CategoryId = db.CategoryId;
                //vacancy.CategoryName = db.CategoryName;
                vacancy.RankId = db.RankId;
                vacancy.RankName = db.RankName;
                vacancy.Total = db.Total;
                //vacancy.CreatedOn = db.CreatedOn;
                //vacancy.UpdatedOn = db.UpdatedOn;
                //vacancy.IsActive = db.IsActive;
                //vacancy.CreatedBy = db.CreatedBy;
                lst.Add(vacancy);
            }
            mainlst.vacancieslist = lst.OrderBy(n => n.EnclosureName).ToList();



            var result1 = from AddGestList in _context.addGestLists
                          join Ocassion in _context.Ocassions on AddGestList.OcassionId equals Ocassion.Id into oc
                          from Ocassion in oc.DefaultIfEmpty()
                          join Enclosure in _context.enclosures on AddGestList.EnclosureId equals Enclosure.Id into En
                         from Enclosure in En.DefaultIfEmpty()
                        
                         join Rank in _context.ranks on AddGestList.RankId equals Rank.Id into rnk
                         from Rank in rnk.DefaultIfEmpty()
                         where Ocassion.Id == Id &&  AddGestList.UserId==UserId && AddGestList.IsActive==true
                          group AddGestList by new
                         {
                             Id=AddGestList.Id,
                             OcassionId = AddGestList.OcassionId,
                             OcassionName = Ocassion.OcassionName,
                             EnclosureId = AddGestList.EnclosureId,
                             EnclosureName = Enclosure.Title,
                             EnclosureColor = Enclosure.ColorCode,
                             //CategoryId = Vacancy.CategoryId,
                             //CategoryName = Category.Title,
                             RankId = AddGestList.RankId,
                             RankName = Rank.Title
                              
                         } into grouped

                         select new { OcassionId = grouped.Key.OcassionId,
                             OcassionName=grouped.Key.OcassionName,
                             EnclosureId = grouped.Key.EnclosureId,
                             EnclosureName=grouped.Key.EnclosureName,
                             EnclosureColor=grouped.Key.EnclosureColor,
                             RankId = grouped.Key.RankId,
                             RankName=grouped.Key.RankName,
                             Count = grouped.Count()
                         };

            List<Vacancy> lst1 = new List<Vacancy>();
            foreach (var db in result1)
            {
                Vacancy vacancy = new Vacancy();


              
                vacancy.OcassionId = db.OcassionId;
                vacancy.OcassionName = db.OcassionName;
                vacancy.EnclosureId = db.EnclosureId;
                vacancy.EnclosureName = db.EnclosureName;
                vacancy.EnclosureColor = db.EnclosureColor;
                //vacancy.CategoryId = db.CategoryId;
                //vacancy.CategoryName = db.CategoryName;
                vacancy.RankId = db.RankId;
                vacancy.RankName = db.RankName;
                vacancy.Total = db.Count;
                //vacancy.CreatedOn = db.CreatedOn;
                //vacancy.UpdatedOn = db.UpdatedOn;
                //vacancy.IsActive = db.IsActive;
                //vacancy.CreatedBy = db.CreatedBy;
                lst1.Add(vacancy);
            }
            mainlst.vacancieslistGest = lst1.OrderBy(n => n.EnclosureName).ToList();





            return mainlst;


        }

        public VacancyPlanEntry VacancyRecall(int Id, int UserId)
        {
            var result = from VacancyPlan in _context.vacancyPlans
                         join Ocassion in _context.Ocassions on VacancyPlan.OcassionId equals Ocassion.Id into Oc
                         from Ocassion in Oc.DefaultIfEmpty()
                         join Enclosure in _context.enclosures on VacancyPlan.EnclosureId equals Enclosure.Id into En
                         from Enclosure in En.DefaultIfEmpty()
                         join Rank in _context.ranks on VacancyPlan.RankId equals Rank.Id into Rnk
                         from Rank in Rnk.DefaultIfEmpty()
                             //join Category in _context.categories on Vacancy.CategoryId equals Category.Id into cat
                             //from Category in cat.DefaultIfEmpty()
                         where Ocassion.Id == Id && VacancyPlan.UserId == UserId
                         select new
                         {
                             Id = VacancyPlan.Id,
                             OcassionId = VacancyPlan.OcassionId,
                            
                             EnclosureId = VacancyPlan.EnclosureId,
                            
                             RankId = VacancyPlan.RankId,
                            
                             Total = VacancyPlan.Total,
                             //CreatedOn = Vacancy.CreatedOn,
                             //UpdatedOn = Vacancy.UpdatedOn,
                             //IsActive = Vacancy.IsActive,
                             //CreatedBy = Vacancy.CreatedBy

                         };

            VacancyPlanEntry mainlst = new VacancyPlanEntry();
            List<Vacancy> lst = new List<Vacancy>();
            foreach (var db in result)
            {
                Vacancy vacancy = new Vacancy();


                vacancy.Id = db.Id;
                vacancy.OcassionId = db.OcassionId;
             
                vacancy.EnclosureId = db.EnclosureId;
              
                vacancy.RankId = db.RankId;
              
                vacancy.Total = db.Total;
                
                lst.Add(vacancy);
            }
            mainlst.vacancieslist = lst.OrderBy(n => n.EnclosureName).ToList();



            var result1 = from AddGestList in _context.addGestLists
                          join Ocassion in _context.Ocassions on AddGestList.OcassionId equals Ocassion.Id into oc
                          from Ocassion in oc.DefaultIfEmpty()
                          join Enclosure in _context.enclosures on AddGestList.EnclosureId equals Enclosure.Id into En
                          from Enclosure in En.DefaultIfEmpty()

                          join Rank in _context.ranks on AddGestList.RankId equals Rank.Id into rnk
                          from Rank in rnk.DefaultIfEmpty()
                          where Ocassion.Id == Id && AddGestList.UserId == UserId && AddGestList.IsActive==true
                          group AddGestList by new
                          {
                              Id = AddGestList.Id,
                              OcassionId = AddGestList.OcassionId,
                              
                              EnclosureId = AddGestList.EnclosureId,
                             
                              RankId = AddGestList.RankId,
                             

                          } into grouped

                          select new
                          {
                              OcassionId = grouped.Key.OcassionId,
                             
                              EnclosureId = grouped.Key.EnclosureId,
                             
                              RankId = grouped.Key.RankId,
                              
                              Count = grouped.Count()
                          };

            List<Vacancy> lst1 = new List<Vacancy>();
            foreach (var db in result1)
            {
                Vacancy vacancy = new Vacancy();



                vacancy.OcassionId = db.OcassionId;
              
                vacancy.EnclosureId = db.EnclosureId;
              
                vacancy.RankId = db.RankId;
               
                vacancy.Total = db.Count;
              
                lst1.Add(vacancy);
            }
            mainlst.vacancieslistGest = lst1.OrderBy(n => n.EnclosureName).ToList();

            foreach (var db in mainlst.vacancieslist)
            {
                int count = 0;
               
                foreach (var db1 in mainlst.vacancieslistGest)
                {

                    if (db.EnclosureId == db1.EnclosureId && db.RankId == db1.RankId)
                    {
                        count = count + 1;

                    }
                    
                }
                if (count < db.Total)
                {


                    var query =
                       from vanplan in _context.vacancyPlans
                       where vanplan.UserId== UserId  && vanplan.OcassionId == Id && vanplan.EnclosureId==db.EnclosureId && vanplan.RankId==db.RankId
                       select vanplan;


                    foreach (VacancyPlan ord in query)
                    {


                        ord.Total = count;



                        // Insert any additional changes to column values.
                    }

                    // Submit the changes to the database.
                    try
                    {
                        
                        _context.SaveChanges();
                       
                    }
                    catch (Exception e)
                    {
                        
                        Console.WriteLine(e);
                        // Provide for exceptions.
                    }





                }
            }
            return mainlst;

        }
    }
}
