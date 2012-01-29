namespace Cloney.Core.Namespace
{
    /// <summary>
    /// This interface can be implemented by classes
    /// that can be used to extract a namespace from
    /// a string value.
    /// </summary>
    public interface INamespaceResolver
    {
        string ResolveNamespace(string str);
    }
}
