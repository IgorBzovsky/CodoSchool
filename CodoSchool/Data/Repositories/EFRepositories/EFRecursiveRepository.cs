using CodoSchool.EntityBase;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace CodoSchool.Data.Repositories.EFRepositories
{
    public class EFRecursiveRepository<TEntity> : EFRepository<TEntity> where TEntity : RecursiveEntity<TEntity>
    {
        public EFRecursiveRepository(ApplicationDbContext context) : base(context)
        {
        }

        public override void Remove(TEntity entity)
        {
            var children = Context.Set<TEntity>().Where(x => x.ParentId == entity.Id);
            if (children != null && children.Any())
            {
                foreach (var child in children)
                {
                    Remove(child);
                    Context.Set<TEntity>().Remove(child);
                }
            }
            Context.Set<TEntity>().Remove(entity);
        }

        public override void RemoveRange(IEnumerable<TEntity> entities)
        {
            throw new NotImplementedException();
        }
    }
}
