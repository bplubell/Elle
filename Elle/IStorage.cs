using Elle.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elle
{
    public interface IStorage
    {
        Task<IReadOnlyList<Calculator>> LoadCalculatorsAsync();
        Task SaveCalculatorsAsync(IList<Calculator> calculators);
    }
}
