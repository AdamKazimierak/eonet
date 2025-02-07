using System.Reflection;

namespace Eonet.Service.Infrastructure
{
    public class Reference
    {
        public static Assembly Assembly => Assembly.GetAssembly(typeof(Reference));
    }
}
