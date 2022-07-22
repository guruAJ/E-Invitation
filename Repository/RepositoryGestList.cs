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
    public class RepositoryGestList
    {
        public readonly ApplicationDbContext _context;
        
        public RepositoryGestList(ApplicationDbContext context)
        {
            _context = context;
            
        }
       
        public async Task<int> Save(AddGestList Db)
        {

          
            Db.IsActive = true;
            //Db.CreatedOn = DateTime.Now;
            //Db.UpdatedOn = DateTime.Now;
            //Db.CreatedBy = Db.Id;
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
        public int UpdatePass(AddGestList Db)
        {
            var query =
            from ord in _context.addGestLists
            where ord.Id == Db.Id
            select ord;

            // Execute the query, and change the column values
            // you want to change.
            foreach (AddGestList ord in query)
            {

                ord.IsPass = Db.IsPass;
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
        public int del(int Id)
        {
            var query =
            from ord in _context.addGestLists
            where ord.Id == Id
            select ord;

            // Execute the query, and change the column values
            // you want to change.
            foreach (AddGestList ord in query)
            {

                ord.IsActive = false;
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
        public bool CheckUserExist(AddGestList db)
        {
          
            return _context.Ocassions.Any(e => e.Id == db.OcassionId && e.IsLock == true);

        }
        public AddGestList GetById(int Id)
        {
            var AllData = _context.addGestLists.Where(i => i.Id == Id && i.IsActive == true).Single();


            return AllData;
        }
        public int GetTotalGest(int UserId, int OcassionId, int Encl, int rankId)
        {
            var AllData = _context.addGestLists.Where(i => i.OcassionId == OcassionId && i.EnclosureId == Encl && i.RankId == rankId && i.UserId == UserId && i.IsActive == true).Count();


            return AllData;
        }
        public List<AddGestList> GetAll(AddGestList db)
        {
            var AllData = _context.addGestLists.Where(i =>i.OcassionId==db.OcassionId &&  i.UserId == db.UserId && i.EnclosureId==db.EnclosureId && i.RankId==db.RankId && i.IsActive==true).OrderByDescending(x => x.ArmyNo).ToList();


            return AllData;

        }
        public List<AddGestList> GetAllOcassionby(AddGestList db)
        {
            var AllData = _context.addGestLists.Where(i => i.OcassionId == db.OcassionId && i.IsActive == true && i.IsPass==true).OrderByDescending(x => x.ArmyNo).ToList();


            return AllData;

        }
        public AddGestList GetbyArmyno(string ArmyNo)
        {
            var AllData = _context.addGestLists.Where(i => i.ArmyNo == ArmyNo).FirstOrDefault();


            return AllData;
        }
        public List<AddGestList> GetAllOcassionby(int OcassionId,int UserId)
        {
            dynamic AllData;
            if(UserId==0)
             AllData = _context.addGestLists.Where(i => i.OcassionId== OcassionId && i.IsActive==true).OrderByDescending(x => x.EnclosureId).ToList();
            else
             AllData = _context.addGestLists.Where(i => i.OcassionId == OcassionId && i.UserId == UserId && i.IsActive == true).OrderByDescending(x => x.EnclosureId).ToList();


            return AllData;
        }
        public List<DTOGestPass> GetAllPass(int UserId,int OcassionId)
        {
            var result = from AddGestList in _context.addGestLists
                         join Ocassion in _context.Ocassions on AddGestList.OcassionId equals Ocassion.Id into Oc
                         from Ocassion in Oc.DefaultIfEmpty()
                         join Enclosure in _context.enclosures on AddGestList.EnclosureId equals Enclosure.Id into En
                         from Enclosure in En.DefaultIfEmpty()
                         join Rank in _context.ranks on AddGestList.RankId equals Rank.Id into Rnk
                         from Rank in Rnk.DefaultIfEmpty()
                         join ECard in _context.ECards on AddGestList.OcassionId equals ECard.OcassionId into Eca
                         from ECard in Eca.DefaultIfEmpty()
                             //join Category in _context.categories on Vacancy.CategoryId equals Category.Id into cat
                             //from Category in cat.DefaultIfEmpty()
                         where Ocassion.Id == OcassionId && AddGestList.UserId == UserId
                         select new
                         {
                             Id = AddGestList.Id,
                             OcassionName = Ocassion.OcassionName,
                             OcassionDate=Ocassion.OcassionDate,
                             ChiefName = Ocassion.ChiefName,
                             Venue = Ocassion.Venue,
                             Time = Ocassion.Time,
                             Dress = Ocassion.Dress,
                             Dress1 = Ocassion.Dress1,
                             ContactName = Ocassion.ContactName,
                             IssueBranch = Ocassion.IssueBranch,
                             PhoneNo = Ocassion.PhoneNo,
                             ASCON = Ocassion.ASCON,
                             EnclosureName = Enclosure.Title,
                             EnclosureColor = Enclosure.ColorCode,
                             RankName = Rank.Title,
                             ArmyNo= AddGestList.ArmyNo,
                             IndlName= AddGestList.IndlName,
                             NameOfGest= AddGestList.NameOfGest,
                             Photo= AddGestList.Photo,
                             Adharno= AddGestList.AdhaorNo,
                             Card1 = ECard.Card1,
                             Card2 = ECard.Card2,
                             Card3 = ECard.Card3
                         };


            List<DTOGestPass> lst = new List<DTOGestPass>();
            foreach (var db in result)
            {
                DTOGestPass dto = new DTOGestPass();


                dto.Id = db.Id;
                dto.OcassionName = db.OcassionName;
                dto.OcassionDate = db.OcassionDate;
                dto.ChiefName = db.ChiefName;
                dto.Venue = db.Venue;
                dto.Time = db.Time;
                dto.Dress = db.Dress;
                dto.Dress1 = db.Dress1;
                dto.ContactName = db.ContactName;
                dto.IssueBranch = db.IssueBranch;
                dto.PhoneNo = db.PhoneNo;
                dto.ASCON = db.ASCON;
                dto.EnclosureName = db.EnclosureName;
                dto.EnclosureColor = db.EnclosureColor;
                dto.RankName = db.RankName;
                dto.ArmyNo = db.ArmyNo;
                dto.IndlName = db.IndlName;
                dto.NameOfGest = db.NameOfGest;
                dto.Photo = db.Photo;
                dto.Card1 = db.Card1;
                dto.Card2 = db.Card2;
                dto.Card3 = db.Card3;
                dto.Adharno = db.Adharno;
                lst.Add(dto);
            }
           
            return lst.OrderBy(n => n.EnclosureName).OrderBy(n => n.RankName).ToList();
        }

    }
}
