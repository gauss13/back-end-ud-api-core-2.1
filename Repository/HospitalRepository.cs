using Contracts;
using Entities;
using Entities.Models;
using Entities.Extenciones;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Repository
{
   public class HospitalRepository: RepositoryBase<Hospital>, IHospitalRepository
    {
        public HospitalRepository(AppDbContext dbContext):base(dbContext)
        {

        }

        public void CreateHospital(Hospital hospital)
        {
            Create(hospital);
            SaveAsync();
        }

        public void DeleteHospital(Hospital hospital)
        {
            Delete(hospital);
            SaveAsync();
        }

        public IEnumerable<Hospital> GetAllHospital()
        {
            return FindAll().OrderBy(x => x.Nombre);
        }

        public Hospital GetHospitalById(Guid Id)
        {
            return FindByCondition(x => x.Id == Id)
                .DefaultIfEmpty(new Hospital())
                .FirstOrDefault();
        }

        public void UpdateHospital(Hospital dbHospital, Hospital hospital)
        {
            dbHospital.Map(hospital);
            Update(dbHospital);
            SaveAsync();
        }
    }
}
