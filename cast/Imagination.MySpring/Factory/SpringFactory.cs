using System;
using System.Collections.Generic;
using System.Reflection;
using Imagination.MySpring.Attribute;
using Imagination.MySpring.Exception;
using Imagination.MySpring.Menu;

namespace Imagination.MySpring.Factory
{

    /// <summary>
    /// <author>yj</author>
    /// <since>2018-3-15 14:20</since>
    /// <description>test--success</description>
    /// </summary>
    public class SpringFactory
    {

        #region cache

        protected static Dictionary<string, object> singleMap = new Dictionary<string, object>();
        protected static Dictionary<string, Type> normalMap = new Dictionary<string, Type>();
        protected static Dictionary<string, Type> serviceMap = new Dictionary<string, Type>();
        protected static Dictionary<string, Type> controllerMap = new Dictionary<string, Type>();

        #endregion

        #region can_extend

        /// <summary>
        /// 初始化
        /// </summary>
        protected void Init()
        {
            //1.获取本项目中的所有类
            var types = Assembly.GetExecutingAssembly().GetTypes();
            foreach (var type in types)
            {
                if (type.IsClass && type.IsPublic && !type.IsAbstract && !type.IsSubclassOf(typeof(System.Attribute)))//基础筛选所有类
                {
                    var attribute = type.GetCustomAttribute<BaseSpringAttribute>();//获取被标注的类
                    if (attribute != null)
                    {
                        if (string.IsNullOrWhiteSpace(attribute.Name))
                        {
                            attribute.Name = type.FullName;
                        }
                        switch (attribute.SpringType)
                        {
                            case MenuSpringType.TYPD_SERVICE:
                                serviceMap.Add(attribute.Name, type);
                                break;
                            case MenuSpringType.TYPE_CONTROLLER:
                                controllerMap.Add(attribute.Name, type);
                                break;
                            case MenuSpringType.TYPE_NORMAL:
                                normalMap.Add(attribute.Name, type);
                                break;
                            case MenuSpringType.TYPE_SINGLE:

                                var constructor = type.GetConstructor(new Type[0]);//获取类的无参构造
                                if (constructor != null)
                                {
                                    var info = constructor.Invoke(null);
                                    info = IocInfo(info);
                                    singleMap.Add(attribute.Name, info);
                                }
                                break;
                        }

                    }
                }
            }
        }

        /// <summary>
        /// 注入属性
        /// </summary>
        /// <param name="info"></param>
        /// <returns></returns>
        protected object IocInfo(object info)
        {
            var type = info.GetType();
            foreach (var propertyInfo in type.GetProperties())
            {
                var attribute = propertyInfo.GetCustomAttribute<BaseIocAttribute>();

                if (attribute != null)
                {
                    propertyInfo.SetValue(info, GetObject(attribute));
                }

            }

            return info;
        }

        /// <summary>
        /// 通过特性获取对象
        /// </summary>
        /// <param name="attribute"></param>
        /// <returns></returns>
        protected object GetObject(BaseIocAttribute attribute)
        {

            object result = null;

            if (string.IsNullOrWhiteSpace(attribute.IntejName))
            {
                attribute.IntejName = attribute.IntejType.FullName;
            }


            if (attribute.IsSingle)
            {
                if (!singleMap.ContainsKey(attribute.IntejName))//单例map中不存在此对象
                {
                    result = GetObject(new BaseIocAttribute(intejName: attribute.IntejName)); //在未单例中查找
                    singleMap.Add(attribute.IntejName, result); //将此对象添加到单例map中  提高复用性
                                                                //                    throw new SpringException(string.Format("未查找到与{0}向匹配的对象", attribute.IntejName));
                }
                else
                {
                    result = singleMap[attribute.IntejName];
                }
            }
            else
            {

                if (!(normalMap.ContainsKey(attribute.IntejName) || serviceMap.ContainsKey(attribute.IntejName) || controllerMap.ContainsKey(attribute.IntejName)))
                {
                    throw new SpringException(string.Format("未查找到与{0}向匹配的对象", attribute.IntejName));
                }

                var type = normalMap[attribute.IntejName];
                if (type == null) type = serviceMap[attribute.IntejName];
                if (type == null) type = controllerMap[attribute.IntejName];
                if (type == null)
                {
                    throw new SpringException(string.Format("未查找到与{0}向匹配的对象", attribute.IntejName));
                }
                result = type.GetConstructor(new Type[0])?.Invoke(null);
                if (result == null)
                {
                    throw new SpringException(string.Format("所要获取的对象{0}未包含无参构造", attribute.IntejType.FullName));
                }
                result = IocInfo(result);
            }

            if (attribute.IntejType != null &&
                         (result.GetType() != attribute.IntejType || result.GetType().IsSubclassOf(attribute.IntejType))
                //及非同类型也无继承关系
                )
            {
                throw new SpringException(string.Format("获取的{0}对象与所想要获取的{1}对象不匹配", result.GetType().FullName, attribute.IntejType.FullName));
            }


            return result;

        }

        #endregion

        #region offer_server

        public object GetObject(string flagName, bool isSigle = false)
        {
            var result = default(object);

            result = GetObject(new BaseIocAttribute(flagName));

            return result;
        }

        public object GetObject(Type type, bool isSigle = false)
        {
            return GetObject(type.FullName, isSigle);
        }

        public T GetObject<T>(bool isSigle = false) where T : class, new()
        {
            return GetObject(typeof(T), isSigle) as T;
        }

        #endregion

        #region single_model
        protected SpringFactory()
        {
            Init();
        }

        private static SpringFactory instance = new SpringFactory();

        public static SpringFactory GetInstance()
        {
            return instance;
        }

        #endregion

    }
}
