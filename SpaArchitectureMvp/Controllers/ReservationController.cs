using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;

namespace SpaArchitectureMvp.Controllers
{
    public class ReservationController : ApiController
    {

        public ISampleService SampleService { get; set; }


        // GET: api/Reservation
        public IEnumerable<string> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET: api/Reservation/5
        public string Get(int id)
        {
            //return "Hello Yeah";
            return this.SampleService.GetMessage();
        }

        // POST: api/Reservation
        public void Post([FromBody]string value)
        {
        }

        // PUT: api/Reservation/5
        public void Put(int id, [FromBody]string value)
        {
        }

        // DELETE: api/Reservation/5
        public void Delete(int id)
        {
        }
    }
}
