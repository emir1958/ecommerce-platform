using Product.Domain.Common;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Product.Domain.Events
{

    public class ProductCreatedEvent : DomainEvent
    {
        public Guid ProductId { get; }

        public ProductCreatedEvent(Guid productId)
        {
            ProductId = productId;
        }
    }
}
