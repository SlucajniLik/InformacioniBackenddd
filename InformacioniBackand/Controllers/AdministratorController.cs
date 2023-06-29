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



        [HttpGet("getTeamsNull"), Authorize(Roles = "admin")]
        public async Task<IActionResult> getTeams()
        {
            var teams = await _db.Tim.Where(t => t.IdMenadzera == null).ToListAsync();
            return Ok(teams);
        }


        [HttpGet("results"),Authorize(Roles ="admin")]
        public async Task<IActionResult> GetResults()
        {
            var results = await _db.Rezultati.ToListAsync();
            return Ok(results);
        }


        [HttpGet("match"), Authorize(Roles = "admin")]
        public async Task<IActionResult> GetMatch()
        {
            var match = await _db.Utakmica.ToListAsync();
            return Ok(match);
        }

       [HttpGet("getTeams"), Authorize(Roles = "admin")]
        public async Task<IActionResult> getTeamsN()
        {
           var teams=await _db.Tim.ToListAsync();
            return Ok(teams);
        }


        [HttpGet("getNonApprovedMemebers"), Authorize(Roles = "admin")]
        public async Task<IActionResult> getNonApprovedMembers()
        {
            var members = await _db.Navijac.Where(t=>t.StatusReg==null).ToListAsync();
            return Ok(members);
        }


        [HttpGet("gettMemebers"), Authorize(Roles = "admin")]
        public async Task<IActionResult> getMembers()
        {
            var members = await _db.Navijac.Where(t=>t.StatusReg==true).ToListAsync();
            return Ok(members);
        }








        [HttpPut("approveMemebers/{id}/{status}")]
        public async Task<IActionResult> approveMembers(int id,bool status)
        {
            
            if(status==false)
            {
                var memeber = await _db.Navijac.FirstOrDefaultAsync(t => t.Id == id);
                _db.Navijac.Remove(memeber);
                _db.SaveChangesAsync();

            }
            else if(status==true)
            {
                var memeber = await _db.Navijac.FirstOrDefaultAsync(t => t.Id == id);
                memeber.StatusReg = true;
                _db.Navijac.Update(memeber);
                _db.SaveChangesAsync();
            }




            return Ok(null);
        }





        [HttpPost("addTeams"), Authorize(Roles = "admin")]
        public async Task<IActionResult> addTeam([FromBody]Tim team)
        {
            _db.Tim.Add(team);
            await _db.SaveChangesAsync();
            return Ok();
        }


        [HttpPost("addResults"), Authorize(Roles = "admin")]
        public async Task<IActionResult> addResult(Rezultati result)
        {
            _db.Rezultati.Add(result);
            await _db.SaveChangesAsync();
            return Ok();
        }

        [HttpPost("addMatch"), Authorize(Roles = "admin")]
        public async Task<IActionResult> addMatch([FromBody]Utakmica match)
        {
          //  var matches = await _db.Utakmica.FirstOrDefaultAsync(t=>t.)
                
                 var matches=await _db.Utakmica.FirstOrDefaultAsync(t=>t.Datum==match.Datum && (t.IdTima1==match.IdTima1
                 ||t.IdTima2==match.IdTima2 ||t.IdTima1==match.IdTima2 ||t.IdTima2==match.IdTima1));
                 
                 if (matches!=null)
                 {
                return Ok(null);
                  }



            _db.Utakmica.Add(match);
            await _db.SaveChangesAsync();
            return Ok();
        }







        [HttpGet("getResults/{id}"), Authorize(Roles = "admin")]
        public async Task<IActionResult> GetResults(int id)
        {


            var results = await this._db.Rezultati.Where(t=>t.IdTima==id).ToListAsync();

            return Ok(results);

        }




        [HttpGet("getManagers"), Authorize(Roles = "admin")]
        public async Task<IActionResult> GetManagers()
        {


            var managers = await (from m in this._db.Menazder
                          where !(from t in this._db.Tim
                                  select t.IdMenadzera).Contains(m.Id)
                          select m).ToListAsync();


           // var managers = await this._db.Menazder.ToListAsync();

            return Ok(managers);

        }











        [HttpGet("getMatches"), Authorize(Roles = "admin")]
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


        [HttpPut("editMatch/{id}"), Authorize(Roles = "admin")]
        public async Task<IActionResult> editMatch(  int id, [FromBody] DtoUtakmica utm)
        {

           var mat =await  _db.Utakmica.FirstOrDefaultAsync(t => t.Id == id);


            mat.Rezultat = utm.rezultat;
            mat.BrCrvenihKartona = utm.brCrvenihKartona;
            mat.BrZutihKartona = utm.brZutihKartona;

            _db.Utakmica.Update(mat);
            await _db.SaveChangesAsync();
            return Ok(mat);
        }



        [HttpPost("addResult"), Authorize(Roles = "admin")]
        public async Task<IActionResult> addResults([FromBody] Rezultati result)
        {
            //  var matches = await _db.Utakmica.FirstOrDefaultAsync(t=>t.)


         
           
                _db.Rezultati.AddAsync(result);
           
            await _db.SaveChangesAsync();
            return Ok();
        }




        [HttpPut("updateResult"), Authorize(Roles = "admin")]
        public async Task<IActionResult> updateResults([FromBody] Rezultati result)
        {
            //  var matches = await _db.Utakmica.FirstOrDefaultAsync(t=>t.)




            _db.Rezultati.Update(result);

            await _db.SaveChangesAsync();
            return Ok();
        }


        [HttpGet("getTeamResult/{id}/{sez}"), Authorize(Roles = "admin")]
        public async Task<IActionResult> getTeamResults(int id ,string sez)
        {
            var result = await _db.Rezultati.FirstOrDefaultAsync(t => t.IdTima == id && t.Sezona == sez);

            if(result== null)
            {
                return Ok(null);
            }


           

            
            return Ok(result);
        }



        [HttpPost("addPayment"), Authorize(Roles = "admin")]
        public async Task<IActionResult> addPayment([FromBody] Uplata uplata)
        {
              var uplata1=await _db.Uplata.FirstOrDefaultAsync(t => t.IdNavijaca==uplata.IdNavijaca);

            if(uplata1!=null)
            {
                uplata1.DatumPlacanja = uplata.DatumPlacanja;
                uplata1.Suma = uplata.Suma;

                _db.Uplata.Update(uplata1);
                await _db.SaveChangesAsync();

                return Ok();
            }




            _db.Uplata.Add(uplata);
            await _db.SaveChangesAsync();
            return Ok();
        }



        [HttpGet("getPaymentMember/{id}"), Authorize(Roles = "admin")]
        public async Task<IActionResult> getPaymentMember(int id)
        {
            // var payment = await _db.Uplata.FirstOrDefaultAsync(t => t.IdNavijaca == id);

            var payment = await (from a in _db.Uplata
                                 join
                                 b in _db.Navijac
                                 on a.IdNavijaca equals b.Id
                                 join c in _db.Tim
                                 on b.IdTima equals c.Id
                                 where a.IdNavijaca==id
                                 select new
                                 {
                                     ime = b.Ime,
                                     prezime = b.Prezime,
                                     datumPlacanja = a.DatumPlacanja,
                                     suma = a.Suma,
                                     brojClanske=a.IdNavijaca,
                                     imeTima=c.Naziv,
                                     logoTima=c.Logo



                                 }).FirstOrDefaultAsync();
                             
            if(payment==null)
            {
                return Ok(null);    
            }
            return Ok(payment);
        }




    }
}
