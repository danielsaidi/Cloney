using Cloney.Core.Localization;

namespace Cloney.Core
{
    /// <summary>
    /// This class inherits from .NExtra resoure manager
    /// facade, which it sets up with the local Language
    /// resource manager.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/Cloney
    /// </remarks>
    public class LanguageProvider : ResourceManagerFacade
    {
        public LanguageProvider()
            : base(Language.ResourceManager)
        {
        }
    }
}
