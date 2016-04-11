using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace BootstrapGenerator
{
    public class GeneratorConfiguration
    {
        BootstrapGenerator generator { get; set; }
        List<Type> CustomTypes { get; set; }

        public GeneratorConfiguration(BootstrapGenerator generator)
        {
            this.generator = generator;
            CustomTypes = new List<Type>();
        }

        public void AddObjectToClassMap(object obj)
        {
            CustomTypes.Add(obj.GetType());
        }

        public bool IsPropertyTypeSupported(Type propType)
        {
            if (propType == typeof(string))
                return true;
            else if (propType == typeof(int))
                return true;
            else if (propType == typeof(DateTime))
                return true;
            else if (propType == typeof(long))
                return true;
            else if (propType == typeof(decimal))
                return true;
            else
                return IsCustomTypeMapped(propType);
        }

        bool IsCustomTypeMapped(Type propType)
        {
            foreach(Type customType in CustomTypes)
            {
                if (propType == customType)
                    return true;
            }

            return false;
        }
    }
}
