using DynamicExpresso;

namespace Elle.Models
{
    public class Calculator
    {
        private string _expression = null;
        private Interpreter _interpreter = new Interpreter()
            .EnableAssignment(AssignmentOperators.None);

        public string Expression
        {
            get => _expression;
            set
            {
                try
                {
                    object result = _interpreter.Eval(value);
                    _expression = value;
                    Result = result;
                    Error = string.Empty;
                }
                catch (System.Exception e)
                {
                    Error = e.Message;
                }
            }
        }
    
        public object Result { get; private set; } = null;

        public string Error { get; private set; } = string.Empty;
    }
}