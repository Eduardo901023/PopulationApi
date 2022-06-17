using System;
namespace Insurify.Population.Business.Contracts
{
    public interface IEntity<T>
    {
        public T Id { get; set; }
    }
}
