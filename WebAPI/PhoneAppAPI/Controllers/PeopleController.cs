using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using ContactLibraryAccess;
using ContactLibrary;

namespace PhoneAppAPI.Controllers
{
    [EnableCors("*", "*", "*")]
    public class PeopleController : ApiController
    {
        [HttpGet]
        public IHttpActionResult Get()
        {
            var result = DatabaseAction.GetPeople();
            return Ok(result);

        }
        [HttpPost]
        public HttpResponseMessage Post([FromBody]Person person)
        {
            using (PhoneAppDatabaseContext db = new PhoneAppDatabaseContext())
            {
                try
                {
                    db.People.Add(person);
                    db.SaveChanges();
                    var message = Request.CreateResponse(HttpStatusCode.Created);
                    return message;
                }
                catch(Exception ex)
                {
                    var message = Request.CreateResponse(HttpStatusCode.BadRequest, ex);
                    return message;
                }
            }
        }
    }
}
