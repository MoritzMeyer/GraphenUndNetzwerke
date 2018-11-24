using System.Collections.Generic;
using System.Linq;

namespace GraphCollection.Extensions
{
    public static class IEnumerableExtensions
    {
        #region IsNotNullOrEmpty
        /// <summary>
        /// Gibt an, ob die Enumeration nicht null oder leer ist.
        /// </summary>
        /// <typeparam name="T">Der generische Typ der Enum.</typeparam>
        /// <param name="iEnum">Die Enumeration.</param>
        /// <returns>False, wenn die Enum leer oder null ist.</returns>
        public static bool IsNotNullOrEmpty<T>(this IEnumerable<T> iEnum)
        {
            return iEnum != null && iEnum.Count() > 0;
        }
        #endregion

        #region IsNullOrEmpty
        /// <summary>
        /// Gibt an, ob die Enumeration null oder leer ist.
        /// </summary>
        /// <typeparam name="T">Der generische Typ der Enum.</typeparam>
        /// <param name="iEnum">Die Enumeration.</param>
        /// <returns>True, wenn die Enum leer oder null ist, false wenn nicht.</returns>
        public static bool IsNullOrEmpty<T>(this IEnumerable<T> iEnum)
        {
            return iEnum == null && iEnum.Count() == 0;
        }
        #endregion
    }
}
