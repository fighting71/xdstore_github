using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Imagination.MySpring.Attribute;
using Imagination.MySpring.Base;
using Imagination.MySpring.Inher;

namespace Imagination.MySpring.Normal
{

    [SpringNormal]
    public class People
    {


        [BaseIoc(typeof(BmvCar))]
        public Car CarInfo { get; set; }

        [BaseIoc(typeof(BmvCar),true)]
        public Car SingleCar { get; set; }

    }
}
