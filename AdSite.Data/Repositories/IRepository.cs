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
        void Add(T entity);
        T Get(Guid id);
        List<T> GetAll();
        bool Exists(T entity);
        bool Delete(Guid? id);
        T Update(T entity);
    }
}
