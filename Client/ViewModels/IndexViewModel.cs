using Elle.Client.Models;
using Microsoft.AspNetCore.Components;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Elle.Client.ViewModels
{
    public class IndexViewModel : ComponentBase
    {
        private bool _collapseNavMenu = true;

        protected override async Task OnInitializedAsync()
        {
            if (Storage != null)
            {
                try
                {
                    Calculators = (await Storage.LoadCalculatorsAsync()).ToList();
                }
                catch (System.Exception e)
                {
                    System.Console.WriteLine("Something went wrong trying to load calculators from storage.");
                    System.Console.WriteLine(e);
                    // TODO Log error loading, maybe even let the user know
                }
            }


            Calculator calculatorToActivate = Calculators.FirstOrDefault();

            if (calculatorToActivate == null)
            {
                AddCalculator();
            }
            else
            {
                ActivateCalculator(calculatorToActivate);
            }

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

        protected List<Calculator> Calculators = new List<Calculator>();

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
