using _2DGameMonogameApi.Classes;
using _2DGameMonogameApi.Controllers.Interface;
using _2DGameMonogameApi.Repository.Repository.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace _2DGameMonogameApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SavesController : ControllerBase
    {
        private readonly IMapper Mapper;

        private readonly ApplicationDbContext Context;

        private readonly IGenericRepository<Saves> SavesRepository;

        public SavesController(IMapper mapper, ApplicationDbContext context, IGenericRepository<Saves> saveRepository)
        {
            Mapper = mapper;
            Context = context;
            SavesRepository = saveRepository;
           
        }

        [Authorize]
        [HttpDelete]
        public ActionResult<HttpStatusCode> Delete([FromQuery] Guid id)
        {
            try
            {

                SavesRepository.Delete(id);


                Context.SaveChanges();
                return HttpStatusCode.OK;

            }
            catch (Exception ex)
            {
                return HttpStatusCode.BadRequest;
            }
        }

        [AllowAnonymous]
        [HttpGet]
        public ActionResult<HttpStatusCode> Get([FromQuery] Guid id)
        {
            try
            {
                var p = SavesRepository.Get(id);
                if (p == null)
                {

                    return HttpStatusCode.NotFound;
                }

                else
                    return HttpStatusCode.OK;

            }
            catch (Exception ex)
            {
               
                return HttpStatusCode.NotFound;
            }
        }

     
    }
}

