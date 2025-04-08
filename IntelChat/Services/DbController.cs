/*
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
*/
using Microsoft.AspNetCore.Mvc;
///using Microsoft.AspNetCore.Mvc.Rendering;
///using Microsoft.EntityFrameworkCore;
using IntelChat.Models;
using Microsoft.EntityFrameworkCore;
using IntelChat.Pages.Account;

/* 23.03.04
 * The purpose of the following nested classes is to better organize the CRUD functions for each view/table used by grouping them together
 * The DbController class has an enormous number of associated methods
 * I think most intuitive way to call this service would, essentially, be "DbController.Minor.Details(id);" and that is the goal of structuring the code this way
 * 
 * DONE - There is hopefully a better method of having a *single instance* of ApplicationDbContext being referenced to by the nested classes, if that isn't already the case
 *      --- make _context public: www.geeksforgeeks.org/nested-classes-in-c-sharp/#:~:text=%C2%A0%C2%A0%C2%A0%C2%A0%C2%A0%C2%A0%C2%A0%C2%A0%C2%A0%C2%A0%C2%A0%C2%A0Console.WriteLine(-,Outer_class.str,-)%3B
        --- Use static for some combination of parent class, inner classes, and/or DbContext attributes
        --- Change something in Program.cs so that the inner and parent classes are all defined as a context from there
*/

namespace IntelChat.Services
{
    public class DbController : Controller
    {
        //protected
        private readonly AppDbContext _context;
        public ViewSlot_CRUD LPSlot;
        public Major_CRUD LPMajor;
        public Minor_CRUD LPMinor;
        public Person_CRUD LPPerson;
        public Calendar_CRUD LPCalendar;

        public DbController(AppDbContext context)
        {
            _context = context;

            LPSlot = new ViewSlot_CRUD(this);
            LPMajor = new Major_CRUD(this);
            LPMinor = new Minor_CRUD(this);
            LPPerson = new Person_CRUD(this);
            LPCalendar = new Calendar_CRUD(this);
        }

        public class ViewSlot_CRUD
        {
            //Note - This should be a *reference* to the parent class, not some terrible repeated duplication of memory (which is what I'm trying to avoid)
            private DbController dbTop; //dbcParent
            public ViewSlot_CRUD(DbController dbController)
            {
                dbTop = dbController;
            }
            public void Create(ViewSlot viewSlot)
            {
                /*
                if(dbTop._context.ViewSlots.FirstOrDefaultAsync(m => m.WeekDay == viewSlot.WeekDay && m.WeekTimeslot == viewSlot.WeekTimeslot && m.WeekOffset == viewSlot.WeekOffset) != null)
                {// ^ Do not add a minor category to a timeslot more than once
                    return;
                }*/

                WeekMyTimeslot newts = new WeekMyTimeslot();
                newts.BuildFromViewslot(viewSlot);
                dbTop._context.WeekMyTimeslots.Add(newts);
                dbTop._context.SaveChanges();
            }
            //DONE - SORT the entries by box day and slot. "lamba 2nd sorting order"
            public List<ViewSlot> Index(int week, int personID = 2)    //personID = 2 = test account's ID. temporary until multiuser support is needed
            {
                ///return await ... ToListAsync()
                return dbTop._context.ViewSlots.Where(v => v.WeekCalendarWeekId == week && v.PersonId == personID).OrderBy(o => o.WeekDay).ThenBy(o => o.WeekTimeslot).ToList();
            }
            public void Delete(int weekCalendarWeekId, int weekDay, int weekTimeslot, int weekOffset, int personId = 2)
            {
                if (dbTop._context.ViewSlots == null) { return; }
                //DONE - To have multiple acitivies exist in a single time slot w/o causing problems, include LpMinor Label in the primary key
                ///WeekMyTimeslot newts = new WeekMyTimeslot();
                ///newts.BuildFromViewslot(viewSlot);

                var deletedObject = dbTop._context.WeekMyTimeslots
                    .FirstOrDefault(m => m.PersonId == personId && m.WeekCalendarWeekId == weekCalendarWeekId
                                            && m.WeekDay == weekDay && m.WeekTimeslot == weekTimeslot && m.WeekOffset == weekOffset);   ///FirstOrDefault

                if (deletedObject == null)  //Check on if '.Result' is needed
                {
                    return;
                }

                dbTop._context.Remove(deletedObject);
                dbTop._context.SaveChanges();
            }
            public void UpdateRaw(ViewSlot viewSlot)    //nexxyy
            {
                if (dbTop._context.ViewSlots == null) { return; }
                WeekMyTimeslot newts = new WeekMyTimeslot();
                newts.BuildFromViewslot(viewSlot);

                //dbTop._context.Update(newts);
                //dbTop._context.SaveChanges();
            }
            public async void UpdateStatus(string weekStatus, ViewSlot viewSlot)
            {
                if (dbTop._context.WeekMyTimeslots == null) { return; }
                WeekMyTimeslot newts = new WeekMyTimeslot();
                newts.BuildFromViewslot(viewSlot);

                newts.WeekStatus = weekStatus;  //Must test this, also test updating status of multiple minors in 1 time slot
                dbTop._context.WeekMyTimeslots.Update(newts);
                await dbTop._context.SaveChangesAsync();
            }
            //23.04.08 - No await or async, deliberately
            public void LoadTemplate(int weekLoadedInto = 17, int personID = 2) //17 for testing
            {
                //if(weekLoadedInto == null) LPCalendar == Date
                List<WeekMyTimeslot> weekMyTimeslots = dbTop._context.WeekMyTimeslots.ToList();

                for(int i = 0; i < weekMyTimeslots.Count; i++)
                {
                    weekMyTimeslots[i].WeekCalendarWeekId = (Int16)weekLoadedInto;
                }

                dbTop._context.Add(weekMyTimeslots);
                dbTop._context.SaveChanges();
            }

        }
        public class Major_CRUD
        {
            private DbController dbTop;
            public Major_CRUD(DbController dbController)
            {
                dbTop = dbController;
            }
            public List<LpMajor> Index()
            {
                return dbTop._context.LpMajors.OrderBy(o => o.LpMajorSeq).ToList(); // => is LINQ lambda expression syntax
            }
        }
        public class Minor_CRUD
        {
            private DbController dbTop;
            public Minor_CRUD(DbController dbController)
            {
                dbTop = dbController;
            }
            public List<LpMinor> Index(int catMajorID)  //TODO - In the future, this should also require a UserID, or something, to not fetch EVERYTHING
            {
                return dbTop._context.LpMinors.Where(o => o.LpMajorId == catMajorID).ToList();
            }
            public void Create(LpMinor lpMinor)
            {
                dbTop._context.Add(lpMinor);
                dbTop._context.SaveChanges();
            }
            public void Update(LpMinor lpMinor)
            {
                //SMD - Return some information about success/not found (bool cannot be passed by async)
                if (dbTop._context.LpMinors == null) { return; }
                /*
                var Found = await dbTop._context.LpMinors.FindAsync(lpMinor.LpMinorId);
                if (Found == null)
                {
                    return;
                }*/

                dbTop._context.LpMinors.Update(lpMinor);
                dbTop._context.SaveChanges();
            }
            public void Delete(int minorId)
            {
                if (dbTop._context.LpMinors == null) { return; }

                //SMD - To have multiple acitivies exist in a single time slot w/o causing problems, include LpMinor Label in the primary key
                var deletedObject =  dbTop._context.LpMinors
                    .FirstOrDefaultAsync(m => m.LpMinorId == minorId).Result;
                if (deletedObject == null)  //Todo - Check on if '.Result' is needed for this
                {
                    return;
                }
                else
                {
                    dbTop._context.Remove(deletedObject);
                    dbTop._context.SaveChanges();
                }
            }

        }

        public class Person_CRUD
        {
            private DbController dbTop;
            public Person_CRUD(DbController dbController)
            {
                dbTop = dbController;
            }
            public Person? Login(string username, string psw)
            {
                //SMD - this is an insecure password structure, unsuitable for deployment
                var Found = dbTop._context.People
                    .FirstOrDefaultAsync(m => m.Person1 == username && m.PersonPw == psw).Result;
                if (Found == null)
                {//failure - login details incorrect
                    return null;
                }
                else
                {//success - login correct
                    return Found;
                }
            }
            public void Signup(Person person)   //It seems better to me to not have this specific operation be async
            {
                dbTop._context.Add(person);
                dbTop._context.SaveChanges();
            }
            public bool CheckUsernameAlreadyExists(string username)
            {
                var Found = dbTop._context.People
                    .FirstOrDefaultAsync(m => m.Person1 == username);
                if (Found.Result == null)
                {//success - username does not yet exist
                    return false;
                }
                else
                {//failure - someone's taken the username
                    return true;
                }
            }
        }
        public class Calendar_CRUD
        {
            private DbController dbTop;
            public Calendar_CRUD(DbController dbController)
            {
                dbTop = dbController;
            }

            public List<WeekCalendar> Index()
            {
                return dbTop._context.WeekCalendars.OrderBy(o => o.WeekCalendarWeekId).ToList();
            }
        }
    }
}
/*
public List<ViewSlot> Index(DateTime? week, int personID = 2)    //personID = 2 = test account's ID. temporary until multiuser support is needed
{
    if (week == null)
        week = new DateTime(1995, 1, 1);
    ///return await ... ToListAsync()
    return dbTop._context.ViewSlots.Where(v => v.WeekCalendarDate == week && v.PersonId == personID).OrderBy(o => o.WeekDay).ThenBy(o => o.WeekTimeslot).ToList();
}
 */

/*
public List<ViewSlot> Index()
{
    return _context.ViewSlots.OrderBy(o => o.WeekDay).ToList(); //=> is LINQ lambda expression syntax
}
*/
