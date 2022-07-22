using E_Invitation.Data;
using E_Invitation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Invitation.Repository
{
    public class RepositoryOcassionStatus
    {
        public readonly ApplicationDbContext _context;
        public RepositoryOcassionStatus(ApplicationDbContext context)
        {
            _context = context;

        }
        public async Task<int> Save(OcassionStatus Db, int status)
        {
            if (Db.Id > 0)
                Db.Id = 0;
            if (status == 0)
            { 
                if (!CheckUserExist(Db))
                {
                    Db.IsStatus = 1;
                    _context.Add(Db);
                    _context.SaveChanges();

                    return 1;
                }
                else
                {

                    var query =
                    from ord in _context.ocassionStatuses
                    where ord.OcassionId == Db.OcassionId && ord.UserId == Db.UserId
                    select ord;
                    foreach (OcassionStatus ord in query)
                    {

                        ord.IsStatus = 0;
                        ord.UserId = 0;
                        ord.OcassionId = 0;
                        // Insert any additional changes to column values.
                    }

                    // Submit the changes to the database.
                    try
                    {
                        _context.SaveChanges();
                        return 2;
                    }
                    catch (Exception e)
                    {
                        return 3;
                        Console.WriteLine(e);
                        // Provide for exceptions.
                    }
                    return 1;
                }
        }
            else
            {
                if (!CheckUserExist(Db))
                {
                    Db.IsStatus = 1;
                    _context.Add(Db);
                    _context.SaveChanges();

                    return 1;
                }
                else
                {
                    return 3;
                }
                
                }


        }
        public bool CheckUserExist(OcassionStatus db)
        {

            return _context.ocassionStatuses.Any(e => e.OcassionId == db.OcassionId && e.UserId == db.UserId);

        }
        public Boolean GetById(int OcassionFilterId, int UserId)
        {
            return _context.ocassionStatuses.Any(e => e.OcassionId == OcassionFilterId && e.UserId == UserId);


        }
        public List<OcassionStatus> GetAll(int OcassionFilterId)
        {
            return _context.ocassionStatuses.Where(e => e.OcassionId == OcassionFilterId).ToList();


        }
    }
}
