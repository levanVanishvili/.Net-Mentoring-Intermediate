using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositePatternTask
{
    public class Form : Composite
    {
        private readonly string _name;

        public Form(string name)
        {
            _name = name;
        }

        public void AddComponent(Composite component)
        {
            _components.Add(component);
        }

        public override string ConvertToString()
        {
            _stringBuilder.Append($"<form name='{_name}'>");
            _stringBuilder.Append("\n\r");
            foreach (var component in _components)
            {
                _stringBuilder.Append(component.ConvertToString());
                _stringBuilder.Append("\n\r");
            }

            _stringBuilder.Append($"</form>");
            return _stringBuilder.ToString();
        }
    }
}
