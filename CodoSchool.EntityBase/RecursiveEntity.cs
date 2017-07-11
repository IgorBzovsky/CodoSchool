using System;
using System.Collections.Generic;
using System.Text;

namespace CodoSchool.EntityBase
{
    public abstract class RecursiveEntity<TEntity> : Entity, IRecursiveEntity<TEntity>
    where TEntity : RecursiveEntity<TEntity>
    {
        public int? ParentId { get; set; }
        public virtual TEntity Parent { get; set; }
        public virtual ICollection<TEntity> Children { get; set; }
    }
}
