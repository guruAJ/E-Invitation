using E_Invitation.Data;
using E_Invitation.Helpers;
using E_Invitation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Invitation.Repository
{
    public class RepositoryEnclosure
    {
        public readonly ApplicationDbContext _context;
     
        public RepositoryEnclosure(ApplicationDbContext context)
        {
            _context = context;
            
        }
       
        public async Task<int> Save(Enclosure Db)
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
                //_context.Update(Db);
                //await _context.SaveChangesAsync();
                return 3;
            }



        }
        public int del(Enclosure Db)
        {
            var query =
            from ord in _context.enclosures
            where ord.Id == Db.Id
            select ord;

            // Execute the query, and change the column values
            // you want to change.
            foreach (Enclosure ord in query)
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
        public bool CheckUserExist(Enclosure db)
        {
          
            return _context.enclosures.Any(e => e.Title == db.Title && e.Id!=db.Id && e.IsActive == 1);

        }
        public Enclosure GetById(int Id)
        {
            var AllData = _context.enclosures.Where(i => i.Id == Id).Single();


            return AllData;
        }
       
        public List<Enclosure> GetAll()
        {
            var AllData = _context.enclosures.Where(i => i.IsActive == 1).OrderBy(x => x.Id).ToList();


            return AllData;
        }
       
    }
}
