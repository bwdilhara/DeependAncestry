using DeependAncestry.Models;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Web;

namespace DeependAncestry.Repository
{
    public class iDARepository: DARepository
    {
        public iDARepository(string jsonFilePath) : base(jsonFilePath)
        {
        }

        public DataSet GetPersonData()
        {
            return this.GetPersonData();
        }
    }
}