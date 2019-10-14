using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Collections.Generic;
using Elle.Client.Models;
using DynamicExpresso;
using System.Threading.Tasks;

namespace Elle.Client.ViewModels
{
    public class CalculatorViewModel : ComponentBase
    {
        protected Calculator Calculator { get; set; } = new Calculator();
        
        [Inject]
        protected IStorage? Storage { get; private set; }

        protected override async Task OnParametersSetAsync()
        {
            if (Storage != null && Id != null)
            {
                Calculator? calculator = await Storage.GetCalculatorById(Id);
                if (calculator != null)
                {
                    Calculator = calculator;
                }
                else
                {
                    Calculator = new Calculator() { Name = Id };
                }
            }
            else
            {
                // TODO Show error
            }
        }

        protected async Task Save()
        {
            if (Storage != null)
            {
                await Storage.SaveCalculatorAsync(Calculator);
            }
        }

        protected List<Expression> Expressions
        {
            get => Calculator.Expressions;
            set => Calculator.Expressions = value;
        }

        protected void AddExpression() => Calculator.AddExpression();

        protected string Name
        {
            get => Calculator.Name;
            set => Calculator.Name = value;
        }

        [Parameter]
        public string? Id { private get; set; }

        protected void RemoveExpression(int index) => Calculator.RemoveExpression(index);

        protected void Clear() => Calculator.Clear();

        protected void Solve() => Calculator.Solve();

        protected bool ExpressionNameIsValid(string name)
        {
            return (!string.IsNullOrWhiteSpace(name)
                && Expressions.Count(ex => ex.Name == name) == 1);
        }
    }
}
