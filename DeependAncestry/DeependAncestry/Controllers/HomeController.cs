using DeependAncestry.Models;
using DeependAncestry.Repository;
using PagedList;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DeependAncestry.Controllers
{
    public class HomeController : Controller
    {
        DARepository repo = new iDARepository("/App_Data/data_large.json");

        public ActionResult Index(string searchString, bool? chkMale,bool? chkFemale, int? page)
        {
            //Blank list
            List<Person> persons = new List<Person>();
            IPagedList<Person> items = new PagedList<Person>(persons, 1, 10);
                        DataSet _DataSet = repo.GetPersonData();
                        int pageSize = 10;
            int pageIndex = 1;
            List<string> gender = new List<string>();
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;

            ViewBag.CurrentFilter = searchString;
            
            //Setting the Gender
            if (chkMale.HasValue)
            {
                ViewBag.GenderM = (bool)chkMale;
                if (ViewBag.GenderM)
                    gender.Add ("M");
            }
            else
            {
                ViewBag.GenderM = false;
            }

            if (chkFemale.HasValue)
            {
                ViewBag.GenderF = (bool)chkFemale;
                if(ViewBag.GenderF)
                gender.Add("F");
            }
            else
            {
                ViewBag.GenderF = false;
            }

            if (searchString != null && searchString != "")
            {
                /* filtering person based on name and gender */
                items = (from ppl in _DataSet.Tables["people"].AsEnumerable()
                         join pl in _DataSet.Tables["places"].AsEnumerable()
                         on Convert.ToInt32(ppl["place_id"]) equals Convert.ToInt32(pl["id"])
                         where ((string)ppl["name"]).ToLower().Contains(searchString.ToLower())
                         where (gender.Count ==0 || gender.Contains((string)ppl["gender"]))
                         select new Person
                         {
                             Id = Convert.ToInt32(ppl["id"]),
                             Name = (string)ppl["name"],
                             Gender = (string)ppl["gender"] == "F" ? "Female" : "Male",
                             Father_Id = ppl["father_id"] == DBNull.Value ? 0 : Convert.ToInt32(ppl["father_id"]),
                             Mother_Id = ppl["mother_id"] == DBNull.Value ? 0 : Convert.ToInt32(ppl["mother_id"]),
                             Place = (string)pl["name"],
                             Level = ppl["level"] == DBNull.Value ? 0 : Convert.ToInt32(ppl["level"])
                         }).ToPagedList(pageIndex, pageSize);
            }

            return View(items);
        }

    }
}