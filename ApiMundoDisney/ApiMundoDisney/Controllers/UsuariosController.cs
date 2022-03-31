using ApiMundoDisney.Entities;
using ApiMundoDisney.Entities.Dtos;
using ApiMundoDisney.Repositories;
using AutoMapper;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace ApiMundoDisney.Controllers
{    
    [Authorize(AuthenticationSchemes = JwtBearerDefaults.AuthenticationScheme)]
    [Route("api/auth")]
    [ApiController]
    public class UsuariosController : ControllerBase
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;
        private readonly IConfiguration _config;

        public UsuariosController(IUsuarioRepository usuarioRepository, IMapper mapper, IUnitOfWork unitOfWork, IConfiguration config)
        {
            _usuarioRepository = usuarioRepository;
            _mapper = mapper;
            _unitOfWork = unitOfWork;
            _config = config;
        }

        [HttpGet]
        public IActionResult GetUsuarios()
        {
            var listaUsuarios = _unitOfWork.Usuarios.GetAll();

            var listaUsuariosDto = new List<UsuarioDto>();

            foreach (var usuario in listaUsuarios)
            {
                listaUsuariosDto.Add(_mapper.Map<UsuarioDto>(usuario));
            }
            return Ok(listaUsuariosDto);
        }

        [HttpGet("{usuarioId:int}", Name = "GetUsuario")]
        public IActionResult GetUsuario(int usuarioId)
        {
            var usuario = _unitOfWork.Usuarios.Get(usuarioId);

            if (usuario is null)
            {
                return NotFound();
            }

            var usuarioDto = _mapper.Map<UsuarioDto>(usuario);

            return Ok(usuarioDto);
        }

        [AllowAnonymous]
        [HttpPost("register")]
        public IActionResult Registro(UsuarioAuthDto usuarioAuthDto)
        {
            usuarioAuthDto.Usuario = usuarioAuthDto.Usuario.ToLower();

            if (_usuarioRepository.ExisteUsuario(usuarioAuthDto.Usuario))
            {
                return BadRequest("El usuario ya existe");
            }

            var usuarioACrear = new Usuario
            {
                UsuarioA = usuarioAuthDto.Usuario
            };

            var usuarioCreado = _usuarioRepository.Registro(usuarioACrear, usuarioAuthDto.Password);
            return Ok(usuarioCreado);
        }

        [AllowAnonymous]
        [HttpPost("login")]
        public IActionResult Login(UsuarioAuthLoginDto usuarioAuthLoginDto)
        {        
            var usuarioDesdeRepo = _usuarioRepository.Login(usuarioAuthLoginDto.Usuario, usuarioAuthLoginDto.Password);

            if (usuarioDesdeRepo == null)
            {
                return Unauthorized();
            }

            var claims = new[]
            {
            new Claim(ClaimTypes.NameIdentifier, usuarioDesdeRepo.UsuarioId.ToString()),
            new Claim(ClaimTypes.Name, usuarioDesdeRepo.UsuarioA.ToString())
            };

            //token
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_config.GetSection("AppSettings:Token").Value));
            var credenciales = new SigningCredentials(key, SecurityAlgorithms.HmacSha512Signature);

            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(claims),
                Expires = DateTime.Now.AddDays(1),
                SigningCredentials = credenciales
            };

            var tokenHandler = new JwtSecurityTokenHandler();
            var token = tokenHandler.CreateToken(tokenDescriptor);

            return Ok(new
            {
                token = tokenHandler.WriteToken(token)
            });
        }
    }
}


