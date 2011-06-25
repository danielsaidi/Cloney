using Cloney.Domain.Cloning.Abstractions;

namespace Cloney.Domain.Cloning
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
