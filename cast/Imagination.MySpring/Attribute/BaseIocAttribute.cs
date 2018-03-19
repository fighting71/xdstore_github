using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Imagination.MySpring.Attribute
{
    [AttributeUsage(AttributeTargets.Property,AllowMultiple = false,Inherited = false)]//仅用于注入属性
    public class BaseIocAttribute:System.Attribute
    {

        public string IntejName { get; set; }

        public Type IntejType { get; set; }

        public bool IsSingle { get; set; }

        public BaseIocAttribute(string intejName, Type intejType, bool isSingle)
        {
            IntejName = intejName;
            IntejType = intejType;
            IsSingle = isSingle;
        }

        public BaseIocAttribute( Type intejType, bool isSingle=false):this(intejType.FullName,intejType,isSingle)
        {
        }

        public BaseIocAttribute(string intejName, bool isSingle = false,Type intejType =null) : this(intejName, intejType, isSingle)
        {
        }

    }
}
