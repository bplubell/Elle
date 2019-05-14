using Blazor.Extensions.Storage;
using Elle.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elle.DataAccess
{
    public class Storage : IStorage
    {
        private readonly string _storageKey = "calculators";
        private readonly LocalStorage _localStorage;

        public Storage(LocalStorage localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<IReadOnlyList<Calculator>> LoadCalculatorsAsync()
        {
            Calculator[] calculators = await _localStorage.GetItem<Calculator[]>(_storageKey) ?? new Calculator[] { };
            return calculators.ToList();
        }

        public async Task SaveCalculatorsAsync(IList<Calculator> calculators)
        {
            await _localStorage.SetItem<Calculator[]>(_storageKey, calculators.ToArray());
        }
    }
}
