using System;
using Insurify.Population.Api.Models.Contracts;

namespace Insurify.Population.Api.Models
{
    public class EntityViewModel<T> : IEntityVewModel<T>
    {
        public T Id { get; set; }
    }
}
