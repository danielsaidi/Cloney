using System.Collections.Generic;
using System.Globalization;
using System.Resources;
using System.Threading;

namespace Cloney.Core.Localization
{
    ///<summary>
    /// This class can translate language keys hierarchically,
    /// using a resource file. Unlike the base class, it will
    /// strip language keys piece by piece (using the defined
    /// key separatator) until it finds a translation, if any.
    /// 
    /// This approach means that one can specify very general
    /// translations for rather basic terms (like "Password")
    /// and then override the general translation with a more
    /// specific one (like "User_Password").
    /// 
    /// If no key separator is specified, the class will make
    /// _ the default separator.
    ///</summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://danielsaidi.github.com/nextra
    /// 
    /// If we want to translate "Domain_User_UserName", using
    /// _ as key separator, the class will first check if the
    /// full language key ("Domain_User_UserName") exists. If
    /// it does not, the class will strip the first part from
    /// the key ("Domain"). It then checks if "User_UserName"
    /// exists, then finally "UserName".
    /// </remarks>
    public class HierarchicalResourceManagerTranslator : ResourceManagerTranslator
    {
        public HierarchicalResourceManagerTranslator(ResourceManager resourceManager)
            : this(resourceManager, "_")
        {
        }

        public HierarchicalResourceManagerTranslator(ResourceManager resourceManager, string keySeparator)
            : base(resourceManager)
        {
            KeySeparator = keySeparator;
        }


        public string KeySeparator { get; private set; }


        public IEnumerable<string> GetKeys(string key)
        {
            var result = new List<string>();

            while (true)
            {
                result.Add(key);

                if (key.IndexOf(KeySeparator) < 0)
                    break;

                key = key.Substring(key.IndexOf(KeySeparator) + 1);
            }

            return result;
        }

        public override string Translate(string key)
        {
            return Translate(key, Thread.CurrentThread.CurrentUICulture);
        }

        public override string Translate(string key, CultureInfo cultureInfo)
        {
            var currentVerb = key;

            while (true)
            {
                var result = base.Translate(currentVerb, cultureInfo);
                if (result != null)
                    return result;

                if (currentVerb.IndexOf(KeySeparator) < 0)
                    return null;

                currentVerb = currentVerb.Substring(currentVerb.IndexOf(KeySeparator) + 1);
            }
        }
    }
}
