using _2DGameMonogameApi.Classes;
using _2DGameMonogameApi.Controllers.Interface;
using _2DGameMonogameApi.Repository;
using _2DGameMonogameApi.Repository.Repository.Interface;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using System.Security.Claims;

namespace _2DGameMonogameApi.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GamesSavesController : ControllerBase
    {
        private readonly IMapper Mapper;

        private readonly ApplicationDbContext Context;

        private readonly IGenericRepository<GamesSaves> GamesSavesRepository;
        private readonly IGenericRepository<Saves> SavesRepository;
        private readonly UserRepository userRepository;
        public GamesSavesController(IMapper mapper, ApplicationDbContext context, IGenericRepository<GamesSaves> gamesSavesRepository, IGenericRepository<Saves> savesRepository, UserRepository userRepository)

        {
            Mapper = mapper;
            Context = context;
            GamesSavesRepository = gamesSavesRepository;
            SavesRepository = savesRepository;
            userRepository = userRepository;

        }

        [Authorize]
        [HttpDelete]
        public ActionResult<HttpStatusCode> Delete([FromQuery] Guid id)
        {
            try
            {
                var p = GamesSavesRepository.Get(id);
                p._saves.ForEach(save =>
                {
                    p._saves.Remove(save);
                    SavesRepository.Delete(save.Id);

                }); 

                GamesSavesRepository.Delete(id);


                Context.SaveChanges();
                return HttpStatusCode.OK;

            }
            catch (Exception ex)
            {
                return HttpStatusCode.BadRequest;
            }
        }

        [Authorize]
        [HttpPost]
        public ActionResult<HttpStatusCode> Add(GamesSaves gameSave)
        {
            try
            {
                if (gameSave == null)
                    return HttpStatusCode.NotFound;
                else
                {

                    gameSave._saves.ForEach(save =>
                     {
                         SavesRepository.Add(save);
                     });
                    var identity = HttpContext.User.Identity as ClaimsIdentity;
                    if (identity != null)
                    {
                        IEnumerable<Claim> claims = identity.Claims;
                        
                        var email = identity.FindFirst("email").Value;

                        var isNotBlank = userRepository.GetByEmail(email) is null ? true : false;
                        if (!isNotBlank)
                        {
                            return HttpStatusCode.BadRequest;
                        }
                        var user = userRepository.GetByEmail(email);
                        gameSave.setUser(user);


                        GamesSavesRepository.Add(gameSave);

                        return HttpStatusCode.OK;

                    }
                    else
                    {
                        return HttpStatusCode.BadRequest;

                    }
                   
                }

            }
            catch (Exception ex)
            {

                return HttpStatusCode.BadRequest;
            }
        }

        [Authorize]
        [HttpGet]
        public ActionResult<HttpStatusCode> Get([FromQuery] Guid id)
        {
            try
            {
                var p = GamesSavesRepository.Get(id);
                if (p == null)
                {

                    return HttpStatusCode.NotFound;
                }

                else
                    return Ok(p);

            }
            catch (Exception ex)
            {

                return HttpStatusCode.NotFound;
            }
        }

    }
}
