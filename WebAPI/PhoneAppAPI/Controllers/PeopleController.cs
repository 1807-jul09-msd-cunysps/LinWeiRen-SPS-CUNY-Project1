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
        public HttpResponseMessage Post([FromBody]DeserializedPerson person)
        {
            try
            {
                DatabaseAction.AddNewContact(person);
                DatabaseAction.Save();
                var message = Request.CreateResponse(HttpStatusCode.Created,person);
                return message;
            }
            catch (Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }
    }
}
