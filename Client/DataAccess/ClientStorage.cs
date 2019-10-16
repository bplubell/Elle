using Blazor.Extensions.Storage;
using Elle.Client.Models;
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

        public async Task DeleteCalculator(string id)
        {
            List<Calculator> calculators = (await LoadCalculatorsAsync()).ToList();

            calculators.RemoveAll(c => c.Name == id);

            await SaveCalculatorsAsync(calculators);
        }

        public async Task<IReadOnlyList<Calculator>> LoadCalculatorsAsync() => await _localStorage.GetItem<Calculator[]>(_storageKey) ?? _sampleCalculators;

        public async Task SaveCalculatorAsync(Calculator calculator)
        {
            List<Calculator> calculators = (await LoadCalculatorsAsync()).ToList();
            
            int existingIndex = calculators.FindIndex(c => c.Name == calculator.Name);

            if (existingIndex >= 0)
            {
                calculators[existingIndex] = calculator;
            }
            else
            {
                calculators.Add(calculator);
            }

            await SaveCalculatorsAsync(calculators);
        }

        public async Task SaveCalculatorsAsync(IList<Calculator> calculators)
        {
            await _localStorage.SetItem<Calculator[]>(_storageKey, calculators.ToArray());
        }

        public async Task<Calculator?> GetCalculatorById(int id)
        {
            IReadOnlyList<Calculator> calculators = await LoadCalculatorsAsync();
            return calculators.FirstOrDefault(c => c.Id == id);
        }

        private readonly Calculator[] _sampleCalculators = new Calculator[] {
            new Calculator() {
                Id = 1,
                Name = "Simple",
                Expressions = new List<Expression>() {
                    new Expression() { Name = "a", Value = "3" },
                    new Expression() { Name = "b", Value = "4" },
                    new Expression() { Name = "c", Value = "2 * a + b" },
                }
            },
            new Calculator() {
                Id = 2,
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
