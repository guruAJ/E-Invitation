using E_Invitation.Data;
using E_Invitation.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Invitation.Repository
{
    public class RepositoryOcassionMapping
    {
        public readonly ApplicationDbContext _context;

        public RepositoryOcassionMapping(ApplicationDbContext context)
        {
            _context = context;

        }
        public int Save(OcassionMapping Db)
        {


            Db.IsActive = 1;
            
            if (!CheckUserExist(Db))
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
                //_context.Update(Db);
                //await _context.SaveChangesAsync();
                return 3;
            }



        }
        public bool CheckUserExist(OcassionMapping db)
        {

            return _context.ocassionMappings.Any(e => e.OcassionId == db.OcassionId && e.UserId == db.UserId && e.IsActive == 1);

        }
        public async Task<int> delAsync(int UserId,int OcassionId)
        {
            //try
           // {
                var query =
                from ord in _context.ocassionMappings
                where ord.UserId == UserId && ord.OcassionId == OcassionId
                select ord;


            foreach (OcassionMapping ord in query)
            {

                ord.IsActive = 0;
                // Insert any additional changes to column values.
            }

            // Submit the changes to the database.
            try
            {
               await _context.SaveChangesAsync();
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
