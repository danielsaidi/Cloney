using System.Globalization;
using System.Resources;
using System.Threading;

namespace Cloney.Core.Localization
{
    ///<summary>
    /// This class can be used to translate language keys
    /// using a resource file. It requires an exact match
    /// to translate a language key.
    ///</summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://www.dotnextra.com
    /// </remarks>
    public class ResourceManagerFacade : ITranslator
    {
        private readonly ResourceManager resourceManager;


        /// <summary>
        /// Create an instance of the class, using a
        /// custom resource manager.
        /// </summary>
        public ResourceManagerFacade(ResourceManager resourceManager)
        {
            this.resourceManager = resourceManager;
        }


        /// <summary>
        /// Translate a certain language key for the current culture.
        /// </summary>
        public virtual string Translate(string key)
        {
            return Translate(key, Thread.CurrentThread.CurrentUICulture);
        }

        ///<summary>
        /// Translate a certain language key for a certain culture.
        ///</summary>
        public virtual string Translate(string key, CultureInfo cultureInfo)
        {
            return resourceManager.GetString(key, cultureInfo);
        }

        /// <summary>
        /// Check whether or not a translation exists for
        /// a certain language key and the current culture.
        /// </summary>
        public bool TranslationExists(string key)
        {
            return TranslationExists(key, Thread.CurrentThread.CurrentUICulture);
        }

        /// <summary>
        /// Check whether or not a translation exists for
        /// a certain language key and culture.
        /// </summary>
        public bool TranslationExists(string key, CultureInfo cultureInfo)
        {
            return Translate(key, cultureInfo) != null;
        }
    }
}
