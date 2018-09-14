using Contracts;
using Entities;
using Entities.Extenciones;
using Entities.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Repository
{
    public class UsuarioRepository : RepositoryBase<Usuario>, IUsuarioRepository
    {

        public UsuarioRepository(AppDbContext contextRepository) : base(contextRepository)
        {

        }

        public async Task<Usuario> CreateUsuarioAsync(Usuario owner)
        {
            Create(owner);
            await SaveAsync();

            return owner;
        }

        public async Task DeleteUsuarioAsync(Usuario owner)
        {
            Delete(owner);
            await SaveAsync();
        }

        public async Task<IEnumerable<Usuario>> GetAllUsuarioAsync()
        {
            var lista = await FindAllAsync();
            return lista.OrderBy(x => x.UserName);
        }

        public async Task<Usuario> GetUsuarioByIdAsync(string ownerId)
        {
            var item = await FindByConditionAsync(x => x.Id == ownerId);

            return item.DefaultIfEmpty(new Usuario())
                .FirstOrDefault();
        }

        public async Task<Usuario> UpdateUsuarioAsync(Usuario dbOwner, Usuario nuevo)
        {
            dbOwner.Map(nuevo);
            Update(dbOwner);

            await SaveAsync();

            return dbOwner;
        }

        public async Task UpdateImgAsync(string id, string strImg)
        {
            var user = await GetUsuarioByIdAsync(id);
            user.Img = strImg;
            Update(user);
            await SaveAsync();

        }

        public async Task  SaveChangesAsync()
        {
            await SaveAsync();
        }
    }
}
