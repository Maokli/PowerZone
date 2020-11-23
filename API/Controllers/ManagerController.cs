using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using API.Data;
using API.Models;
using API.Models.DTOs;
using AutoMapper;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace API.Controllers
{
    [ApiController]
    [Route("api/[controller]")]
    [Authorize(Roles = "Manager")]
    public class ManagerController : ControllerBase
    {
    private readonly IMapper _mapper;
    private UserManager<Client> _userManager;

    private IdentityContext _context;

    private RoleManager<IdentityRole> _roleManager;

    public ManagerController(IMapper mapper, IdentityContext context,
     UserManager<Client> userManager,RoleManager<IdentityRole> roleMgr)
    {
        _mapper = mapper;
        _context = context;
        _userManager = userManager;
        _roleManager = roleMgr;
    }

    //GET: Returns an array of clients
    [HttpGet]
    public async Task<ActionResult<IReadOnlyList<ClientDTO>>> GetClients()
    {
      var clients = await _context.Users.Include(x => x.MembershipType).ToListAsync();
      var data = _mapper.
      Map<IReadOnlyList<Client>,IReadOnlyList<ClientDTO>>(clients);
      return Ok(data);
    }


    //GET: returns a client based on the given id
    //recieves as params the id of the desired client to get
    [HttpGet("{id}")]
    public async Task<ActionResult<ClientDTO>> GetClient(string id)
    {
      Client client =  await _context.Users.Include(x => x.MembershipType).
      FirstOrDefaultAsync(c => c.Id == id);
      if(client != null){

        var ClientDto = _mapper.Map<Client,ClientDTO>(client);
        ClientDto.SetMembership();
        return Ok(ClientDto);
        }
      return NotFound();
    }

    //PUT: Selects a certain client depending on the id
    //Receives as params the id of the client to update and the new client
    [HttpPut("{id}")]
    public async Task<ActionResult> UpdateClient(string id, ClientDTO client){
      if(!ModelState.IsValid){
        return BadRequest("Client Data not matching Database requirements");
      }
      
      Client clientFromDb = await _context.Users.
      FirstOrDefaultAsync(c => c.Id == id);
      if(clientFromDb != null){
        _mapper.Map<ClientDTO,Client>(client,clientFromDb);
        _mapper.Map<Client,ClientDTO>(clientFromDb,client);
        await _userManager.UpdateAsync(clientFromDb);
        await _context.SaveChangesAsync();
        return Ok(client);
      }
      return NotFound("Client Not Found");
    }

    //DELETE: Deletes a client from the Db
    //Recieves the desired client id as a param
    [HttpDelete("{id}")]
    public async Task<ActionResult> DeleteClient(string id){
      Client clientFromDb = await _context.Users.
      SingleOrDefaultAsync(c => c.Id == id);
      if(clientFromDb == null)
        return NotFound("Client Not Found");
      _context.Remove(clientFromDb);
      await _context.SaveChangesAsync();
      return Ok("Client Deleted");
    }

    //POST: Adds a membership to the database
    [HttpPost("membership/add")]
    public async Task<ActionResult> AddMembership(MembershipTypeDTO membership){
        if(!ModelState.IsValid)
          return BadRequest("Membership Data Not matching database requirement");
        MembershipType newMembership = _mapper
        .Map<MembershipTypeDTO,MembershipType>(membership);

        await _context.MembershipTypes.AddAsync(newMembership);
        await _context.SaveChangesAsync();

        return Ok("Membership Added");
    }

    //PUT:Updates an existing Membership
    //Takes the desired membership id to update
    [HttpPut("membership/update/{id}")]
    public async Task<ActionResult> UpdateMembership(int id,
     MembershipTypeDTO membership){
       if(!ModelState.IsValid){
        return BadRequest("Client Data not matching Database requirements");
      }
      
      MembershipType membershipFromDb = await _context.MembershipTypes.
      FirstOrDefaultAsync(c => c.Id == id);
      if(membershipFromDb != null){
        _mapper.Map<MembershipTypeDTO,MembershipType>(membership,membershipFromDb);
        _mapper.Map<MembershipType,MembershipTypeDTO>(membershipFromDb,membership);
        await _context.SaveChangesAsync();
        return Ok(membership);
      }
      return NotFound("Membership Not Found");

    }

    [HttpGet("membership")]
    public async Task<ActionResult<IReadOnlyList<MembershipTypeDTO>>> GetMemberships(){
      var data = await _context.MembershipTypes.ToListAsync();
      return Ok(_mapper.Map<IReadOnlyList<MembershipType>,IReadOnlyList<MembershipTypeDTO>>(data));
    }

    [HttpGet("membership/{id}")]
    public async Task<ActionResult<MembershipTypeDTO>> GetMembership(int id){
      var membership = await _context.MembershipTypes
      .FirstOrDefaultAsync(m => m.Id == id);
      if(membership == null)
        return BadRequest("Desired membership does not exist");
      return Ok(_mapper.Map<MembershipType,MembershipTypeDTO>(membership));
    }

    [HttpDelete("membership/{id}")]
    public async Task<ActionResult> DeleteMembership(int id){
      var membership = await _context.MembershipTypes.FirstOrDefaultAsync(m => m.Id == id);
      if(membership == null)
        return BadRequest("Desired membership does not exist");
      _context.MembershipTypes.Remove(membership);
      await _context.SaveChangesAsync();
      return Ok("Membership Deleted");
    }

        [HttpPost("roles")]
        public async Task<IActionResult> CreateRole(Role role)
        {
            if (ModelState.IsValid)
            {
                IdentityResult result = await _roleManager.
                CreateAsync(new IdentityRole(role.Name));
                if (result.Succeeded)
                    return Ok("Role Created");
                else
                    BadRequest(result);
            }
            return BadRequest("Provided Model doesnt match database requirements");
        }

        [HttpGet("roles")]
        public async Task<ActionResult<IReadOnlyList<Role>>> GetRoles(){

          var data = await _roleManager.Roles.ToListAsync();
          return Ok(_mapper.Map<IReadOnlyList<IdentityRole>, IReadOnlyList<Role>>(data));

        }
  }

}