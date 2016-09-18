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

        public ActionResult Index(string searchString, string gender, int? page)
        {
            
        //public HomeController(iDARepository postRepository)
        //{
        //    _iDARepository = postRepository;
        //}
        //Blank list
        List<Person> persons = new List<Person>();
            IPagedList<Person> items = new PagedList<Person>(persons, 1, 10);

            //public HomeController()
            ////{
            //using (StreamReader r = new StreamReader(Server.MapPath("~/App_Data/data_large.json")))
            //{
            //    string json = r.ReadToEnd();
            //    _DataSet = JsonConvert.DeserializeObject<DataSet>(json);
            //}
            //Json data from the repo
            //DARepository repo = new iDARepository("/App_Data/data_large.json");
            DataSet _DataSet= repo.GetPersonData();

            int pageSize = 10;
            int pageIndex = 1;
            pageIndex = page.HasValue ? Convert.ToInt32(page) : 1;
          
            ViewBag.CurrentFilter = searchString;
            ViewBag.Gender = gender;

            //Personplace ro = JsonConvert.DeserializeObject<Personplace>(json);

            if (searchString != null && searchString != "")
            {
                /* IPagedList<Person> */
                items = (from ppl in _DataSet.Tables["people"].AsEnumerable()
                         join pl in _DataSet.Tables["places"].AsEnumerable()
                         on Convert.ToInt32(ppl["place_id"]) equals Convert.ToInt32(pl["id"])
                         where ((string)ppl["name"]).ToLower().Contains(searchString.ToLower())
                         where (gender == null || gender.Length > 1 || (string)ppl["gender"] == gender.ToUpper().Substring(1))
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
                //}b
            }

            return View(items);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}