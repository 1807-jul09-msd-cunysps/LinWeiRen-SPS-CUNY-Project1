using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using PhoneAppAPI.Models;
using System.Web.Http.Cors;

namespace PhoneAppAPI.Controllers
{
    [EnableCors("*", "*", "*")]
    public class ContactMessageController : ApiController
    {
        [HttpPost]
        public HttpResponseMessage Post([FromBody]Contact_message message)
        {
            try
            {
                using(ContactMessage db = new ContactMessage())
                {
                    db.Contact_message.Add(message);
                    db.SaveChanges();
                }
                var resonse = Request.CreateResponse(HttpStatusCode.Created);
                return resonse;
            }
            catch(Exception ex)
            {
                return Request.CreateErrorResponse(HttpStatusCode.BadRequest, ex);
            }
        }

    }
}
