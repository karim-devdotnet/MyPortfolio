using System;

namespace Core.Entities
{
    public class EntityBase
    {

        public EntityBase()
        {
            Id = Guid.NewGuid();
        }
        public Guid Id { get; set; }
    }
}
