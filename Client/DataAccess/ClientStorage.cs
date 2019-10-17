using Blazor.Extensions.Storage;
using Elle.Client.Models;
using System;
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

        public event EventHandler? CalculatorsUpdated;

        public async Task Clear()
        {
            await _localStorage.RemoveItem(_storageKey);
            CalculatorsUpdated?.Invoke(this, null);
        }

        public async Task<int> CreateCalculatorAsync(Calculator calculator)
        {
            List<Calculator> calculators = (await LoadCalculatorsAsync()).ToList();

            int newId = calculators.Count > 0 ? calculators.Max(c => c.Id) + 1 : 1;

            calculator.Id = newId;
            calculators.Add(calculator);

            await SaveCalculatorsAsync(calculators);

            CalculatorsUpdated?.Invoke(this, null);

            return newId;
        }

        public async Task DeleteCalculator(int id)
        {
            List<Calculator> calculators = (await LoadCalculatorsAsync()).ToList();

            calculators.RemoveAll(c => c.Id == id);

            await SaveCalculatorsAsync(calculators);

            CalculatorsUpdated?.Invoke(this, null);
        }

        public async Task<Calculator?> GetCalculatorById(int id)
        {
            IReadOnlyList<Calculator> calculators = await LoadCalculatorsAsync();
            return calculators.FirstOrDefault(c => c.Id == id);
        }

        public async Task<IReadOnlyList<Calculator>> LoadCalculatorsAsync() => await _localStorage.GetItem<Calculator[]>(_storageKey) ?? _sampleCalculators;

        public async Task UpdateCalculatorAsync(Calculator calculator)
        {
            List<Calculator> calculators = (await LoadCalculatorsAsync()).ToList();

            int existingIndex = calculators.FindIndex(c => c.Id == calculator.Id);

            if (existingIndex >= 0)
            {
                calculators[existingIndex] = calculator;
            }
            else
            {
                throw new System.Exception($"Calculator with ID {calculator.Id} not found!");
            }

            await SaveCalculatorsAsync(calculators);

            CalculatorsUpdated?.Invoke(this, null);
        }

        private async Task SaveCalculatorsAsync(IList<Calculator> calculators)
        {
            await _localStorage.SetItem<Calculator[]>(_storageKey, calculators.ToArray());
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
