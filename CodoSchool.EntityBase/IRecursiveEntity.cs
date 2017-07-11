using System;
using System.Collections.Generic;
using System.Text;

namespace CodoSchool.EntityBase
{
    public interface IRecursiveEntity<TEntity> where TEntity : IEntity
    {
        int? ParentId { get; set; }
        TEntity Parent { get; set; }
        ICollection<TEntity> Children { get; set; }
    }
}
