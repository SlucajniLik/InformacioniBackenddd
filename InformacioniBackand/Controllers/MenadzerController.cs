﻿using InformacioniBackand.DataContext;
using InformacioniBackand.Dto;
using InformacioniBackand.Entities;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using System.Data;
using System.Runtime.InteropServices;

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
            var players = await _db.Igraci.Where(t=>t.IdTima==id  && t.Postava==null).ToListAsync();
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




        [HttpGet("getManagerTeam/{id}")]
        public async Task<IActionResult> GetManagerTeam(int id)
        {


            var team= await this._db.Tim.FirstOrDefaultAsync(t=>t.IdMenadzera==id);

            return Ok(team);

        }



        [HttpGet("getPlayersLineUp/{id}/{type}")]
        public async Task<IActionResult> GetPlayersFirst(int id,bool type)
        {
            var players = await _db.Igraci.Where(t => t.IdTima == id && t.Postava==type).ToListAsync();
            return Ok(players);
        }


       



        [HttpPut("editLineUp/{id}/{type}")]
        public async Task<IActionResult> EditLineUp(int id,bool? type)
        {

            var player = await _db.Igraci.FirstOrDefaultAsync(t => t.Id == id);



            player.Postava = type;


            _db.Igraci.Update(player);

            _db.SaveChangesAsync();





            return Ok(player);
        }







    }
}
