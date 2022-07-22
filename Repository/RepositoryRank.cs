using E_Invitation.Data;
using E_Invitation.Helpers;
using E_Invitation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Invitation.Repository
{
    public class RepositoryRank
    {
        public readonly ApplicationDbContext _context;
     
        public RepositoryRank(ApplicationDbContext context)
        {
            _context = context;
            
        }
       
        public async Task<int> Save(Rank Db)
        {

          
            
           
            Db.UpdatedOn = DateTime.Now;
           
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



        }
        public int del(Rank Db)
        {
            var query =
            from ord in _context.ranks
            where ord.Id == Db.Id
            select ord;

            // Execute the query, and change the column values
            // you want to change.
            foreach (Rank ord in query)
            {
               
                ord.IsActive = 0;
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
        public bool CheckUserExist(Rank db)
        {
          
            return _context.ranks.Any(e => e.Title == db.Title && e.Id != db.Id && e.IsActive==1);

        }
        public Rank GetById(int Id)
        {
            var AllData = _context.ranks.Where(i => i.Id == Id).Single();


            return AllData;
        }
   
        public List<Rank> GetAll()
        {
            var AllData = _context.ranks.Where(i => i.IsActive == 1).OrderBy(x => x.Id).ToList();


            return AllData;
        }
       
    }
}
