using InformacioniBackand.DataContext;
using InformacioniBackand.Dto;
using InformacioniBackand.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using Microsoft.Extensions.Primitives;
using Microsoft.AspNetCore.Authorization;

namespace InformacioniBackand.Controllers
{
    public class KorisnikController : ControllerBase
    {
        private readonly DataContexDb _db;
        private readonly IConfiguration config;
        public KorisnikController(DataContexDb db, IConfiguration configuration)
        {
            _db = db;
            config=configuration;
        }



        [HttpPost("register/{type}")]
        public async Task<IActionResult> register([FromBody] DtoUser user, string type)
        {

            var admin = await _db.Administrator.FirstOrDefaultAsync(t => t.KorisnickoIme == user.KorisnickoIme);
               

            var menadzer = await _db.Menazder.FirstOrDefaultAsync(t => t.KorisnickoIme == user.KorisnickoIme);
           

            var navijac = await _db.Navijac.FirstOrDefaultAsync(t => t.KorisnickoIme == user.KorisnickoIme);
            if (admin != null ||menadzer!=null||navijac!=null)
            { return Ok("ne"); }





            byte[] passwordHash, passwordKey;

            using (var hmac = new HMACSHA512())
            {
                passwordKey = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(user.Lozinka));
            }

            if (type == "admin")
            {
                Administrator adminn = new Administrator();
                adminn.LozinkaKljuc = passwordKey;
                adminn.Ime = user.Ime;
                adminn.Prezime = user.Prezime;
                adminn.KorisnickoIme = user.KorisnickoIme;
                adminn.Lozinka = passwordHash;
                _db.Administrator.Add(adminn);
                await _db.SaveChangesAsync();
                return Ok();



            }
            else if(type=="menadzer")
            {

                Menadzer managerr = new Menadzer();
                managerr.LozinkaKljuc = passwordKey;
                managerr.Ime = user.Ime;
                managerr.Prezime = user.Prezime;
                managerr.KorisnickoIme = user.KorisnickoIme;
                managerr.Lozinka = passwordHash;





                _db.Menazder.Add(managerr);
                await _db.SaveChangesAsync();
                return Ok();

            }
            else if(type=="navijac")
            {
                Navijac memberFann = new Navijac();
                memberFann.LozinkaKljuc = passwordKey;
                memberFann.Ime = user.Ime;
                memberFann.Prezime = user.Prezime;
                memberFann.KorisnickoIme = user.KorisnickoIme;
                memberFann.Lozinka = passwordHash;




                _db.Navijac.Add(memberFann);
                await _db.SaveChangesAsync();
                return Ok();

            }

            return Ok();
           
        }


























        /* [HttpPost("registerAdmin")]
         public async Task<IActionResult> registerAdmin([FromBody]DtoAdmin admin)
         {


             byte[] passwordHash, passwordKey;

             using (var hmac = new HMACSHA512())
             {
                 passwordKey = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(admin.Lozinka ));
             }

             Administrator adminn= new Administrator();
             adminn.LozinkaKljuc = passwordKey;
             adminn.Ime=admin.Ime;
             adminn.Prezime=admin.Prezime;
             adminn.KorisnickoIme=admin.KorisnickoIme;
             adminn.Lozinka = passwordHash;
             _db.Administrator.Add(adminn);
             await _db.SaveChangesAsync();
             return Ok();
         }



         [HttpPost("registerManager")]
         public async Task<IActionResult> registerManager([FromBody] DtoMenadzer manager)
         {
             byte[] passwordHash, passwordKey;
             using (var hmac = new HMACSHA512())
             {
                 passwordKey = hmac.Key;
                 passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(manager.Lozinka));
             }

             Menadzer managerr = new Menadzer();
             managerr.LozinkaKljuc = passwordKey;
             managerr.Ime = manager.Ime;
             managerr.Prezime = manager.Prezime;
             managerr.KorisnickoIme = manager.KorisnickoIme;
             managerr.Lozinka = passwordHash;





             _db.Menazder.Add(managerr);
             await _db.SaveChangesAsync();
             return Ok();
         }


         [HttpPost("registerMemmerFan")]
         public async Task<IActionResult> registerMemmerFan([FromBody] DtoNavijac memberFan)
         {
             byte[] passwordHash, passwordKey;

             using (var hmac = new HMACSHA512())
             {
                 passwordKey = hmac.Key;
                 passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(memberFan.Lozinka));
             }


             Navijac memberFann = new Navijac();
             memberFann.LozinkaKljuc = passwordKey;
             memberFann.Ime = memberFan.Ime;
             memberFann.Prezime = memberFan.Prezime;
             memberFann.KorisnickoIme = memberFan.KorisnickoIme;
             memberFann.Lozinka = passwordHash;




             _db.Navijac.Add(memberFann);
             await _db.SaveChangesAsync();
             return Ok();
         }*/


        [HttpPost("login")]
        public async Task<IActionResult> login([FromBody] DtoLogin user)
        {


            string token = null;
            
            DtoUserJwt dtoUser = new DtoUserJwt();

            var admin = await _db.Administrator!.FirstOrDefaultAsync(x => x.KorisnickoIme == user.KorisnickoIme );


            var menadzer = await _db.Menazder!.FirstOrDefaultAsync(x => x.KorisnickoIme == user.KorisnickoIme );
            var navijac = await _db.Navijac!.FirstOrDefaultAsync(x => x.KorisnickoIme == user.KorisnickoIme );


            if(admin!=null)
            {
                if (!MatchPasswordHash(user.Lozinka, admin.Lozinka, admin.LozinkaKljuc))
                {
                    return Ok("ne");
                }



               

                dtoUser.KorisnickoIme = admin.KorisnickoIme;
                dtoUser.Id = admin.Id;
                dtoUser.Tip = "admin";

                var tip = "admin";

             



                 token = CreateJWT(dtoUser);


                var msg = new
                {
                    
                    token = token,
                    tip = tip,

                };
                return Ok(token) ;


            }
            else if(menadzer!=null)
            {
                if (!MatchPasswordHash(user.Lozinka, menadzer.Lozinka, menadzer.LozinkaKljuc))
                {
                    return Ok("ne");
                }

                

                dtoUser.KorisnickoIme = menadzer.KorisnickoIme;
                dtoUser.Id = menadzer.Id;
                dtoUser.Tip = "menadzer";
                var tip = "menadzer";




                token = CreateJWT(dtoUser);



                var msg = new
                {
                    tip = tip,
                    token = token
                    
                };
                return Ok(token);




            }
            else if (navijac != null)
            {
                if (!MatchPasswordHash(user.Lozinka, navijac.Lozinka, navijac.LozinkaKljuc))
                {
                    return Ok("ne");
                }

                if (navijac.StatusReg == null)
                {
                    return Ok("nonAllowed");
                }



                dtoUser.KorisnickoIme = navijac.KorisnickoIme;
                dtoUser.Id = navijac.Id;
                dtoUser.Tip = "navijac";

                var tip = "navijac";



                token = CreateJWT(dtoUser);

                var msg = new
                {
                    tip = tip,
                    token = token
                   
                };
                return Ok(token);
            }
            else
            {
                return Ok("nel");
            }    
        }


































       /* [HttpPost("loginAdmin")]
        public async Task<IActionResult> loginAdmin([FromBody]LoginDtoAdmin admin)
        {


          var adminn = await _db.Administrator!.FirstOrDefaultAsync(x => x.KorisnickoIme == admin.KorisnickoIme /*&& x.password==password*///);

       /*    if (adminn == null )
            {
                return Ok(null);
            }

            if (!MatchPasswordHash(admin.Lozinka, adminn.Lozinka, adminn.LozinkaKljuc))
            {
                return Ok(null);
            }

            string token = CreateJWTAdmin(adminn);
            return Ok(token);
        }



        [HttpPost("loginManager")]
        public async Task<IActionResult> loginManager([FromBody] LoginDtoMenadzer manager)
        {
            var managerr = await _db.Menazder!.FirstOrDefaultAsync(x => x.KorisnickoIme == manager.KorisnickoIme /*&& x.password==password*///);

        /*    if (managerr == null)
            {
                return Ok(null);
            }

            if (!MatchPasswordHash(manager.Lozinka, managerr.Lozinka, managerr.LozinkaKljuc))
            {
                return Ok(null);
            }

            string token = CreateJWTManager(managerr);
            return Ok(token);
        }


        [HttpPost("loginMemberFan")]
        public async Task<IActionResult> loginMemmerFan([FromBody] LoginDtoNavijac memberFan)
        {
            var memmberr = await _db.Navijac!.FirstOrDefaultAsync(x => x.KorisnickoIme == memberFan.KorisnickoIme /*&& x.password==password*///);

          /*  if (memmberr == null)
            {
                return Ok(null);
            }

            if (!MatchPasswordHash(memberFan.Lozinka, memmberr.Lozinka, memmberr.LozinkaKljuc))
            {
                return Ok(null);
            }

            if(memmberr.StatusReg==null)
            {
                return Ok("nonAllowed");
            }

            string token = CreateJWTMember(memmberr);
            return Ok(token);
        }


        */
        [HttpGet("getInformation")]
        public async Task<IActionResult> getInformation()
        {

            var token = Request.Headers.Authorization[0];

            token = token.Split(' ')[1];

            var tokenHandler = new JwtSecurityTokenHandler();
            var jwtToken = tokenHandler.ReadToken(token) as JwtSecurityToken;


           

            var userId = jwtToken.Claims.FirstOrDefault(c => c.Type == "nameid")?.Value;
            var role = jwtToken.Claims.FirstOrDefault(c => c.Type == "role")?.Value;
            var username = jwtToken.Claims.FirstOrDefault(c => c.Type == "name")?.Value;

            var user = new
            {
                userId = userId,
                role= role,
                username = username,
            };
            return Ok(user);

        }



        private string CreateJWT(DtoUserJwt user)
        {

            var Skey = config.GetSection("JWTSettings:Key").Value;
            //var Skey = "moj1234567891234";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Skey));

            var claims = new Claim[]
            {
            new Claim(ClaimTypes.Name,user.KorisnickoIme),
            new Claim(ClaimTypes.NameIdentifier,user.Id.ToString()),
            new Claim(ClaimTypes.Role,user.Tip)
            };

            var signingCredentials = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256Signature);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(100),
                SigningCredentials = signingCredentials

            };
            var tokenHandlerer = new JwtSecurityTokenHandler();
            var token = tokenHandlerer.CreateToken(tokenDescriptor);



            return tokenHandlerer.WriteToken(token);

        }










       /* private string CreateJWTMember(Navijac memberFan)
        {

            var Skey = config.GetSection("JWTSettings:Key").Value;
            //var Skey = "moj1234567891234";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Skey));
            
            var claims = new Claim[]
            {
            new Claim(ClaimTypes.Name,memberFan.KorisnickoIme),
            new Claim(ClaimTypes.NameIdentifier,memberFan.Id.ToString()),
            new Claim(ClaimTypes.Role,"navijac")
            };

            var signingCredentials = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256Signature);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(100),
                SigningCredentials = signingCredentials

            };
            var tokenHandlerer = new JwtSecurityTokenHandler();
            var token = tokenHandlerer.CreateToken(tokenDescriptor);



            return tokenHandlerer.WriteToken(token);

        }

        private string CreateJWTAdmin(Administrator admin)
        {

            var Skey = config.GetSection("JWTSettings:Key").Value;
            //var Skey = "moj1234567891234";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Skey));

            var claims = new Claim[]
            {
            new Claim(ClaimTypes.Name,admin.KorisnickoIme),
            new Claim(ClaimTypes.NameIdentifier,admin.Id.ToString()),
            new Claim(ClaimTypes.Role,"admin")
            };

            var signingCredentials = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256Signature);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(100),
                SigningCredentials = signingCredentials

            };
            var tokenHandlerer = new JwtSecurityTokenHandler();
            var token = tokenHandlerer.CreateToken(tokenDescriptor);



            return tokenHandlerer.WriteToken(token);

        }



        private string CreateJWTManager(Menadzer manager)
        {

            var Skey = config.GetSection("JWTSettings:Key").Value;
            //var Skey = "moj1234567891234";
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(Skey));

            var claims = new Claim[]
            {
            new Claim(ClaimTypes.Name,manager.KorisnickoIme),
            new Claim(ClaimTypes.NameIdentifier,manager.Id.ToString()),
            new Claim(ClaimTypes.Role,"menadzer")
            };

            var signingCredentials = new SigningCredentials(
                key, SecurityAlgorithms.HmacSha256Signature);


            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.UtcNow.AddMinutes(100),
                SigningCredentials = signingCredentials

            };
            var tokenHandlerer = new JwtSecurityTokenHandler();
            var token = tokenHandlerer.CreateToken(tokenDescriptor);



            return tokenHandlerer.WriteToken(token);

        }*/


        private bool MatchPasswordHash(string passwordText, byte[] password, byte[] passwordKey)
        {

            using (var hmac = new HMACSHA512(passwordKey))
            {

                var passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(passwordText));

                for (int i = 0; i < passwordHash.Length; i++)
                {
                    if (passwordHash[i] != password[i])
                    {
                        return false;
                    }
                }

                return true;




            }
        }

    }
}
