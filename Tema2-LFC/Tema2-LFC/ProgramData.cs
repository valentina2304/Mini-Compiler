using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

public class ProgramData
{
    public class Variable
    {
        public enum Type
        {
            Int,
            Float,
            String
        }
        public Type VariableType { get; set; }
        public dynamic? Value { get; set; }
    }

    public List<Variable> Variables { get; set; } = new List<Variable>();
}