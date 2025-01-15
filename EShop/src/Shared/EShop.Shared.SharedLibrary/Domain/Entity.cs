using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace EShop.Shared.SharedLibrary.Domain
{
    public abstract class Entity<T> where T : IEquatable<T>
    {
        public T Id { get; protected set; }
        public DateTime CreatedDate { get; protected set; }
        public DateTime? LastModifiedDate { get; protected set; }

        protected Entity()
        {
            CreatedDate = DateTime.UtcNow;
            Id = typeof(T) == typeof(Guid) ? (T)(object)Guid.NewGuid() : default!;


        }
        // override object.Equals
        public override bool Equals(object obj)
        {
           
            if (obj == null || GetType() != obj.GetType())
            {
                return false;
            }

            if (Object.ReferenceEquals(this,obj))
            {
                return true;
            }

            Entity<T> item = (Entity<T>)obj;
            return item.Id.Equals(Id);        
        }

        // override object.GetHashCode
        public override int GetHashCode()
        {
            // TODO: write your implementation of GetHashCode() here
            return Id.GetHashCode();
        }

        public static bool operator ==(Entity<T> left, Entity<T> right) {
            if (left is null && right is null)
            {
                return true;
            }

            if (left is null || right is null)
            {
                return false;
            }

            return left.Equals(right);
        }

        public static bool operator !=(Entity<T> left, Entity<T> right) => !(left == right);


    }
}
