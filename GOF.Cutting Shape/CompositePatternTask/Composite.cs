using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CompositePatternTask
{
    public abstract class Composite
    {
        protected readonly List<Composite> _components;
        protected readonly StringBuilder _stringBuilder;

        public Composite()
        {
            _components = new List<Composite>(0);
            _stringBuilder = new StringBuilder();
        }

        public virtual string ConvertToString()
        {
            return _stringBuilder.ToString();
        }
    }
}
