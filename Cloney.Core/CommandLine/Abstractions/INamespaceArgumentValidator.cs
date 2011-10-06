namespace Cloney.Core.CommandLine.Abstractions
{
    public interface INamespaceArgumentValidator
    {
        void Validate(string value, string errorMessage);
    }
}