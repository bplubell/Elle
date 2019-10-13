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


    }
}
