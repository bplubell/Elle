using Elle.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elle.ViewModels
{
    public class IndexViewModel : ComponentBase
    {
        private bool _collapseNavMenu = true;

        protected override async Task OnInitAsync()
        {
            if (Storage != null)
            {
                try
                {
                    IReadOnlyList<Calculator> calculators = await Storage.LoadCalculatorsAsync();
                    if (calculators != null)
                    {
                        Calculators.RemoveAll(c => calculators.Any(s => s.Name == c.Name));
                        Calculators.AddRange(calculators);
                    }
                }
                catch (System.Exception e)
                {
                    System.Console.WriteLine("Something went wrong trying to load calculators from storage.");
                    System.Console.WriteLine(e);
                    // TODO Log error loading, maybe even let the user know
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
        protected IStorage? Storage { get; private set; }

        protected string? NavMenuCssClass => _collapseNavMenu ? "collapse" : null;

        protected void RemoveCalculator(Calculator calculator)
        {
            Calculators.Remove(calculator);
            if (Calculators.Count == 0)
            {
                AddCalculator();
            }
            else
            {
                ActivateCalculator(Calculators.First());
            }
        }

        protected async void SaveLocal()
        {
            if (Storage != null)
            {
                try
                {
                    await Storage.SaveCalculatorsAsync(Calculators.ToArray());
                }
                catch (System.Exception e)
                {
                    System.Console.WriteLine("Something went wrong trying to save calculators to storage.");
                    System.Console.WriteLine(e);
                    // TODO Log error loading, maybe even let the user know
                }
            }
        }

        protected void ToggleNavMenu()
        {
            _collapseNavMenu = !_collapseNavMenu;
        }
    }
}
