using InformacioniBackand.DataContext;
using InformacioniBackand.Dto;
using InformacioniBackand.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Text.RegularExpressions;

namespace InformacioniBackand.Controllers
{
    public class AdministratorController : ControllerBase
    {
        private readonly DataContexDb _db;

        public AdministratorController(DataContexDb db)
        {
            _db = db;
        }


       



        [HttpGet("results")]
        public async Task<IActionResult> GetResults()
        {
            var results = await _db.Rezultati.ToListAsync();
            return Ok(results);
        }


        [HttpGet("match")]
        public async Task<IActionResult> GetMatch()
        {
            var match = await _db.Utakmica.ToListAsync();
            return Ok(match);
        }

        [HttpGet("getTeams")]
        public async Task<IActionResult> getTeams()
        {
           var teams=await _db.Tim.ToListAsync();
            return Ok(teams);
        }




        [HttpPost("addTeams")]
        public async Task<IActionResult> addTeam([FromBody]Tim team)
        {
            _db.Tim.Add(team);
            await _db.SaveChangesAsync();
            return Ok();
        }


        [HttpPost("addResults")]
        public async Task<IActionResult> addResult(Rezultati result)
        {
            _db.Rezultati.Add(result);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("addMatch")]
        public async Task<IActionResult> addMatch([FromBody]Utakmica match)
        {
            _db.Utakmica.Add(match);
            await _db.SaveChangesAsync();
            return Ok();
        }





        [HttpGet("getMatches")]
        public async Task<IActionResult> GetMatches()
        {


            var matches =await (from a in _db.Utakmica

                              
                                 select new
                                 {
                                      Utakmica= a,
                                      Tim1=_db.Tim.FirstOrDefault(t=>t.Id==a.IdTima1).Naziv,
                                      Tim2 = _db.Tim.FirstOrDefault(t => t.Id == a.IdTima2).Naziv,
                                      




                                 }).ToListAsync();

            return Ok(matches); 

        }


        [HttpPut("editMatch/{id}")]
        public async Task<IActionResult> editMatch(  int id, [FromBody] DtoUtakmica utm)
        {

           var mat =await  _db.Utakmica.FirstOrDefaultAsync(t => t.Id == id);

            mat.Rezultat = utm.rezultat;
            mat.BrCrvenihKartona = utm.brCrvenihKartona;
            mat.BrZutihKartona = utm.brZutihKartona;

            _db.Utakmica.Update(mat);
            await _db.SaveChangesAsync();
            return Ok();
        }





    }
}
