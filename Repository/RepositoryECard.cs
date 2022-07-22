using E_Invitation.Data;
using E_Invitation.Helpers;
using E_Invitation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Invitation.Repository
{
    public class RepositoryECard
    {
        public readonly ApplicationDbContext _context;
     
        public RepositoryECard(ApplicationDbContext context)
        {
            _context = context;
            
        }
       
        public async Task<int> Save(ECard Db)
        {




            Db.IsActive = 1;

           // if (!CheckUserExist(Db))
            {
               
                _context.Update(Db);
                await _context.SaveChangesAsync();

              
                return 1;
            }

           // else
           // {
               
              //  return 3;
           // }



        }
        public int del(ECard Db)
        {
            var query =
            from ord in _context.ECards
            where ord.Id == Db.Id
            select ord;

            // Execute the query, and change the column values
            // you want to change.
            foreach (ECard ord in query)
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
        public bool CheckUserExist(ECard db)
        {
          
            return _context.ECards.Any(e => e.OcassionId == db.OcassionId );

        }
        public ECard GetById(int Id)
        {
            var AllData = _context.ECards.Where(i => i.Id == Id).Single();


            return AllData;
        }
   
        public List<ECard> GetAll(int OcassionId)
        {
            var AllData = _context.ECards.Where(i =>i.OcassionId== OcassionId && i.IsActive == 1).OrderBy(x => x.Id).ToList();


            return AllData;
        }
       
    }
}
