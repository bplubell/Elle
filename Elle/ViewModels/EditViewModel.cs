using Microsoft.AspNetCore.Components;
using Blazor.Extensions.Storage;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Elle.Models;
using DynamicExpresso;

namespace Elle.ViewModels
{
    public class EditViewModel : ComponentBase
    {
        private readonly string _key = "expressions";

        [Inject]
        protected LocalStorage? LocalStorage { get; private set; }

        protected override async Task OnInitAsync()
        {
            if (LocalStorage != null)
            {
                Expression[] expressions = await LocalStorage.GetItem<Expression[]>(_key);
                if (expressions != null)
                {
                    Expressions = expressions.ToList();
                }
            }
        }

        protected List<Expression> Expressions { get; set; } = new List<Expression>();

        protected void AddExpression() => Expressions.Add(new Expression());

        protected void RemoveExpression(int index)
        {
            Expressions.RemoveAt(index);
        }

        protected void Clear()
        {
            Expressions = new List<Expression>();
        }

        protected void Solve()
        {
            Interpreter interpreter = new Interpreter()
                .EnableAssignment(AssignmentOperators.None);

            foreach (Expression expression in Expressions)
            {
                // Dependent on the order of the expressions; dependent expressions must come later
                object result = interpreter.Eval(expression.Value);
                expression.Result = result switch
                {
                    double doubleResult => doubleResult,
                    int intResult => System.Convert.ToDouble(intResult),
                    _ => 0
                };
                interpreter.SetVariable(expression.Name, expression.Result);
            }
        }

        protected bool ExpressionNameIsValid(string name)
        {
            return (!string.IsNullOrWhiteSpace(name)
                && Expressions.Count(ex => ex.Name == name) == 1);
        }

        protected async void SaveLocal()
        {
            if (LocalStorage != null)
            {
                await LocalStorage.SetItem<Expression[]>(_key, Expressions.ToArray());
            }
        }

    }
}
