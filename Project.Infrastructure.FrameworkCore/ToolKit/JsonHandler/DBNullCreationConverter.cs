using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json;

namespace Project.Infrastructure.FrameworkCore.ToolKit.JsonHandler
{
    public class DBNullCreationConverter : JsonConverter
    {
        /// <summary>
        /// 是否允许转换
        /// </summary>
        public override bool CanConvert(Type objectType)
        {
            bool canConvert = false;
            switch (objectType.FullName)
            {
                case "System.DBNull":

                    canConvert = true;
                    break;
            }
            return canConvert;
        }

        public override object ReadJson(JsonReader reader, Type objectType, object existingValue, JsonSerializer serializer)
        {
            return existingValue;
        }

        public override void WriteJson(JsonWriter writer, object value, JsonSerializer serializer)
        {
            writer.WriteValue(string.Empty);
        }

        public override bool CanRead
        {
            get
            {
                return false;
            }
        }
        /// <summary>
        /// 是否允许转换JSON字符串时调用
        /// </summary>
        public override bool CanWrite
        {
            get
            {
                return true;
            }
        }
    }
}
