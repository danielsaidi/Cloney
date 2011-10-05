using System.Collections.Specialized;

namespace Cloney.Abstractions
{
    interface IWizardFacade
    {
        bool ShouldStart(StringDictionary applicationArguments);
        bool Start(StringDictionary applicationArguments);
    }
}
