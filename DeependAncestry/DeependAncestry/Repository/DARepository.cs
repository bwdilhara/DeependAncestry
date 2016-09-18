using DeependAncestry.Models;
using Newtonsoft.Json;
using System.Collections.Generic;
using System.Data;
using System.IO;

namespace DeependAncestry.Repository
{
    public class DARepository
    {
        DataSet _DataSet = new DataSet();

        public DARepository(string jsonFilePath)
        {
            using (StreamReader r = new StreamReader(System.Web.HttpContext.Current.Server.MapPath("~" + jsonFilePath)))
            {
                string json = r.ReadToEnd();
                _DataSet = JsonConvert.DeserializeObject<System.Data.DataSet>(json);
            }
        }

        public DataSet GetPersonData()
        {
            return _DataSet;
        }
    }
}