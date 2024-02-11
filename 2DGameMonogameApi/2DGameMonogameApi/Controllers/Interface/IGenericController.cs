using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace _2DGameMonogameApi.Controllers.Interface
{

        public interface IGenericController<Tdo> where Tdo : class
        {
            ActionResult<HttpStatusCode> Get([FromQuery] Guid id);
            ActionResult<HttpStatusCode> Update(Tdo dto);
            ActionResult<HttpStatusCode> Add(Tdo addressDto);

            ActionResult<HttpStatusCode> Delete([FromQuery] Guid id);

        }
    
}
