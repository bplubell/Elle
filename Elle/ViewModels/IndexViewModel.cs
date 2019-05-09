using Blazor.Extensions.Storage;
using Elle.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elle.ViewModels
{
    public class IndexViewModel : ComponentBase
    {
        private readonly string _storageKey = "calculators";
        private bool _collapseNavMenu = true;

        protected override async Task OnInitAsync()
        {
            if (LocalStorage != null)
            {
                Calculator[] calculators = await LocalStorage.GetItem<Calculator[]>(_storageKey);
                if (calculators != null)
                {
                    Calculators.RemoveAll(c => calculators.Any(s => s.Name == c.Name));
                    Calculators.AddRange(calculators);
                }
            }

            ActivateCalculator(Calculators.FirstOrDefault());
        }

        protected void ActivateCalculator(Calculator calculator)
        {
            ActiveCalculator = calculator;
            ActiveCalculator.Solve();
        }

        protected Calculator ActiveCalculator { get; set; } = new Calculator();

        protected void AddCalculator()
        {
            Calculator newCalculator = new Calculator() { Name = "New calculator" };
            Calculators.Add(newCalculator);
            ActivateCalculator(newCalculator);
        }

        protected List<Calculator> Calculators = new List<Calculator>() {
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
                    new Expression() { Name = "PI", Value = "3.141592654" },
                    new Expression() { Name = "diameter", Value = "3" },
                    new Expression() { Name = "radius", Value = "diameter / 2" },
                    new Expression() { Name = "perimeter", Value = "diameter * PI" },
                    new Expression() { Name = "area", Value = "PI * Math.Pow(radius, 2)" },
                }
            },
        };

        [Inject]
        protected LocalStorage? LocalStorage { get; private set; }

        protected string? NavMenuCssClass => _collapseNavMenu ? "collapse" : null;

        protected async void SaveLocal()
        {
            if (LocalStorage != null)
            {
                await LocalStorage.SetItem<Calculator[]>(_storageKey, Calculators.ToArray());
            }
        }

        protected void ToggleNavMenu()
        {
            _collapseNavMenu = !_collapseNavMenu;
        }
    }
}
