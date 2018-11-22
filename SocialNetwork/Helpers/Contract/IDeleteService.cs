using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Helpers.Contract
{
    public interface IDeleteService<T>
    {
        void Remove(T entity);
    }
}
