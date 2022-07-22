using E_Invitation.Data;
using E_Invitation.Helpers;
using E_Invitation.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace E_Invitation.Repository
{
    public class RepositoryUser
    {
        public readonly ApplicationDbContext _context;
     
        public RepositoryUser(ApplicationDbContext context)
        {
            _context = context;
            
        }

        public User Validated(User Db)
        {
            if (!string.IsNullOrEmpty(Db.UserName) && !string.IsNullOrEmpty(Db.Password))
            { 
                Db.Password = Utility.Security.GetHashString(Db.Password);

            var AllData = _context.Users.Where(i => i.UserName == Db.UserName && i.Password == Db.Password).Single();

            return AllData;
            }
            else
            {
                return null;
            }
        }
        public async Task<int> Save(User Db)
        {


            Db.IsActive = 1;
           
            Db.UpdatedOn = DateTime.Now;
          
            if (!CheckUserExist(Db.UserName,Db.Id))
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
                return 2;
            }



        }
        public int ChnagePassword(User Db)
        {
            var query =
            from ord in _context.Users
            where ord.Id == Db.Id
            select ord;

            // Execute the query, and change the column values
            // you want to change.
            foreach (User ord in query)
            {

                ord.Password = Db.Password;
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
        public int MakeAdmin(int UserId)
        {
            var query =
            from ord in _context.Users
            where ord.Id == UserId
            select ord;

            // Execute the query, and change the column values
            // you want to change.
            foreach (User ord in query)
            {

                ord.TypeId = 1;
                
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

        public int RemoveAdmin(int UserId)
        {
            var query =
            from ord in _context.Users
            where ord.Id == UserId
            select ord;

            // Execute the query, and change the column values
            // you want to change.
            foreach (User ord in query)
            {

                ord.TypeId = 2;

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
        public bool CheckUserExist(string UserName,int Id)
        {
          
            return _context.Users.Any(e => e.UserName == UserName && e.Id!=Id);

        }
        public User GetById(int Id)
        {
            var AllData = _context.Users.Where(i => i.Id == Id).Single();


            return AllData;
        }
        public User GetByIdAll(string Id)
        {
            var AllData = _context.Users.Where(i => i.UserName == Id).Single();


            return AllData;
        }
        public List<User> GetAll(int GroupId)
        {
            var AllData = _context.Users.Where(i => i.IsActive == 1 && i.Id != 1).ToList();
           


            return AllData;
        }
        public List<User> GetAllOcassionBy(int OcassionId)
        {
          //  var AllData = _context.Users.Where(i => i.IsActive == 1 && i.GroupId == GroupId && i.Id != 1).ToList();

            var All = new HashSet<int>(_context.ocassionMappings.Where(i => i.OcassionId == OcassionId && i.IsActive == 1).Select(x => x.UserId));

            var AllData = _context.Users.Where(x => All.Contains(x.Id) && x.TypeId != 1 && x.IsActive == 1).ToList();



            return AllData;
        }



        public int ResetPassword(User Db)
        {









            var query =
            from ord in _context.Users
            where ord.Id == Db.Id
            select ord;

            // Execute the query, and change the column values
            // you want to change.
            foreach (User ord in query)
            {

                ord.Password = Db.Password;
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

        public List<User> GetNotMappAll(int OcassionId)
        {
          
            var All = new HashSet<int>(_context.ocassionMappings.Where(i => i.OcassionId == OcassionId && i.IsActive==1).Select(x => x.UserId));

            var filtered = _context.Users.Where(x => !All.Contains(x.Id) && x.TypeId != 1 && x.IsActive==1).ToList();

            return filtered;
        }
        public List<User> GetByIdAll(int ComponentId)
        {
            var AllData = _context.Users.Where(i => i.IsActive == 1 && i.Id != 1).ToList();


            return AllData;
        }
    }
}
