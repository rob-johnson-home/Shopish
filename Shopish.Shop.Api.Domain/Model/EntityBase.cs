using System;
using System.ComponentModel.DataAnnotations;

namespace Shopish.Shop.Api.Domain.Model
{
    public abstract class EntityBase
    {
        [Required, Key]
        public Guid ID { get; protected set; }
    }
}
