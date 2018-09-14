using Entities.Models;
using System;
using System.Collections.Generic;
using System.Text;

namespace Contracts
{
  public interface IHospitalRepository
    {


        IEnumerable<Hospital> GetAllHospital();
        Hospital GetHospitalById(Guid Id);
        //OwnerExtended GetOwnerWithDetails(Guid ownerId);
        void CreateHospital(Hospital hospital);
        void UpdateHospital(Hospital dbHospital, Hospital hospital);
        void DeleteHospital(Hospital hospital);

        

    }
}
