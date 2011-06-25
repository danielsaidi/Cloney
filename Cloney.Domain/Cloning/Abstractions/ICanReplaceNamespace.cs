namespace Cloney.Domain.Cloning.Abstractions
{
    public interface ICanReplaceNamespace
    {
        string ReplaceNamespace(string str, string sourceNamespace, string targetNamespace);
    }
}
