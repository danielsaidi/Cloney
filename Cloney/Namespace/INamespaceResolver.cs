namespace Cloney.Namespace
{
    /// <summary>
    /// This interface can be implemented by classes that
    /// can be used to extract a namespace.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
    public interface INamespaceResolver
    {
        string ResolveNamespace(string str);
    }
}
