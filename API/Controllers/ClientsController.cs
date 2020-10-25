using System.Collections.Generic;
using System.Linq;
using API.Data;
using API.Models;
using Microsoft.AspNetCore.Mvc;
using AutoMapper;
using System.Threading.Tasks;
using API.Models.DTOs;
using System;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Authorization;
using API.Services;

namespace API.Controllers
{

  [ApiController]
  [Route("api")]
  public class ClientsController : ControllerBase
  {
    private readonly IdentityContext _context;
    private readonly IMapper _mapper;

    private UserManager<Client> _userManager;
    private SignInManager<Client> _signInManager;

    private readonly ITokenService _tokenService;

    public ClientsController(UserManager<Client> userManager,
     SignInManager<Client> signInManager, IdentityContext context,
     IMapper mapper, ITokenService tokenService)
    {
      _tokenService = tokenService;
      _userManager = userManager;
      _signInManager = signInManager;
      _mapper = mapper;
      _context = context;
    }

    [AllowAnonymous]
    //[ValidateAntiForgeryToken]
    [HttpPost("signup")]
    public async Task<ActionResult> SignUp(ClientDTO client)
    {

      if (!ModelState.IsValid)
      {
        return BadRequest("Client Data not matching Database requirements");
      }

      Client inUseClient = new Client();

      if (!String.IsNullOrEmpty(client.Email))
      {
        inUseClient = await _userManager.FindByNameAsync(client.Username);
        if (inUseClient != null)
          return BadRequest("Username in use, please choose another one");
      }

      if (!String.IsNullOrEmpty(client.Username))
      {
        inUseClient = await _userManager.FindByEmailAsync(client.Email);
        if (inUseClient != null)
          return BadRequest("Email in Use, Have you forgotten your password ?");
      }
      client.Role = "Client";
      var clientToCreate = _mapper.Map<ClientDTO, Client>(client);

      var result = await _userManager.CreateAsync(clientToCreate, client.Password);
      if (result.Succeeded)
      {
        await _userManager.AddToRoleAsync(clientToCreate, clientToCreate.Role);
        var roles = _userManager.GetRolesAsync(clientToCreate).Result;
        clientToCreate.Role = roles.First();
        return Ok(new ClientLogin{
          Username = clientToCreate.UserName,
          Email = clientToCreate.Email,
          Role = clientToCreate.Role,
          Token = _tokenService.CreateToken(clientToCreate)
        });
      }

      return BadRequest(result.Errors);
    }

    [AllowAnonymous]
    // [ValidateAntiForgeryToken]
    [HttpPost("login")]
    public async Task<ActionResult> Login(ClientLogin client)
    {

      if (!ModelState.IsValid)
      {
        return BadRequest("Client Data not matching Database requirements");
      }
      Client user = new Client();
      if (String.IsNullOrEmpty(client.Email))
        user = await _userManager.FindByNameAsync(client.Username);

      if (String.IsNullOrEmpty(client.Username))
        user = await _userManager.FindByEmailAsync(client.Email);

      if (user == null)
      {
        return BadRequest("Wrong Email or Username");
      }
      var result = await _signInManager.CheckPasswordSignInAsync(user,
      client.Password, false);
      if (result.Succeeded)
      {
        var roles = _userManager.GetRolesAsync(user).Result;
        user.Role = roles.First();
        return Ok(new ClientLogin{
          Username = user.UserName,
          Email = user.Email,
          Role = roles.First(),
          Token = _tokenService.CreateToken(user)
        });
      }

      return BadRequest("Wrong Password");
    }


  }
}