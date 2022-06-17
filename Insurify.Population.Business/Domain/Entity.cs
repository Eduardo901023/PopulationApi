using System;
using Insurify.Population.Business.Contracts;

namespace Insurify.Population.Business.Domain
{
    public class Entity : IEntity<Guid>
    {
        public Entity()
        {
            Id = Guid.NewGuid();
        }

        public Guid Id { get; set; }
    }
}
