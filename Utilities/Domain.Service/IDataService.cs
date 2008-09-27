using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities
{
    public interface IDataService<T>
    {
        void Create(T type);
        
        List<T> GetAll();
        T GetById(int Id);
    }
}
