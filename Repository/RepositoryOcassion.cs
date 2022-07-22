using E_Invitation.Data;
using E_Invitation.Helpers;
using E_Invitation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Invitation.Repository
{
    public class RepositoryOcassion
    {
        public readonly ApplicationDbContext _context;
     
        public RepositoryOcassion(ApplicationDbContext context)
        {
            _context = context;
            
        }
       
        public async Task<int> Save(Ocassion Db)
        {

          
            Db.IsActive = 1;
         
            if (!CheckUserExist(Db))
            {

                _context.Update(Db);
                await _context.SaveChangesAsync();

                //_context.Update(Db);
                //await _context.SaveChangesAsync();
                //ViewBag.message = "The user " + Db.Name + " is Update successfully";

                return 1;
            }
            else
            {
                return 3;
            }

            //else
            //{
            //    _context.Update(Db);
            //    await _context.SaveChangesAsync();
            //    return 2;
            //}



        }
        public bool CheckUserExist(Ocassion db)
        {
          
            return _context.Ocassions.Any(e => e.OcassionName == db.OcassionName && e.OcassionDate==db.OcassionDate && e.Id!=db.Id);

        }
        public Ocassion GetById(int Id)
        {
            var AllData = _context.Ocassions.Where(i => i.Id == Id).Single();


            return AllData;
        }
       
        public List<Ocassion> GetAll()
        {
            var AllData = _context.Ocassions.Where(i => i.IsActive == 1).OrderByDescending(x => x.CreatedOn).ToList();


            return AllData;
        }
        public List<Ocassion> GetAllClose()
        {
            var AllData = _context.Ocassions.Where(i => i.IsActive == 1 && i.IsFinish==true).OrderByDescending(x => x.CreatedOn).ToList();


            return AllData;
        }
        public List<Ocassion> GetAllActive()
        {
            var AllData = _context.Ocassions.Where(i => i.IsActive == 1  && i.IsFinish== false).OrderByDescending(x => x.CreatedOn).ToList();
            AllData.Insert(0, new Ocassion { Id = -1, OcassionName = "--Select Event Name--" });


            return AllData;
        }
        public List<Ocassion> GetAllDeActive()
        {
            var AllData = _context.Ocassions.Where(i => i.IsActive == 1 && i.IsFinish == true).OrderByDescending(x => x.CreatedOn).ToList();
            AllData.Insert(0, new Ocassion { Id = -1, OcassionName = "--Select Event Name--" });


            return AllData;
        }
        public int SaveStatus(int OcassionId, int Status)
        {
            var query =
            from ord in _context.Ocassions
            where ord.Id == OcassionId
            select ord;

            // Execute the query, and change the column values
            // you want to change.
            foreach (Ocassion ord in query)
            {
                if(Status==0)
                ord.IsLock = false;
                else if (Status == 1)
                    ord.IsLock = true;
                else if (Status == 3)
                    ord.IsFinish = true;
                else if (Status == 4)
                    ord.IsFinish = false;

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
    }
}


