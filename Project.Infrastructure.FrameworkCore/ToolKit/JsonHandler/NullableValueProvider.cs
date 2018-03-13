using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using Newtonsoft.Json.Serialization;

namespace Project.Infrastructure.FrameworkCore.ToolKit.JsonHandler
{
    public class NullableValueProvider : IValueProvider
    {
    
        private readonly IValueProvider _underlyingValueProvider;


        public NullableValueProvider(MemberInfo memberInfo)
        {
            _underlyingValueProvider = new DynamicValueProvider(memberInfo);
        
        }

        public void SetValue(object target, object value)
        {
            _underlyingValueProvider.SetValue(target, value);
        }

        public object GetValue(object target)
        {
            return _underlyingValueProvider.GetValue(target) ?? "";
        }
    }

    public class SpecialContractResolver : DefaultContractResolver
    {
        protected override IValueProvider CreateMemberValueProvider(MemberInfo member)
        {
            if (member.MemberType == MemberTypes.Property)
            {
                var pi = (PropertyInfo)member;
                if (pi.PropertyType== typeof(string))
                {
                    return new NullableValueProvider(member);
                }
            }
            else if (member.MemberType == MemberTypes.Field)
            {
                var fi = (FieldInfo)member;
                if (fi.FieldType == typeof(string))
                    return new NullableValueProvider(member);
            }

            return base.CreateMemberValueProvider(member);
        }
    }
}
