using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Imagination.MySpring.Menu;

namespace Imagination.MySpring.Attribute
{
    [AttributeUsage(AttributeTargets.Class)]//仅用于类
    public abstract class BaseSpringAttribute:System.Attribute
    {
        
        public readonly MenuSpringType SpringType;

        public string Name { get; set; }

        public BaseSpringAttribute(MenuSpringType type)
        {
            SpringType = type;
        }

    }
}
