using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositePatternTask
{
    public class LabelText : Composite
    {
        private readonly string _value;

        public LabelText(string value)
        {
            _value = value;
        }

        public override string ConvertToString()
        {
            return $"<label value='{_value}'/>";
        }
    }
}
