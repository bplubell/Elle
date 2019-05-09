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
            ActiveCalculator = Calculators.FirstOrDefault();

            foreach (Calculator calculator in Calculators)
            {
                calculator.Solve();
            }
        }

        protected void ActivateCalculator(Calculator calculator)
        {
            ActiveCalculator = calculator;
        }

        protected Calculator ActiveCalculator { get; set; } = new Calculator();

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

        protected string? NavMenuCssClass => _collapseNavMenu ? "collapse" : null;

        protected void ToggleNavMenu()
        {
            _collapseNavMenu = !_collapseNavMenu;
        }
    }
}
