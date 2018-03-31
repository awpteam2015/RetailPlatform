using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Project.Infrastructure.FrameworkCore.ToolKit.Extensions
{
    /// <summary>
    /// 字典辅助类
    /// </summary>
    public static class DictionaryExtensions
    {
        /// <summary>
        /// 返回Key的Value
        /// </summary>
        public static string GetValue(this Dictionary<string, string> source, string key)
        {
            if (source.ContainsKey(key) == false) return null;
            else
                return source.First(c => c.Key == key).Value.ToString();
        }

        public static dynamic GetValue<T>(this Dictionary<string, string> source, string key)
        {
            var exist = source.ContainsKey(key);

            if (typeof(T) == typeof(string))
                return exist ? source.First(c => c.Key == key).Value.ToString() : "";
            if (typeof(T) == typeof(int))
                return exist ? Convert.ToInt32(source.First(c => c.Key == key).Value.ToString()) : 0;
            if (typeof(T) == typeof(decimal))
                return exist ? Convert.ToDecimal(source.First(c => c.Key == key).Value.ToString()) : 0;
            if (typeof(T) == typeof(bool))
                return exist ? Convert.ToBoolean(source.First(c => c.Key == key).Value.ToString()) : false;

            throw new Exception("字典扩展异常：缺少匹配条件!");
        }

        /// <summary>
        /// 更新某个KEY的VALUE
        /// </summary>
        public static Dictionary<string, string> UpdateValue(this Dictionary<string, string> source, string key, string value)
        {
            var kv = source.GetValue(key);
            if (kv != null)
            {
                source.Remove(key);
                source.Add(key, value);
            }
            else
            {
                source.Add(key, value);
            }
            return source;
        }

        public static int GetInt(this Dictionary<string, string> source, string key)
        {
            if (source.ContainsKey(key) == false) return 0;
            else return int.Parse(source.First(c => c.Key == key).Value.ToString());
        }

        public static string GetString(this Dictionary<string, string> source, string key)
        {
            if (source.ContainsKey(key) == false) return "";
            else return source.First(c => c.Key == key).Value.ToString();
        }

        public static Dictionary<string, string> Copy(this Dictionary<string, string> source)
        {
            var tempDic = new Dictionary<string, string>();
            foreach (var i in source)
            {
                tempDic.Add(i.Key, i.Value);
            }
            return tempDic;
        }
    }
}
