using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Imagination.MySpring.Attribute;
using Imagination.MySpring.Base;

namespace Imagination.MySpring.Inher
{
    [SpringNormal]
    public class BmvCar:Car
    {

        public BmvCar()
        {
            Console.WriteLine("---");
            this.CarType = "宝马";
            this.CarNo = Guid.NewGuid().ToString();
        }

        public BmvCar(string carName) : this()
        {
            this.CarName = carName;
        }

        public override string ToString()
        {
            return string.Format("BmvCar{0}CarName={1},CarType={2},CarNo={3}{4}","{",CarName,CarType,CarNo,"}");
        }
    }
}
