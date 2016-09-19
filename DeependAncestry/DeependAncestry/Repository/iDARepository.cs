using DeependAncestry.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DeependAncestry.Repository
{
    public interface iDARepository
    {
        //public iDARepository(string jsonFilePath) : base(jsonFilePath)
        //{
        //}

         DataSet GetPersonData();
    }
}