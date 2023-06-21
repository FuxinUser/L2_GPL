using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

/**
 * Author: ICSC余士鵬 (Modfiy From K51 張恩碩)
 * Date: 2019/10/04
 * Description: 反射Utilty 
 * Reference: 
 * Modified: 
 */
namespace Core.Util
{
    public class ReflectionUtil
    {
     


        public enum Modifier
        {
            PUBLIC,             // 未限制存取。
            PROTECTED,          // 存取限於包含類別或衍生自包含類別的類型。
            INTERNAL,           // 存取限於目前組件。
            PRIVATE,            // 存取限於包含類型。
            PUBLIC_STATIC,      // 未限制存取的靜態。
            PRIVATE_STATIC,     // 存取限於包含類型的靜態。
        }

        /// <summary>
        /// 創造實體物件
        /// </summary>
        /// <param name="assemblyName">組件名稱，相當於組件名稱</param>
        /// <param name="className">類別名稱</param>
        /// <param name="paramArray">建構子初始化所需的參數</param>
        /// <returns>回傳實體，型別為object</returns>
        public static object CreateInstance(string assemblyName, string className, params object[] paramArray)
        {
            try
            {
                var type = GetAssemblyType(assemblyName, className);
                return Activator.CreateInstance(type, paramArray);
            }
            catch (Exception e)
            { Console.WriteLine(e.Message); }

            return null;
        }

        /// <summary>
        /// 取得組件名稱內的組件類型(AssemblyType)
        /// </summary>
        /// <param name="assemblyName">組件名稱，相當於"專案名稱"</param>
        /// <param name="className">類別名稱</param>
        /// <returns>組件類型(AssemblyType)</returns>
        public static Type GetAssemblyType(string assemblyName, string className)
        {
            foreach (Type a in Assembly.Load(assemblyName).GetTypes())
            {
                if (className.CompareTo(a.Name) == 0)
                {
                    return a;
                }
            }

            return null;
        }

        public static object CallFuction(object instance, string function, BindingFlags modifier, params object[] paramArray)
        {
            var methodInfo = GetMethodInfo(instance.GetType(), function, modifier);
            return methodInfo.Invoke(instance, paramArray);
        }

        public static MethodInfo GetMethodInfo(Type type, string function, BindingFlags modifier)
        {

            return (modifier == BindingFlags.Default) ? type.GetMethod(function) : type.GetMethod(function);
        }


        #region --- 取得或設定屬性(Set or Get Property Value) ---

        public static T GetProperty<T>(object instance, string propertyName, Modifier modifier, object[] index = null)
        {
            var propertyInfo = GetPropertyInfo(instance.GetType(), propertyName, modifier);
            return (T)propertyInfo.GetValue(instance, index);
        }

        public static void SetProperty(object instance, string propertyName, Modifier modifier, object obj, object[] index = null)
        {
            var propertyInfo = GetPropertyInfo(instance.GetType(), propertyName, modifier);
            propertyInfo.SetValue(instance, obj, index);
        }

        public static PropertyInfo GetPropertyInfo(Type type, string propertyName, Modifier modifier)
        {
            var bindingFlags = GetBindingFlags(modifier);
            return (bindingFlags == BindingFlags.Default) ? type.GetProperty(propertyName) : type.GetProperty(propertyName, bindingFlags);
        }

        #endregion

        public static BindingFlags GetBindingFlags(Modifier modifier)
        {
            BindingFlags bindingFlags = BindingFlags.Default;

            if (modifier == Modifier.PROTECTED || modifier == Modifier.PRIVATE || modifier == Modifier.INTERNAL)
            {
                bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic;
            }
            else if (modifier == Modifier.PUBLIC_STATIC)
            {
                bindingFlags = BindingFlags.Instance | BindingFlags.Public | BindingFlags.Static;
            }
            else if (modifier == Modifier.PRIVATE_STATIC)
            {
                bindingFlags = BindingFlags.Instance | BindingFlags.NonPublic | BindingFlags.Static;
            }

            return bindingFlags;
        }
    }
}
