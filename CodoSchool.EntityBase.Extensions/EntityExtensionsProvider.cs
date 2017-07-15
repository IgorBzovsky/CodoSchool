using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace CodoSchool.EntityBase.Extensions
{
    //Question about the way on ordering, flattening of hierarchies !!!!!!!!!!
    public static class EntityExtensionsProvider
    {
        public static TEntity AddChild<TEntity>(this TEntity parent, TEntity child)
            where TEntity : RecursiveEntity<TEntity>
        {
            child.Parent = parent;
            if (parent.Children == null)
                parent.Children = new List<TEntity>();
            parent.Children.Add(child);
            return parent;
        }

        public static TEntity AddChildren<TEntity>(this TEntity parent, IEnumerable<TEntity> children)
        where TEntity : RecursiveEntity<TEntity>
        {
            children.ToList().ForEach(c => parent.AddChild(c));
            return parent;
        }
        
        //Requires refactoring!!!!!!!
        public static IEnumerable<TEntity> OrderHierarchyBy<TEntity, TKey>(this IEnumerable<TEntity> hierarchy, Func<TEntity, TKey> predicate)
        where TEntity : RecursiveEntity<TEntity>
        {
            IEnumerable<TEntity> sortedHierarchy = hierarchy.OrderBy(predicate).ToList();
            foreach (TEntity item in sortedHierarchy)
            {
                yield return item;
                if (item.Children != null && item.Children.Any())
                    item.Children = OrderHierarchyBy(item.Children, predicate).ToList();
            }
        }

        //Requires refactoring!!!!!!!!
        public static IEnumerable<TEntity> FlattenHierarchy<TEntity>(this IEnumerable<TEntity> hierarchy)
        where TEntity : RecursiveEntity<TEntity>
        {
            IEnumerable<TEntity> flatHierarchy = hierarchy.ToList();
            foreach (TEntity item in flatHierarchy)
            {
                yield return item;
                if (item.Children != null && item.Children.Any())
                    foreach (var child in FlattenHierarchy(item.Children))
                    {
                        yield return child;
                    }
            }
        }
    }
}
