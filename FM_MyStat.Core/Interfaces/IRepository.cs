using Ardalis.Specification;
using FM_MyStat.Core.DTOs.UsersDTO.User;
using FM_MyStat.Core.Entities.Specifications;
using FM_MyStat.Core.Entities.Users;
using FM_MyStat.Core.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace FM_MyStat.Core.Interfaces
{
    public interface IRepository<TEntity> where TEntity : class, IEntity
    {
        Task Save();
        Task Insert(TEntity entity);
        Task<IEnumerable<TEntity>> GetAll();
        Task Delete(object id);
        Task Delete(TEntity entityToDelete);
        Task Update(TEntity ententityToUpdate);
        Task<TEntity?> GetItemBySpec(ISpecification<TEntity> specification);
        Task<IEnumerable<TEntity>> GetListBySpec(ISpecification<TEntity> specification);
        Task<TEntity?> GetByID(object id);
    }
}
