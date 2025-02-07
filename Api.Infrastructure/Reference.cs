using System.Reflection;

namespace Eonet.Api.Infrastructure
{
    public class Reference
    {
        public static Assembly Assembly => Assembly.GetAssembly(typeof(Reference));
    }
}
