using _2DGameMonogameApi.Authentification.Interface;
using _2DGameMonogameApi.Classes;
using _2DGameMonogameApi.Repository;
using _2DGameMonogameApi.Repository.Interface;
using _2DGameMonogameApi.Repository.Repository.Interface;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Components;
using Microsoft.AspNetCore.Mvc;
using System.Net;

namespace _2DGameMonogameApi.Controllers
{

	namespace ApiApplication.Controllers
	{

		[Microsoft.AspNetCore.Components.Route("api/[controller]")]
		[ApiController]
		public class UserController : ControllerBase
		{
			private IUserRepository _userRepository { get; set; }
			private readonly IJWTManagerRepository JwtManager;
			public UserController(IJWTManagerRepository jwtManager, IUserRepository userRepository)
			{
				JwtManager = jwtManager;
				_userRepository = userRepository;

			}

			[AllowAnonymous]
			[HttpPost]
			[Microsoft.AspNetCore.Mvc.Route("createaccount")]
			public IActionResult CreateAccount(Users usersdata)
			{
				try
				{
					JwtManager.CreateUser(usersdata);
					
					return Ok();

				}
				catch (Exception ex)
				{
					return BadRequest();
				}

			}



			[AllowAnonymous]
			[HttpPost]
			[Microsoft.AspNetCore.Mvc.Route("authenticate")]
			public IActionResult Authenticate(Users usersdata)
			{

				var token = JwtManager.Authenticate(usersdata);

				if (token == null)
				{
					return BadRequest(new { message = "Username or password is incorrect" });
				}


				return Ok(token);



			}

			[Authorize]
			[HttpGet]
			[Microsoft.AspNetCore.Mvc.Route("get")]
			public ActionResult<Users> Get([FromQuery] Guid id)
			{
				try
				{
					var p = _userRepository.Get(id);	
					if (p == null)
					{

						return NotFound();
					}

					else
						return Ok(p);

				}
				catch (Exception ex)
				{

					return NotFound();
				}
			}

		}
	}
}
