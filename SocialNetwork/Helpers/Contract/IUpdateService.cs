using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Helpers.Contract
{
    public interface IUpdateService<T>
    {
        void Update(T entity);
    }
}
