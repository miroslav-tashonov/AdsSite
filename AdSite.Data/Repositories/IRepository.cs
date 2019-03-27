using AdSite.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;

namespace AdSite.Data.Repositories
{
    public interface IRepository<T> where T : RepositoryEntity
    {
        bool Add(T entity);
        T Get(Guid id);
        List<T> GetAll(Guid countryId);
        bool Exists(Guid id);
        bool Delete(Guid id);
        bool Update(T entity);
    }

    public interface ICountryRepository<T> where T: RepositoryEntity
    {
        bool Add(T entity);
        T Get(Guid id);
        bool Exists(Guid id);
        bool Delete(Guid id);
        bool Update(T entity);
    }
}
