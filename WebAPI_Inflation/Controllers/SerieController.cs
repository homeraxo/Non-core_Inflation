using System;
using System.Configuration;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Runtime.Serialization.Json;
using WebAPI_Inflation.Models;

namespace WebAPI_Inflation.Controllers
{
    public class SerieController : ApiController
    {
        // GET: api/Serie
        public DataSerie[] Get()
        {
            DataSerie[] dataSeries = new DataSerie[] { };
            try
            {
                string url = "https://www.banxico.org.mx/SieAPIRest/service/v1/series/SP74665/datos/";
                HttpWebRequest request = WebRequest.Create(url) as HttpWebRequest;
                request.Accept = "application/json";

                string bmxtoken = ConfigurationManager.AppSettings["BmxToken"].ToString().Trim();

                request.Headers["Bmx-Token"] = bmxtoken; //"7129bfea1f989fca2990db9941007a1798de3b0d2cc6b508626aeb151700832c";

                HttpWebResponse response = request.GetResponse() as HttpWebResponse;

                if (response.StatusCode != HttpStatusCode.OK)
                    throw new Exception(String.Format("Server error (HTTP {0}: {1}).", response.StatusCode, response.StatusDescription));

                DataContractJsonSerializer jsonSerializer = new DataContractJsonSerializer(typeof(Response));
                object objResponse = jsonSerializer.ReadObject(response.GetResponseStream());
                Response jsonResponse = objResponse as Response;

                dataSeries = jsonResponse.seriesResponse.series[0].Data;

                return dataSeries;
            }
            catch (Exception e)
            {
                Console.WriteLine(e.Message);
            }

            return dataSeries;
        }

        // GET: api/Serie
        //public IEnumerable<string> Get()
        //{
        //    return new string[] { "value1", "value2" };
        //}

        // GET: api/Serie/5
        public string Get(int id)
        {
            return "value";
        }

        // POST: api/Serie
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Serie/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Serie/5
        public void Delete(int id)
        {
        }
    }
}
