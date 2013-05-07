namespace Cloney.Core.Namespace
{
    /// <summary>
    /// This interface can be implemented by classes that
    /// can resolve the namespace of a folder or file.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
    public interface INamespaceResolver
    {
        string ResolveNamespace(string path);
    }
}
