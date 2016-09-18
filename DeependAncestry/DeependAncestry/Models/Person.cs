using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DeependAncestry.Models
{
    public class Person
    {
        public int Id { get; set; }
        public string Name { get; set; }
        public string Gender { get; set; }
        public int Father_Id { get; set; }
        public int Mother_Id { get; set; }
        //public int place_id { get; set; }
        public string Place { get; set; }
        public int Level { get; set; }
    }
}