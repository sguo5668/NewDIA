using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using DIA.Entities;

namespace DIA.Services
{
    public interface IRepository<T> where T : BaseEntity
    {
        IEnumerable<T> GetAll();
        T Get(long id);
        int Insert(T entity);        
        void Update(T entity);
        void Delete(T entity);
		void Add(T entity);  
    }
}
