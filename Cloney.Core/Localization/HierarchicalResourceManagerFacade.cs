using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace Cloney.Core.Localization
{
    ///<summary>
    /// This class can translate language keys hierarchically,
    /// using a resource file. Unlike its base class, it will
    /// strip language keys, piece by piece, until it finds a
    /// translation, if one exists.
    ///</summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://www.dotnextra.com
    /// 
    /// If we take the key Domain_User_UserName, for instance,
    /// the class uses it directly, without any stripping. If
    /// no translation is found and we use _ as key separator,
    /// it then uses User_UserName, then finally UserName.
    /// </remarks>
    public class HierarchicalResourceManagerFacade : ResourceManagerFacade
    {
        private readonly string keySeparator = "_";


        /// <summary>
        /// Create an instance of the class, using a
        /// custom resource manager and key separator.
        /// </summary>
        public HierarchicalResourceManagerFacade(ResourceManager resourceManager, string keySeparator = "_")
            : base(resourceManager)
        {
            this.keySeparator = keySeparator;
        }


        /// <summary>
        /// Retrieve all translation keys for a certain key.
        /// For instance, "User_UserName" will result in an
        /// IEnumerable with "User" and "UserName".
        /// </summary>
        public IEnumerable<string> GetKeys(string key)
        {
            var result = new List<string>();

            while (true)
            {
                result.Add(key);

                if (key.IndexOf(keySeparator) < 0)
                    break;

                key = key.Substring(key.IndexOf(keySeparator) + 1);
            }

            return result;
        }

        /// <summary>
        /// Translate a certain language key for the current culture.
        /// </summary>
        public override string Translate(string key)
        {
            return Translate(key, Thread.CurrentThread.CurrentUICulture);
        }

        ///<summary>
        /// Translate a certain language key for a certain culture.
        ///</summary>
        public override string Translate(string key, CultureInfo cultureInfo)
        {
            var currentVerb = key;

            while (true)
            {
                var result = base.Translate(currentVerb, cultureInfo);
                if (result != null)
                    return result;

                if (currentVerb.IndexOf(keySeparator) < 0)
                    return null;

                currentVerb = currentVerb.Substring(currentVerb.IndexOf(keySeparator) + 1);
            }
        }
    }
}
