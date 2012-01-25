using NExtra.Localization;

namespace Cloney.Core
{
    public class LanguageProvider : ResourceManagerFacade
    {
        public LanguageProvider()
            : base(Language.ResourceManager)
        {
        }
    }
}
