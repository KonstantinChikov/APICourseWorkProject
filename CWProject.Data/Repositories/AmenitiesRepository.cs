using CWProject.Data.Repositories.Interfaces;
using CWProject.Models.DtoModels.AmenitiesDto;
using CWProject.Models.Models;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CWProject.Data.Repositories
{
    public class AmenitiesRepository : IAmenitiesRepository
    {
        private readonly AppDbContext _appDbContext;

        public List<AmenitiesModel> GetAll => _appDbContext.Amenities
                .Include(x => x.VillaAmenities)
                .ThenInclude(x => x.Villas)
                .Select(x => new AmenitiesModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Villas = x.VillaAmenities.Select(z => z.Villas.Name).ToList(),
                }).ToList();

        public AmenitiesModel GetById(int id) => _appDbContext.Amenities
        .Include(x => x.VillaAmenities)
                .ThenInclude(x => x.Villas)
                .Select(x => new AmenitiesModel()
                {
                    Id = x.Id,
                    Name = x.Name,
                    Villas = x.VillaAmenities.Select(z => z.Villas.Name).ToList()
                }).Where(x => x.Id == id).SingleOrDefault();

        public void Save() => _appDbContext.SaveChanges();
        public DbSet<Amenities> Amenities => _appDbContext.Amenities;
    }
}
