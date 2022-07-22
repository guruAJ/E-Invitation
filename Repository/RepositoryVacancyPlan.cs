using E_Invitation.Data;
using E_Invitation.DTO;
using E_Invitation.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Invitation.Repository
{
    public class RepositoryVacancyPlan
    {
        public readonly ApplicationDbContext _context;

        public RepositoryVacancyPlan(ApplicationDbContext context)
        {
            _context = context;

        }
         
        public async Task<int> SaveAsync(VacancyPlan Db)
        {

            var result = from Enclosure in _context.enclosures
                         join Vacancy in _context.vacancies on Enclosure.Id equals Vacancy.EnclosureId into En
                         from Vacancy in En.DefaultIfEmpty()
                         join Ocassion in _context.Ocassions on Vacancy.OcassionId equals Ocassion.Id into oc
                         from Ocassion in oc.DefaultIfEmpty()
                         join Rank in _context.ranks on Vacancy.RankId equals Rank.Id into rnk
                         from Rank in rnk.DefaultIfEmpty()
                         where Ocassion.Id == Db.OcassionId && Vacancy.IsActive == 1
                         group Vacancy by new
                         {
                             Id = Ocassion.Id,
                             EnclosureId=Vacancy.EnclosureId,
                             RankId=Rank.Id,
                         } into grouped

                         select new { Id = grouped.Key.Id, EnclosureId = grouped.Key.EnclosureId, RankId = grouped.Key.RankId, Count = grouped.Sum(t => t.Total) };

            var tot = result.Where(t => t.EnclosureId == Db.EnclosureId && t.RankId == Db.RankId);

            var allotresult= from VacancyPlan in _context.vacancyPlans
                             join Ocassion in _context.Ocassions on VacancyPlan.OcassionId equals Ocassion.Id into oc
                             from Ocassion in oc.DefaultIfEmpty()
                             where Ocassion.Id == Db.OcassionId && VacancyPlan.UserId!=Db.UserId
                             group VacancyPlan by new
                             {
                               
                                 Id = Ocassion.Id,
                                 EnclosureId = VacancyPlan.EnclosureId,
                                 RankId = VacancyPlan.RankId,
                             } into grouped

                             select new { Id = grouped.Key.Id, EnclosureId = grouped.Key.EnclosureId, RankId = grouped.Key.RankId, Count = grouped.Sum(t => t.Total) };

            var tot1 = allotresult.Where(t => t.EnclosureId == Db.EnclosureId && t.RankId == Db.RankId);

            VacancyPlan plan = new VacancyPlan();
            int totalseat = 0, allotseat = 0;
            foreach (var db in tot)
            {


                totalseat = db.Count;
             
            }
            foreach (var db in tot1)
            {


                allotseat = db.Count;

            }


            if (totalseat>= allotseat+Db.Total)
            {
                plan = CheckUserExist(Db);
                if (plan != null && plan.Id > 0)
                    Db.Id = plan.Id;


                if (Db.Id == 0)
                {

                    _context.Add(Db);
                    _context.SaveChanges();

                    //_context.Update(Db);
                    //await _context.SaveChangesAsync();
                    //ViewBag.message = "The user " + Db.Name + " is Update successfully";

                    return 1;
                }

                else
                {
                    var query =
                   from ord in _context.vacancyPlans
                   where ord.Id == Db.Id
                   select ord;
                    foreach (VacancyPlan ord in query)
                    {

                        ord.Total = Db.Total;
                        // Insert any additional changes to column values.
                    }

                    // Submit the changes to the database.
                    try
                    {
                        _context.SaveChanges();
                        return 1;
                    }
                    catch (Exception e)
                    {
                        return 2;
                        Console.WriteLine(e);
                        // Provide for exceptions.
                    }
                    return 3;
                }
            }
            else
            {
                return 4;
            }


        }
        public int del(VacancyPlan Db)
        {
            var query =
        from ord in _context.vacancyPlans
        where ord.UserId == Db.UserId && ord.OcassionId == Db.OcassionId
        select ord;

            // Execute the query, and change the column values
            // you want to change.
            foreach (VacancyPlan ord in query)
            {

                ord.Total = 0;
                // Insert any additional changes to column values.
            }

            // Submit the changes to the database.
            try
            {
                _context.SaveChanges();
                return 1;
            }
            catch (Exception e)
            {
                return 2;
                Console.WriteLine(e);
                // Provide for exceptions.
            }
        }
        public VacancyPlan CheckUserExist(VacancyPlan db)
        {

            var data= _context.vacancyPlans.SingleOrDefault(e =>e.UserId==db.UserId && e.OcassionId == db.OcassionId && e.EnclosureId==db.EnclosureId && e.RankId==db.RankId);
            return data;
        }
        public VacancyPlan GetById(int Id)
        {
            var AllData = _context.vacancyPlans.Where(i => i.Id == Id).Single();


            return AllData;
        }

        public List<VacancyPlan> GetAll()
        {
            var AllData = _context.vacancyPlans.ToList();


            return AllData;
        }
        public VacancyPlanEntry GetAllVacancy(int Id)
        {
            //var AllData = _context.vacancies.Where(i => i.IsActive == 1).OrderByDescending(x => x.CreatedOn).ToList();

            var result = from Vacancy in _context.vacancies
                         
                         join Ocassion in _context.Ocassions on Vacancy.OcassionId equals Ocassion.Id
                          //into Oc from Ocassion in Oc.DefaultIfEmpty()
                         join Enclosure in _context.enclosures on Vacancy.EnclosureId equals Enclosure.Id 
                        //into En from Enclosure in En.DefaultIfEmpty()
                         join Rank in _context.ranks on Vacancy.RankId equals Rank.Id
                        // into Rnkfrom Rank in Rnk.DefaultIfEmpty()
                         //join Category in _context.categories on Vacancy.CategoryId equals Category.Id into cat
                         //from Category in cat.DefaultIfEmpty()
                         where Ocassion.Id == Id && Vacancy.IsActive == 1
                         select new
                         {
                             Id = Vacancy.Id,
                             OcassionId = Vacancy.OcassionId,
                             OcassionName = Ocassion.OcassionName,
                             EnclosureId = Vacancy.EnclosureId,
                             EnclosureName = Enclosure.Title,
                             EnclosureColor=Enclosure.ColorCode,
                             //CategoryId = Vacancy.CategoryId,
                             //CategoryName = Category.Title,
                             RankId = Vacancy.RankId,
                             RankName = Rank.Title,
                             Total = Vacancy.Total,
                             IsLock=Ocassion.IsLock,
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
                vacancy.IsLock = db.IsLock;
                //vacancy.CreatedOn = db.CreatedOn;
                //vacancy.UpdatedOn = db.UpdatedOn;
                //vacancy.IsActive = db.IsActive;
                //vacancy.CreatedBy = db.CreatedBy;
                lst.Add(vacancy);
            }
            mainlst.vacancieslist=lst.OrderBy(n => n.EnclosureName).ToList();

            // var AllData = _context.Users.Where(i => i.IsActive == 1 && i.GroupId == 1 && i.Id != 1).ToList();
            var All = new HashSet<int>(_context.ocassionMappings.Where(i => i.OcassionId == Id && i.IsActive==1).Select(x => x.UserId));

            var filtered = _context.Users.Where(x => All.Contains(x.Id) && x.TypeId != 1 && x.IsActive==1).ToList();
            List<User> userlst = new List<User>();

            foreach (var db in filtered)
            {
                User user = new User();

                user.Id = db.Id;
                user.UserName = db.UserName;

                userlst.Add(user);

            }
            mainlst.Userlist = userlst;


            var OcassionStatus = _context.ocassionStatuses.Where(x => x.OcassionId==Id && x.IsStatus==1).ToList();
            List<OcassionStatus> OcassionStatuslst = new List<OcassionStatus>();

            foreach (var db in OcassionStatus)
            {
                OcassionStatus sat = new OcassionStatus();

                sat.UserId = db.UserId;
                sat.IsStatus = db.IsStatus;

                OcassionStatuslst.Add(sat);

            }
            mainlst.OcassionStatus = OcassionStatuslst;






            var resultplan = from VacancyPlan in _context.vacancyPlans
                             join OcassionMapping in _context.ocassionMappings on VacancyPlan.OcassionId equals OcassionMapping.OcassionId
                             join Ocassion in _context.Ocassions on VacancyPlan.OcassionId equals Ocassion.Id into Oc
                             from Ocassion in Oc.DefaultIfEmpty()

                             where VacancyPlan.OcassionId == Id && OcassionMapping.IsActive == 1
                             //select new
                             //{
                             //    UserId = VacancyPlan.UserId,
                             //    EnclosureId = VacancyPlan.EnclosureId,
                             //    RankId = VacancyPlan.RankId,
                             //    Total = VacancyPlan.Total,

                             //};
                             group VacancyPlan by new
                             {
                                 UserId = VacancyPlan.UserId,
                                 EnclosureId = VacancyPlan.EnclosureId,
                                 RankId = VacancyPlan.RankId,
                                 Total = VacancyPlan.Total,
                             } into grouped
                             select new
                             {
                                 UserId = grouped.Key.UserId,
                                 EnclosureId = grouped.Key.EnclosureId,
                                 RankId = grouped.Key.RankId,
                                 Total = grouped.Key.Total,

                             };


            List<VacancyPlan> vlst = new List<VacancyPlan>();

                    foreach (var db in resultplan)
                    {
                        VacancyPlan vacancyPlan = new VacancyPlan();

                        vacancyPlan.UserId = db.UserId;
                        vacancyPlan.EnclosureId = db.EnclosureId;
                        vacancyPlan.RankId = db.RankId;
                        vacancyPlan.Total = db.Total;

                vlst.Add(vacancyPlan);

                    }
                    mainlst.vacancyPlanslist = vlst;


            return mainlst;


        }

        public VacancyPlanEntry GetAllVacancyWithPlan(int Id)
        {
            // var AllData = _context.vacancies.Where(i => i.IsActive == 1).OrderByDescending(x => x.CreatedOn).ToList();
            var result = from Vacancy in _context.vacancies
                        
                         join Ocassion in _context.Ocassions on Vacancy.OcassionId equals Ocassion.Id into Oc
                         from Ocassion in Oc.DefaultIfEmpty()
                         join Enclosure in _context.enclosures on Vacancy.EnclosureId equals Enclosure.Id into En
                         from Enclosure in En.DefaultIfEmpty()
                         join Rank in _context.ranks on Vacancy.RankId equals Rank.Id into Rnk
                         from Rank in Rnk.DefaultIfEmpty()

                             //join Category in _context.categories on Vacancy.CategoryId equals Category.Id into cat
                             //from Category in cat.DefaultIfEmpty()
                         where Ocassion.Id == Id && Vacancy.IsActive == 1
                         select new
                         {
                             Id = Vacancy.Id,
                             OcassionId = Vacancy.OcassionId,
                             OcassionName = Ocassion.OcassionName,
                             EnclosureId = Vacancy.EnclosureId,
                             EnclosureName = Enclosure.Title,
                             EnclosureColor = Enclosure.ColorCode,
                             //CategoryId = Vacancy.CategoryId,
                             //CategoryName = Category.Title,
                             RankId = Vacancy.RankId,
                             RankName = Rank.Title,
                             Total = Vacancy.Total,
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

            //var AllData = _context.Users.Where(i => i.IsActive == 1 && i.GroupId == 1 && i.Id != 1).ToList();

            //List<User> userlst = new List<User>();

            //foreach (var db in AllData)
            //{
            //    User user = new User();

            //    user.Id = db.Id;
            //    user.UserName = db.UserName;

            //    userlst.Add(user);

            //}
            //mainlst.Userlist = userlst;
            var resultplan = from VacancyPlan in _context.vacancyPlans
                             join OcassionMapping in _context.ocassionMappings on VacancyPlan.OcassionId equals OcassionMapping.OcassionId
                             join Ocassion in _context.Ocassions on VacancyPlan.OcassionId equals Ocassion.Id into Oc
                             from Ocassion in Oc.DefaultIfEmpty()

                             where VacancyPlan.OcassionId == Id && OcassionMapping.IsActive == 1
                             //select new
                             //{
                             //    UserId = VacancyPlan.UserId,
                             //    EnclosureId = VacancyPlan.EnclosureId,
                             //    RankId = VacancyPlan.RankId,
                             //    Total = VacancyPlan.Total,

                             //};
                             group VacancyPlan by new
                             {
                                 UserId = VacancyPlan.UserId,
                                 EnclosureId = VacancyPlan.EnclosureId,
                                 RankId = VacancyPlan.RankId,
                                 Total = VacancyPlan.Total,
                             } into grouped
                             select new
                             {
                                 UserId = grouped.Key.UserId,
                                 EnclosureId = grouped.Key.EnclosureId,
                                 RankId = grouped.Key.RankId,
                                 Total = grouped.Key.Total,

                             };

            List<VacancyPlan> vlst = new List<VacancyPlan>();

            foreach (var db in resultplan)
            {
                VacancyPlan vacancyPlan = new VacancyPlan();

                vacancyPlan.UserId = db.UserId;
                vacancyPlan.EnclosureId = db.EnclosureId;
                vacancyPlan.RankId = db.RankId;
                vacancyPlan.Total = db.Total;

                vlst.Add(vacancyPlan);

            }
            mainlst.vacancyPlanslist = vlst;

            return mainlst;


        }

        public VacancyPlanEntry GetAllVacancyWithPlanByUserId(int Id,int UserId)
        {
            // var AllData = _context.vacancies.Where(i => i.IsActive == 1).OrderByDescending(x => x.CreatedOn).ToList();
            var result = from VacancyPlan in _context.vacancyPlans
                         join Ocassion in _context.Ocassions on VacancyPlan.OcassionId equals Ocassion.Id into Oc
                         from Ocassion in Oc.DefaultIfEmpty()
                         join Enclosure in _context.enclosures on VacancyPlan.EnclosureId equals Enclosure.Id into En
                         from Enclosure in En.DefaultIfEmpty()
                         join Rank in _context.ranks on VacancyPlan.RankId equals Rank.Id into Rnk
                         from Rank in Rnk.DefaultIfEmpty()
                             //join Category in _context.categories on Vacancy.CategoryId equals Category.Id into cat
                             //from Category in cat.DefaultIfEmpty()
                         where Ocassion.Id == Id && VacancyPlan.UserId== UserId 
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

            //var AllData = _context.Users.Where(i => i.IsActive == 1 && i.GroupId == 1 && i.Id != 1).ToList();

            //List<User> userlst = new List<User>();

            //foreach (var db in AllData)
            //{
            //    User user = new User();

            //    user.Id = db.Id;
            //    user.UserName = db.UserName;

            //    userlst.Add(user);

            //}
            //mainlst.Userlist = userlst;
            var resultplan = from VacancyPlan in _context.vacancyPlans
                             join Ocassion in _context.Ocassions on VacancyPlan.OcassionId equals Ocassion.Id into Oc
                             from Ocassion in Oc.DefaultIfEmpty()

                             where VacancyPlan.OcassionId == Id && VacancyPlan.UserId==UserId 
                             select new
                             {
                                 UserId = VacancyPlan.UserId,
                                 EnclosureId = VacancyPlan.EnclosureId,
                                 RankId = VacancyPlan.RankId,
                                 Total = VacancyPlan.Total,

                             };

            List<VacancyPlan> vlst = new List<VacancyPlan>();

            foreach (var db in resultplan)
            {
                VacancyPlan vacancyPlan = new VacancyPlan();

                vacancyPlan.UserId = db.UserId;
                vacancyPlan.EnclosureId = db.EnclosureId;
                vacancyPlan.RankId = db.RankId;
                vacancyPlan.Total = db.Total;

                vlst.Add(vacancyPlan);

            }
            mainlst.vacancyPlanslist = vlst;

            return mainlst;


        }
        public VacancyPlanEntry GetAllVacancyUserBy(int Id,int UserId)
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
                         where Ocassion.Id == Id && VacancyPlan.UserId==UserId
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
                             IsLock=Ocassion.IsLock,
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
                vacancy.IsLock = db.IsLock;
                //vacancy.CreatedOn = db.CreatedOn;
                //vacancy.UpdatedOn = db.UpdatedOn;
                //vacancy.IsActive = db.IsActive;
                //vacancy.CreatedBy = db.CreatedBy;
                lst.Add(vacancy);
            }
            mainlst.vacancieslist = lst.OrderBy(n => n.EnclosureName).ToList();

            var AllData = _context.Users.Where(i => i.IsActive == 1 && i.GroupId == 1 && i.Id == UserId).ToList();

            List<User> userlst = new List<User>();

            foreach (var db in AllData)
            {
                User user = new User();

                user.Id = db.Id;
                user.UserName = db.UserName;

                userlst.Add(user);

            }
            mainlst.Userlist = userlst;









            var resultplan = from VacancyPlan in _context.vacancyPlans
                             join Ocassion in _context.Ocassions on VacancyPlan.OcassionId equals Ocassion.Id into Oc
                             from Ocassion in Oc.DefaultIfEmpty()

                             where VacancyPlan.OcassionId == Id && VacancyPlan.UserId== UserId
                             select new
                             {
                                 UserId = VacancyPlan.UserId,
                                 EnclosureId = VacancyPlan.EnclosureId,
                                 RankId = VacancyPlan.RankId,
                                 Total = VacancyPlan.Total,

                             };


            List<VacancyPlan> vlst = new List<VacancyPlan>();

            foreach (var db in resultplan)
            {
                VacancyPlan vacancyPlan = new VacancyPlan();

                vacancyPlan.UserId = db.UserId;
                vacancyPlan.EnclosureId = db.EnclosureId;
                vacancyPlan.RankId = db.RankId;
                vacancyPlan.Total = db.Total;

                vlst.Add(vacancyPlan);

            }
            mainlst.vacancyPlanslist = vlst;




            return mainlst;


        }
        public async Task<VacancyPlanEntry> GetAllVacancyRankWise(int Id, int UserId,int EnclosureId,int RankId)
        {
           // await _context.vacancies.Where(i => i.IsActive == 1).OrderByDescending(x => x.CreatedOn).ToList();

            var result =from VacancyPlan in _context.vacancyPlans
                         join Ocassion in _context.Ocassions on VacancyPlan.OcassionId equals Ocassion.Id 
                         //into Oc from Ocassion in Oc.DefaultIfEmpty()
                         join Enclosure in _context.enclosures on VacancyPlan.EnclosureId equals Enclosure.Id into En
                         from Enclosure in En.DefaultIfEmpty()
                         join Rank in _context.ranks on VacancyPlan.RankId equals Rank.Id into Rnk
                         from Rank in Rnk.DefaultIfEmpty()
                             //join Category in _context.categories on Vacancy.CategoryId equals Category.Id into cat
                             //from Category in cat.DefaultIfEmpty()
                         where Ocassion.Id == Id && VacancyPlan.UserId == UserId && Enclosure.Id==EnclosureId  && Rank.Id==RankId
                         select new
                         {
                             Id = VacancyPlan.Id,
                             OcassionId = VacancyPlan.OcassionId,
                             OcassionName = Ocassion.OcassionName,
                             IsLock=Ocassion.IsLock,
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
                vacancy.IsLock = db.IsLock;
                //vacancy.CreatedOn = db.CreatedOn;
                //vacancy.UpdatedOn = db.UpdatedOn;
                //vacancy.IsActive = db.IsActive;
                //vacancy.CreatedBy = db.CreatedBy;
                lst.Add(vacancy);
            }
            mainlst.vacancieslist = lst.OrderBy(n => n.EnclosureName).ToList();

           // var AllData = _context.Users.Where(i => i.IsActive == 1 && i.GroupId == 1 && i.Id == UserId).ToList();


            return mainlst;


        }

        public async Task<VacancyPlanEntry> GetAllVacancyOcassion(int Id)
        {
            // await _context.vacancies.Where(i => i.IsActive == 1).OrderByDescending(x => x.CreatedOn).ToList();

            var result = from VacancyPlan in _context.vacancyPlans
                         join Ocassion in _context.Ocassions on VacancyPlan.OcassionId equals Ocassion.Id
                         //into Oc from Ocassion in Oc.DefaultIfEmpty()
                         join Enclosure in _context.enclosures on VacancyPlan.EnclosureId equals Enclosure.Id into En
                         from Enclosure in En.DefaultIfEmpty()
                         join Rank in _context.ranks on VacancyPlan.RankId equals Rank.Id into Rnk
                         from Rank in Rnk.DefaultIfEmpty()
                             //join Category in _context.categories on Vacancy.CategoryId equals Category.Id into cat
                             //from Category in cat.DefaultIfEmpty()
                         where Ocassion.Id == Id
                         select new
                         {
                             Id = VacancyPlan.Id,
                             OcassionId = VacancyPlan.OcassionId,
                             OcassionName = Ocassion.OcassionName,
                             IsLock = Ocassion.IsLock,
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
                vacancy.IsLock = db.IsLock;
                //vacancy.CreatedOn = db.CreatedOn;
                //vacancy.UpdatedOn = db.UpdatedOn;
                //vacancy.IsActive = db.IsActive;
                //vacancy.CreatedBy = db.CreatedBy;
                lst.Add(vacancy);
            }
            mainlst.vacancieslist = lst.OrderBy(n => n.EnclosureName).ToList();

            // var AllData = _context.Users.Where(i => i.IsActive == 1 && i.GroupId == 1 && i.Id == UserId).ToList();


            return mainlst;


        }

        public async Task<int> MirronSave(int OcassionFilterId, int UserId)
        {
            try
            {
                var query =
                           from vanplan in _context.vacancyPlans
                           where vanplan.UserId == UserId && vanplan.OcassionId == OcassionFilterId
                           select vanplan;


                foreach (VacancyPlan db in query)
                {

                    VacancyPlanMirror vn = new VacancyPlanMirror();

                    vn.PlanId = db.Id;
                    vn.OcassionId = db.OcassionId;
                    vn.UserId = db.UserId;
                    vn.RankId = db.RankId;
                    vn.EnclosureId = db.EnclosureId;
                    vn.MirronOn = DateTime.Now;



                    // _context.Entry(vn).State = EntityState.Modified;

                    _context.MirrorvacancyPlans.Add(vn);

                    _context.SaveChanges();

                    // Insert any additional changes to column values.
                }
            }
            catch(Exception ex)
            {
                _ = ex.Message;
            }
            return 1;
        }
    }
}
