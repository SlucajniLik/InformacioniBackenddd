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


        [HttpPost("addTeam"), Authorize(Roles ="navijac")]
        public async Task<IActionResult> addTeam(Tim team)
        {
           await  _db.Tim.AddAsync(team);
           await _db.SaveChangesAsync();



          
            return Ok(team);

        }



        [HttpGet("getTeamsMember/{id}"), Authorize(Roles = "navijac")]
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


        [HttpGet("getTeamMemeberInforrmation/{id}") ,Authorize(Roles = "navijac")]
        public async Task<IActionResult> TeamMemeberInforrmation(int? id)
        {

            var member = await _db.Navijac.FirstOrDefaultAsync(t => t.Id == id);


           



            return Ok(member);
        }








        [HttpPut("editMemeberTeam/{idMemeber}/{idTeam}"), Authorize(Roles = "navijac")]
        public async Task<IActionResult> editTeamsMember(int idMemeber,int idTeam)
        {
            
            var member= await _db.Navijac.FirstOrDefaultAsync(t=>t.Id==idMemeber); 


            member.IdTima=idTeam;


            _db.Navijac.Update(member);    

            await _db.SaveChangesAsync();   





            return Ok(member);
        }




        [HttpGet("getResultForMember/{id}"), Authorize(Roles = "navijac")]
        public async Task<IActionResult> getResultForMember(int id)
        {
            var matches = await _db.Rezultati.Where(t=>t.IdTima==id).ToListAsync();




            return Ok(matches);

        }



        [HttpGet("getMatchesForMember/{id}/{sezona}"), Authorize(Roles = "navijac")]
        public async Task<IActionResult> getMatchesForMember(int id,string sezona)
        {

            var matches = await (from a in _db.Utakmica


                                 select new
                                 {
                                     Utakmica = a,
                                     Tim1 = _db.Tim.FirstOrDefault(t => t.Id == a.IdTima1).Naziv,
                                     Tim2 = _db.Tim.FirstOrDefault(t => t.Id == a.IdTima2).Naziv,





                                 }).ToListAsync();
            var matchess = matches.Where(t => t.Utakmica.Datum.Split("-")[0] == sezona && (t.Utakmica.IdTima1==id || t.Utakmica.IdTima2==id));



            return Ok(matchess);

        }

        


 [HttpGet("getMemberData/{id}"), Authorize(Roles = "navijac")]
        public async Task<IActionResult> getMemberdata(int id)
        {
            // var payment = await _db.Uplata.FirstOrDefaultAsync(t => t.IdNavijaca == id);

            var payment = await (from a in _db.Uplata
                                 join
                                 b in _db.Navijac
                                 on a.IdNavijaca equals b.Id
                                 join c in _db.Tim
                                 on b.IdTima equals c.Id
                                 where a.IdNavijaca == id
                                 select new
                                 {
                                     ime = b.Ime,
                                     prezime = b.Prezime,
                                     datumPlacanja = a.DatumPlacanja,
                                     suma = a.Suma,
                                     imeTima=c.Naziv,
                                     logo=c.Logo




                                 }).FirstOrDefaultAsync();

            if (payment == null)
            {
                return Ok(null);
            }
            return Ok(payment);
        }


    }
}
