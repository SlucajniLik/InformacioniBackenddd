using InformacioniBackand.DataContext;
using InformacioniBackand.Dto;
using InformacioniBackand.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;

namespace InformacioniBackand.Controllers
{
    public class MenadzerController : ControllerBase
    {
        private readonly DataContexDb _db;

        public MenadzerController(DataContexDb db)
        {
            _db = db;
        }


        [HttpGet("getPlayers/{id}")]
        public async Task<IActionResult> GetPlayers(int id)
        {
            var players = await _db.Igraci.Where(t=>t.IdTima==id).ToListAsync();
            return Ok(players);
        }




        [HttpPost("addPlayers")]
        public async Task<IActionResult> addPlayer([FromBody]Igraci player)
        {
            _db.Igraci.Add(player);
            await _db.SaveChangesAsync();
            return Ok();
        }




        [HttpPut("choseTeam/{id}/{idMenadzera}")]
        public async Task<IActionResult> editTeam(int id,int idMenadzera)
        {

            var tim = await _db.Tim.FirstOrDefaultAsync(t => t.Id == id);

            tim.IdMenadzera=idMenadzera;
           

            _db.Tim.Update(tim);
            await _db.SaveChangesAsync();
            return Ok();
        }


        [HttpGet("getTeamsManager/{id}")]
        public async Task<IActionResult> getTeams(int id)
        {
            var teams = await _db.Tim.Where(t=>t.IdMenadzera==id).ToListAsync();
            return Ok(teams);
        }



        [HttpGet("getTeamsNull")]
        public async Task<IActionResult> getTeams()
        {
            var teams = await _db.Tim.Where(t=>t.IdMenadzera==null).ToListAsync();
            return Ok(teams);
        }





    }
}
