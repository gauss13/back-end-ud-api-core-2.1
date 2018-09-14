using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using Entities.Models;

namespace Contracts
{
    public interface IUsuarioRepository
    {
        Task<IEnumerable<Usuario>> GetAllUsuarioAsync();
        Task<Usuario> GetUsuarioByIdAsync(string ownerId);
        //OwnerExtended GetOwnerWithDetails(Guid ownerId);
        Task<Usuario> CreateUsuarioAsync(Usuario owner);
        Task<Usuario> UpdateUsuarioAsync(Usuario dbOwner, Usuario owner);
        Task DeleteUsuarioAsync(Usuario owner);
        Task UpdateImgAsync(string id, string strImg);
        Task SaveChangesAsync();

    }
}
