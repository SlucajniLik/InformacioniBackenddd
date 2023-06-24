using InformacioniBackand.DataContext;
using InformacioniBackand.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

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



        [HttpGet("getTeamsMember/{id}")]
        public async Task<IActionResult> getTeamsMember(int? id)
        {

           var member=await _db.Navijac.FirstOrDefaultAsync(t=>t.Id==id);   


            if (member.IdTima==null)
            {
                var teams = await _db.Tim.ToListAsync();
                return Ok(teams);
            }


           
            return Ok(null);
        }


        [HttpGet("getTeamMemeberInforrmation/{id}")]
        public async Task<IActionResult> TeamMemeberInforrmation(int? id)
        {

            var member = await _db.Navijac.FirstOrDefaultAsync(t => t.Id == id);


           



            return Ok(member);
        }








        [HttpPut("editMemeberTeam/{idMemeber}/{idTeam}")]
        public async Task<IActionResult> editTeamsMember(int idMemeber,int idTeam)
        {
            
            var member= await _db.Navijac.FirstOrDefaultAsync(t=>t.Id==idMemeber); 


            member.IdTima=idTeam;


            _db.Navijac.Update(member);    

            await _db.SaveChangesAsync();   





            return Ok(member);
        }




    }
}
