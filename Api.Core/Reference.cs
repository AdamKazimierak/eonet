using System.Reflection;

namespace Eonet.Api.Core
{
    public class Reference
    {
        public static Assembly Assembly => Assembly.GetAssembly(typeof(Reference));
    }
}
