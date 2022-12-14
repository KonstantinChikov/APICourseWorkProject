using CWProject.Models.DtoModels.AmenitiesDto;
using CWProject.Models.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWProject.Services.Interfaces
{
    public interface IAmenitiesService
    {
        
        List<AmenitiesModel> GetAll();
        AmenitiesModel GetById(int id);
        Amenities Create(Amenities amenities);
        void Update(Amenities user);
        void Delete(int id); 
        
    }
}
