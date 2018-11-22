using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace SocialNetwork.Helpers.Contract
{
    public interface ICrudService<T> : IAddService<T>,IGetService<T>,IUpdateService<T>,IDeleteService<T>
    {
    }
}
