using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;
using Contracts;
using Entities;
using System.Linq;
using Entities.Extenciones;

namespace Repository
{
  public  class MedicoRepository:RepositoryBase<Medico>, IMedicoRepository
    {

        public MedicoRepository(AppDbContext dbContext):base(dbContext)
        {

        }

        public void CreateMedico(Medico medico)
        {
            Create(medico);
            Save();
        }

        public void DeleteMedico(Medico medico)
        {
            Delete(medico);
            Save();
        }

        public IEnumerable<Medico> GetAllMedico()
        {
            return FindAll().OrderBy(x => x.Nombre);
        }

        public Medico GetMedicoById(Guid Id)
        {
            return FindByCondition(x => x.Id == Id)
               .DefaultIfEmpty(new Medico())
               .FirstOrDefault();
        }

        public void UpdateMedico(Medico dbMedico, Medico medico)
        {
            dbMedico.Map(medico);
            Update(dbMedico);
            Save();
        }
    }
}
