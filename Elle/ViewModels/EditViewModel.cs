using Microsoft.AspNetCore.Components;
using Blazor.Extensions.Storage;
using System.Linq;
using System.Threading.Tasks;
using System.Collections.Generic;
using Elle.Models;

namespace Elle.ViewModels
{
    public class EditViewModel : ComponentBase
    {
        [Inject]
        protected LocalStorage? LocalStorage { get; private set; }

        private readonly string _key = "expressions";

        protected override async Task OnInitAsync()
        {
            if (LocalStorage != null)
            {
                Expressions = (await LocalStorage.GetItem<Expression[]>(_key)).ToList();
            }
        }

        protected List<Expression> Expressions { get; set; } = new List<Expression>();

        protected void AddExpression() => Expressions.Add(new Expression());

        protected void RemoveExpression(int index) => Expressions.RemoveAt(index);

        protected void Clear()
        {
            Expressions = new List<Expression>();
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
