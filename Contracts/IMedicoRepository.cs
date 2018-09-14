using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
 public  interface IMedicoRepository
    {
        IEnumerable<Medico> GetAllMedico();
        Medico GetMedicoById(Guid Id);
        //OwnerExtended GetOwnerWithDetails(Guid ownerId);
        void CreateMedico(Medico medico);
        void UpdateMedico(Medico dbMedico, Medico medico);
        void DeleteMedico(Medico medico);

    }
}
