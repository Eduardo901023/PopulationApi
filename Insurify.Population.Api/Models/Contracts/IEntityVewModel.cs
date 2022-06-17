using System;
namespace Insurify.Population.Api.Models.Contracts
{
    public interface IEntityVewModel<T>
    {
        public T Id { get; set; }
    }
}
