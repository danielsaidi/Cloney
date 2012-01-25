namespace Cloney.Core.Old.Abstractions
{
    public interface ICanReplaceNamespace
    {
        string ReplaceNamespace(string str, string sourceNamespace, string targetNamespace);
    }
}
