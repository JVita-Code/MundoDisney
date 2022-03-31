using ApiMundoDisney.Repositories;
using AutoMapper;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ApiMundoDisney.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IMapper _mapper;
        private readonly IUnitOfWork _unitOfWork;

        public UsuarioService(IMapper mapper, IUnitOfWork unitOfWork)
        {
            _mapper = mapper;
            _unitOfWork = unitOfWork;
        }

        public bool ExisteUsuario(int usuarioId)
        {
            throw new NotImplementedException();
        }
    }
}
