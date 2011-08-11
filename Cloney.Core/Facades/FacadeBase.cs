namespace Cloney.Core.Facades
{
    /// <summary>
    /// This class is the base class of all .NET Extension
    /// facade classes. It can wrap any base class and has
    /// a property that returns the base instance.
    /// </summary>
    /// <remarks>
    /// Author:     Daniel Saidi [daniel.saidi@gmail.com]
    /// Link:       http://www.saidi.se/netextensions
    /// </remarks>
    /// <typeparam name="T">The type that is to be wrapped.</typeparam>
    public class FacadeBase<T>
    {
        /// <summary>
        /// Create an instance of the class.
        /// </summary>
        /// <param name="baseInstance">The base instance that the class should be a facade for.</param>
        public FacadeBase(T baseInstance)
        {
            BaseInstance = baseInstance;
        }


        /// <summary>
        /// The base instance that the class is a facade for.
        /// </summary>
        public T BaseInstance { get; protected set; }
    }
}
