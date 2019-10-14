using Blazor.Extensions.Storage;
using Elle.Client.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elle.Client.DataAccess
{
    public class ClientStorage : IStorage
    {
        private readonly string _storageKey = "calculators";
        private readonly LocalStorage _localStorage;

        public ClientStorage(LocalStorage localStorage)
        {
            _localStorage = localStorage;
        }

        public async Task<IReadOnlyList<Calculator>> LoadCalculatorsAsync()
        {
            Calculator[] calculators = await _localStorage.GetItem<Calculator[]>(_storageKey) ?? new Calculator[] { };
            return calculators.Concat(_sampleCalculators).ToList();
        }

        public async Task SaveCalculatorsAsync(IList<Calculator> calculators)
        {
            await _localStorage.SetItem<Calculator[]>(_storageKey, calculators.ToArray());
        }

        private readonly List<Calculator> _sampleCalculators = new List<Calculator>() {
            new Calculator() {
                Name = "Simple",
                Expressions = new List<Expression>() {
                    new Expression() { Name = "a", Value = "3" },
                    new Expression() { Name = "b", Value = "4" },
                    new Expression() { Name = "c", Value = "2 * a + b" },
                }
            },
            new Calculator() {
                Name = "Circle info",
                Expressions = new List<Expression>() {
                    new Expression() { Name = "diameter", Value = "3" },
                    new Expression() { Name = "radius", Value = "diameter / 2" },
                    new Expression() { Name = "perimeter", Value = "diameter * Math.PI" },
                    new Expression() { Name = "area", Value = "Math.PI * Math.Pow(radius, 2)" },
                }
            },
        };
    }
}
