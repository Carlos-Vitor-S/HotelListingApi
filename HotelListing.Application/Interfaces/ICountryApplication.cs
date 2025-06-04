using HotelListing.Domain.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HotelListing.Application.Interfaces
{
    public interface ICountryApplication
    {
        Task<Country> GetById(int id);
        Task<IEnumerable<Country>> GetAll();
        Task Create(Country country);
        Task Update(Country country);
        Task Delete(int id);
    }
}
