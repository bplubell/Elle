using Microsoft.AspNetCore.Components;
using System.Linq;
using System.Collections.Generic;
using Elle.Client.Models;
using DynamicExpresso;
using System.Threading.Tasks;
using System;

namespace Elle.Client.ViewModels
{
    public class CalculatorViewModel : ComponentBase
    {
        [Parameter]
        public Calculator? Calculator { get; set; }
        
        [Parameter]
        public Func<Task>? Delete { get; set; }

        [Parameter]
        public Func<Task>? Save { get; set; }
        
        protected List<Expression> Expressions
        {
            get => Calculator?.Expressions ?? new List<Expression>();
            set
            {
                if (Calculator != null)
                    Calculator.Expressions = value;
            }
        }

        public string Name
        {
            get => Calculator?.Name ?? string.Empty;
            set
            {
                if (Calculator != null)
                    Calculator.Name = value;
            }
        }

        protected void AddExpression() => Calculator?.AddExpression();

        protected void Clear() => Calculator?.Clear();

        protected bool ExpressionNameIsValid(string name)
        {
            return (!string.IsNullOrWhiteSpace(name)
                && Expressions.Count(ex => ex.Name == name) == 1);
        }
    
        protected void OnDelete() => Delete?.Invoke();

        protected void OnSave() => Save?.Invoke();

        protected void RemoveExpression(int index) => Calculator?.RemoveExpression(index);

        protected void Solve() => Calculator?.Solve();

    }
}
