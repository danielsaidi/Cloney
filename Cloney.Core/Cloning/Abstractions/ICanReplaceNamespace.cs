namespace Cloney.Core.Cloning.Abstractions
{
    public interface ICanReplaceNamespace
    {
        string ReplaceNamespace(string str, string sourceNamespace, string targetNamespace);
    }
}
