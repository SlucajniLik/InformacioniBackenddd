using InformacioniBackand.DataContext;
using InformacioniBackand.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace InformacioniBackand.Controllers
{
    public class NavijacController : ControllerBase
    {
        private readonly DataContexDb _db;

        public NavijacController(DataContexDb db)
        {
            _db = db;
        }


        [HttpPost("addTeam"), Authorize(Roles ="admin")]
        public async Task<IActionResult> addTeam(Tim team)
        {
           await  _db.Tim.AddAsync(team);
           await _db.SaveChangesAsync();



          
            return Ok(team);

        }




    }
}
