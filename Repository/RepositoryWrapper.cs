using Contracts;
using Entities;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository
{
    public class RepositoryWrapper : IRepositoryWrapper
    {
        private AppDbContext _context;
        private IUsuarioRepository _usuario;

        public IUsuarioRepository Usuario
        {
            get
            {
                if(_usuario == null)
                {
                    _usuario = new UsuarioRepository(_context);
                }

                return _usuario;
            }
        }

        public RepositoryWrapper(AppDbContext repositorioContext)
        {
            _context = repositorioContext;
        }

    }
}
