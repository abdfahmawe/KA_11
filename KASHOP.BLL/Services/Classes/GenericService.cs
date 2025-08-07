using KASHOP.BLL.Services.Interfaces;
using KASHOP.DAL.DTO.Responses;
using KASHOP.DAL.Models;
using KASHOP.DAL.Repositories;
using KASHOP.DAL.Repositories.Classes;
using KASHOP.DAL.Repositories.Interfaces;
using Mapster;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace KASHOP.BLL.Services.Classes
{
    public class GenericService<TRequist, TResponse, TEntity> : IGenericServiece<TRequist, TResponse, TEntity>
    where TEntity : BaseModel
    {
        private readonly IGenericRepositry<TEntity> _repositry;
        public GenericService(IGenericRepositry<TEntity> GenericRepositry)
        {
            _repositry = GenericRepositry;
        }
        public int Create(TRequist requist)
        {
            var entity = requist.Adapt<TEntity>();
            return _repositry.Add(entity);
        }

        public int Delete(int id)
        {
            var entity = _repositry.GetById(id);
            if (entity is null) return 0;
            return _repositry.Delete(entity);
        }

        public IEnumerable<TResponse> GetAll(bool onlyActive = false)
        {
            var entities = _repositry.GetAll(); 
            if(onlyActive)
            {
                entities = entities.Where(e => e.status == Status.Active);
            }
            return entities.Adapt<IEnumerable<TResponse>>(); 
        }

        public TResponse? GetById(int id)
        {
            var entity = _repositry.GetById(id);
            return entity is null ? default : entity.Adapt<TResponse>();
        }

        public bool ToogleStatus(int id)
        {
            var entity = _repositry.GetById(id);
            if (entity is null) return false;
            entity.status = entity.status == Status.Active ? Status.Inactive : Status.Active;
            _repositry.Update(entity);
            return true;
        }

        public int Update(TRequist requist, int id)
        {
            var entity = _repositry.GetById(id);


            if (entity is null) return 0;
            var updateEntity = requist.Adapt<TEntity>();
            return _repositry.Update(updateEntity);
        }
    }
}
