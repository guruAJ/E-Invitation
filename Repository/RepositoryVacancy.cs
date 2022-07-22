using E_Invitation.Data;
using E_Invitation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Invitation.Repository
{
    public class RepositoryVacancy
    {
        public readonly ApplicationDbContext _context;

        public RepositoryVacancy(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<int> Save(Vacancy Db)
        {


            Db.IsActive = 1;
          
            Db.UpdatedOn = DateTime.Now;

            Vacancy Db1 = new Vacancy();
            Db1 = Db;
            if (!CheckUserExist(Db))
            {
               
               
                Db = Db1;
                _context.Update(Db);
                await _context.SaveChangesAsync();

                //_context.Update(Db);
                //await _context.SaveChangesAsync();
                //ViewBag.message = "The user " + Db.Name + " is Update successfully";

                return 1;
            }

            else
            {
                var query = _context.vacancies.Where(e => e.OcassionId == Db.OcassionId && e.EnclosureId == Db.EnclosureId && e.RankId == Db.RankId).First();

                query.Total = query.Total + Db.Total;
                query.RankDesc = Db.RankDesc;
                // Insert any additional changes to column values.
                //  }

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
                return 1;
            }



        }

        public async Task<int> Save1(Vacancy db)
        {

            //var query =
            //from ord in _context.vacancies
            //where ord.Id == Id
            //select ord;

            var query = _context.vacancies.Where(e => e.OcassionId == db.OcassionId && e.EnclosureId == db.EnclosureId && e.RankId == db.RankId).First();

            // Execute the query, and change the column values
            // you want to change.
            //foreach (Vacancy ord in query)
            // {

            query.IsActive = 0;
            // Insert any additional changes to column values.
            //  }

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
        public async Task<int> DeleteData(int Id)
        {

            //var query =
            //from ord in _context.vacancies
            //where ord.Id == Id
            //select ord;
          
            var query = _context.vacancies.Where(s => s.Id == Id).First();

            // Execute the query, and change the column values
            // you want to change.
            //foreach (Vacancy ord in query)
            // {

              query.IsActive = 0;
                // Insert any additional changes to column values.
          //  }

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
        public bool CheckUserExist(Vacancy db)
        {

            return _context.vacancies.Any(e => e.OcassionId == db.OcassionId && e.EnclosureId == db.EnclosureId && e.RankId==db.RankId && e.Id != db.Id && e.IsActive == 1);

        }
        public Vacancy GetById(int Id)
        {
            var AllData = _context.vacancies.Where(i => i.Id == Id).Single();


            return AllData;
        }
        public async Task<Vacancy> GetCatDesc(Vacancy db)
        {
            var AllData = _context.vacancies.Where(e => e.OcassionId == db.OcassionId && e.EnclosureId == db.EnclosureId && e.RankId == db.RankId && e.Id != db.Id && e.IsActive == 1).FirstOrDefault();


            return AllData;
        }
        public List<Vacancy> GetAll(int Id)
        {
            //var AllData = _context.vacancies.Where(i => i.IsActive == 1).OrderByDescending(x => x.CreatedOn).ToList();

            var result = from Vacancy in _context.vacancies
                         join Ocassion in _context.Ocassions on Vacancy.OcassionId equals Ocassion.Id into Oc from Ocassion in Oc.DefaultIfEmpty()
                         join Enclosure in _context.enclosures on Vacancy.EnclosureId equals Enclosure.Id into En
                         from Enclosure in En.DefaultIfEmpty()
                         join Rank in _context.ranks on Vacancy.RankId equals Rank.Id into Rnk
                         from Rank in Rnk.DefaultIfEmpty()
                         join Category in _context.categories on Vacancy.CategoryId equals Category.Id into cat
                         from Category in cat.DefaultIfEmpty()
                         where Ocassion.Id == Id && Vacancy.IsActive==1
                         select new
                         {
                             Id = Vacancy.Id,
                             OcassionId = Vacancy.OcassionId,
                             OcassionName = Ocassion.OcassionName,
                             EnclosureId = Vacancy.EnclosureId,
                             EnclosureName = Enclosure.Title,
                             CategoryId = Vacancy.CategoryId,
                             CategoryName = Category.Title,
                             RankId = Vacancy.RankId,
                             RankName = Rank.Title,
                             Total = Vacancy.Total,
                             CreatedOn = Vacancy.CreatedOn,
                             UpdatedOn = Vacancy.UpdatedOn,
                             IsActive = Vacancy.IsActive,
                             CreatedBy = Vacancy.CreatedBy,
                             IsLock = Ocassion.IsLock,
                             RankDesc = Vacancy.RankDesc

                         };

                        List<Vacancy> lst = new List<Vacancy>();

                        foreach (var db in result)
                        {
                          Vacancy vacancy = new Vacancy();


                             vacancy.Id = db.Id;
                             vacancy.OcassionId = db.OcassionId;
                             vacancy.OcassionName = db.OcassionName;
                             vacancy.EnclosureId = db.EnclosureId;
                             vacancy.EnclosureName = db.EnclosureName;
                             vacancy.CategoryId = db.CategoryId;
                             vacancy.CategoryName = db.CategoryName;
                             vacancy.RankId = db.RankId;
                             vacancy.RankName = db.RankName;
                             vacancy.Total = db.Total;
                             vacancy.CreatedOn = db.CreatedOn;
                             vacancy.UpdatedOn = db.UpdatedOn;
                             vacancy.IsActive = db.IsActive;
                vacancy.CreatedBy = db.CreatedBy;
                vacancy.IsLock = db.IsLock;
                vacancy.RankDesc=db.RankDesc;
                            lst.Add(vacancy);
                        }

                        return lst;


        }


        public List<EnclosureCount> EnclosureCountData(int Id)
        {
            //SELECT e.Title,count(*) total FROM e_invitaion.vacancies v
            //left join e_invitaion.enclosures e on v.EnclosureId = e.id group by e.Title
            var result = from Enclosure in _context.enclosures
                         join Vacancy in _context.vacancies on Enclosure.Id equals Vacancy.EnclosureId into En
                         from Vacancy in En.DefaultIfEmpty()
                         join Ocassion in _context.Ocassions on Vacancy.OcassionId equals Ocassion.Id into oc
                         from Ocassion in oc.DefaultIfEmpty()
                         where Ocassion.Id==Id && Vacancy.IsActive == 1
                         group Vacancy by new
                         {
                             Id=Ocassion.Id,
                             Vacancy.EnclosureId,
                             Enclosure.Title,
                             Enclosure.ColorCode,
                         } into grouped

                         select new { Title = grouped.Key.Title,Color= grouped.Key.ColorCode, Count = grouped.Sum(t => t.Total) };



            //List<EnclosureCount> result1 = _context.enclosures
            //.GroupBy(l => new
            //{
            //    l.Title,
            //    l.ColorCode
            //});
            //.Select(cl => new ResultLine
            //{
            //    ProductName = cl.First().Name,
            //    Quantity = cl.Count().ToString(),
            //    Price = cl.Sum(c => c.Price).ToString(),
            //}).ToList();


            List<EnclosureCount> lst = new List<EnclosureCount>();

            foreach (var db in result)
            {
                EnclosureCount enclosure = new EnclosureCount();
                enclosure.Title = db.Title;
                enclosure.Total = db.Count;
                enclosure.Color = db.Color;
                lst.Add(enclosure);
            }
                return lst;
           // from p in context.ParentTable
           //join c in context.ChildTable on p.ParentId equals c.ChildParentId into j1
           //from j2 in j1.DefaultIfEmpty()
           //group j2 by p.ParentId into grouped
           //select new { ParentId = grouped.Key, Count = grouped.Count(t => t.ChildId != null) }



        }
    }
}
