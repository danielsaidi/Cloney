using Cloney.Core.Cloning.Abstractions;

namespace Cloney.Core.Cloning
{
    public class NamespaceReplacer : ICanReplaceNamespace
    {
        public string ReplaceNamespace(string str, string sourceNamespace, string targetNamespace)
        {
            str = str.Replace(sourceNamespace, targetNamespace);
            str = str.Replace(sourceNamespace.ToLower(), targetNamespace.ToLower());
            return str;
        }
    }
}
