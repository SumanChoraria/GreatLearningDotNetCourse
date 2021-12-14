using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SumanChoraria_Sprint1.Models.Repositories
{
    /// <summary>
    /// 
    /// </summary>
    /// <typeparam name="T"></typeparam>
    public class BaseRepository<T> : IBaseRepository<T> where T : BaseModel
    {
        private readonly Context _context;

        /// <summary>
        /// 
        /// </summary>
        /// <param name="context"></param>
        public BaseRepository (Context context)
        {
            _context=context;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual bool Create(T entity)
        {
            try
            {
                if (entity == null)
                    return false;

                _context.Set<T>().Add(entity);
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {

            }
            return false;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <returns></returns>
        public virtual IEnumerable<T> GetAll()
        {
            try
            {
                return _context.Set<T>().ToList();
            }
            catch (Exception)
            {

            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public virtual T GetById(int id)
        {
            try
            {
                var entity = _context.Set<T>().FirstOrDefault(i => i.Id == id);
                if (entity == null)
                    return null;

                return entity;
            }
            catch (Exception)
            {

            }
            return null;
        }

        /// <summary>
        /// 
        /// </summary>
        /// <param name="id"></param>
        /// <param name="entity"></param>
        /// <returns></returns>
        public virtual bool Update(int id, T entity)
        {
            try
            {
                if (id != entity.Id)
                    return false;

                if (!(_context.Set<T>().Any(e => e.Id == id)))
                {
                    return false;
                }
                _context.Entry(entity).State = EntityState.Modified;
                _context.SaveChanges();

                return true;
            }
            catch (Exception)
            {

            }
            return false;
        }
    }
}
