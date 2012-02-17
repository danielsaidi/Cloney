using System.Globalization;

namespace Cloney.Core.Localization
{
    /// <summary>
    /// This interface can be implemented by classes that
    /// can be used to translate language keys, e.g. from
    /// resource or XML files.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://www.dotnextra.com
    /// </remarks>
    public interface ITranslator
    {
        /// <summary>
        /// Translate a certain language key for the current culture.
        /// </summary>
        string Translate(string key);

        ///<summary>
        /// Translate a certain language key for a certain culture.
        ///</summary>
        string Translate(string key, CultureInfo cultureInfo);

        /// <summary>
        /// Check whether or not a translation exists for
        /// a certain language key and the current culture.
        /// </summary>
        bool TranslationExists(string key);

        /// <summary>
        /// Check whether or not a translation exists for
        /// a certain language key and culture.
        /// </summary>
        bool TranslationExists(string key, CultureInfo cultureInfo);
    }
}
