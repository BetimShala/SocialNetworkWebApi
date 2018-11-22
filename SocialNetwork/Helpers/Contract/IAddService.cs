using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Helpers.Contract
{
    public interface IAddService<T>
    {
        void Add(T entity);
        void AddRange(IEnumerable<T> entities);
    }
}
