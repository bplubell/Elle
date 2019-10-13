using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Collections.Generic;
using Elle.Client.Models;
using DynamicExpresso;

namespace Elle.Client.ViewModels
{
    public class CalculatorViewModel : ComponentBase
    {
        [Parameter]
        public Calculator Calculator { private get; set; } = new Calculator();

        public CalculatorViewModel()
        {
        }

        public CalculatorViewModel(Calculator calculator)
        {
            Calculator = calculator;
        }

        protected List<Expression> Expressions
        {
            get => Calculator.Expressions;
            set => Calculator.Expressions = value;
        }

        protected void AddExpression() => Calculator.AddExpression();

        protected string? Name
        {
            get => Calculator.Name;
            set => Calculator.Name = value;
        }

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
