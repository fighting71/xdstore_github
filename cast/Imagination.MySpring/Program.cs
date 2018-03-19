using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Imagination.MySpring.Attribute;
using Imagination.MySpring.Base;
using Imagination.MySpring.Factory;
using Imagination.MySpring.Inher;
using Imagination.MySpring.Menu;
using Imagination.MySpring.Normal;

namespace Imagination.MySpring
{
    class Program
    {
        static void Main(string[] args)
        {

            // do a project to imitate
            var context = SpringFactory.GetInstance();

//            var data = context.GetObject("Temp");
//            if (data is Car)
//            {
//                Console.WriteLine(data);
//            }

//            var data = context.GetObject(typeof(People));
//            if (data is People)
//            {
//                Console.WriteLine((data as People).CarInfo);//大工告成~~~
//            }

            var data = context.GetObject<People>();

            Console.WriteLine("获取的第一个对象：");

            Console.WriteLine(data.CarInfo);
            Console.WriteLine(data.SingleCar);
            data.CarInfo.CarName = "CarInfo1";
            data.SingleCar.CarName = "SingleCarInfo1";

            data = context.GetObject<People>();
            Console.WriteLine("获取的第二个对象：");

            Console.WriteLine(data.CarInfo);
            Console.WriteLine(data.SingleCar);

            //test>>success

            Console.ReadKey();

        }
    }
}
