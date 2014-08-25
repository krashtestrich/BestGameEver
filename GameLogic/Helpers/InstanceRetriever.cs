using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;

namespace GameLogic.Helpers
{
    public class InstanceRetriever<TType> where TType : class 
    {
        public IEnumerable<TType> RetrieveInstances()
        {
            return from t in Assembly.GetExecutingAssembly().GetTypes()
                            where t.GetInterfaces().Contains(typeof(TType))
                                     && t.GetConstructor(Type.EmptyTypes) != null
                            select Activator.CreateInstance(t) as TType;
        }
    }
}
