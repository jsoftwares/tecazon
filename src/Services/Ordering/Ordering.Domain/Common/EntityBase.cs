using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Ordering.Domain.Common
{
    public abstract class EntityBase
    {
        //Domain layers should be simple and include Domain related common objects, hence this class Class provides common fields
        //for the Entities; ie for every entity in Ordering microservices we should expect the below columns as a default.
        //We made is abstract class since it contains common fields/properties that will be inherited by other classes
        //Id is of type protected set in other for us to use it in Derived classes
        public int Id { get; protected set; }
        public string CreatedBy { get; set; }
        public DateTime CreatedDate { get; set; }
        public string? LastModifiedBy { get; set; }
        public DateTime? LastModifiedDate { get; set; }

    }
}
