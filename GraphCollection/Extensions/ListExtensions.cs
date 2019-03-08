using System.Collections.Generic;

namespace GraphCollection.Extensions
{
    public static class ListExtensions
    {
        #region TryGetValue
        /// <summary>
        /// Durchsucht die Liste nach einem 'gleichen' Wert und gibt den gleichen Wert zurück, wenn es einene findet
        /// </summary>
        /// <typeparam name="T">Typ des Wertes.</typeparam>
        /// <param name="hashSet">Die Liste</param>
        /// <param name="equalValue">Der Wert, nach dem gesucht wird.</param>
        /// <param name="actualValue">Der gleiche wert, wenn vorhanden.</param>
        /// <returns>True, wenn der Wert gefunden werden konnte, False wenn nicht.</returns>
        public static bool TryGetValue<T>(this List<T> list, T equalValue, out T actualValue)
        {
            if (list.Contains(equalValue))
            {
                int i = list.IndexOf(equalValue);
                actualValue = list[i];
                return true;
            }

            actualValue = default(T);
            return false;
        }
        #endregion
    }
}
