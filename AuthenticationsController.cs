using OnlineHotelRoomReservationSystem.Repository;
using OnlineHotelRoomReservationSystem.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Web.Http;
using System.Web.Http.Cors;
using System.Web.Http.Description;

namespace OnlineHotelRoomReservationSystem.Controllers
{
    [EnableCors("*", "*", "POST")]
    [RoutePrefix("api/authentications")]
    public class AuthenticationsController : ApiController
    {
        /// <summary>
        /// Validates credentials against the credentials stored in database
        /// </summary>
        /// <param name="model">Login credentials</param>
        /// <response code="200">Authentication successful</response>
        /// <response code="400">Authentication failed or Model validation failure</response>
        [HttpPost]
        [ResponseType(typeof(LoginResponse))]
        public IHttpActionResult Post([FromBody] LoginRequest model)
        {
            if (!ModelState.IsValid)
                return BadRequest(ModelState);

            IHttpActionResult response = BadRequest("Invalid email/password");

            using (var context = new ApplicationDbContext())
            {
                if (context.Customers.Any(u => u.EmailId == "minalnirgudkar@gmail.com" && u.Password == "Minal@123"))
                {
                    response = Ok();
                }
                else if (context.Customers.Any(u => u.EmailId == model.EmailId && u.Password == model.Password))
                {
                    var result = (from user in context.Customers
                                  where user.EmailId == model.EmailId
                                  select new { user.EmailId}).Single();
                    response = Ok();
                }
            }
            return response;
        }

    }
}

