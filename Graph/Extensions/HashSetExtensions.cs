using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCollection.Extensions
{
    public static class HashSetExtensions
    {
        #region TryGetValue
        /// <summary>
        /// Durchsucht das HashSet nach einem 'gleichen' Wert und gibt den gleichen Wert zurück, wenn es einene findet
        /// </summary>
        /// <typeparam name="T">Typ des Wertes.</typeparam>
        /// <param name="hashSet">Das HashSet</param>
        /// <param name="equalValue">Der Wert, nach dem gesucht wird.</param>
        /// <param name="actualValue">Der gleiche wert, wenn vorhanden.</param>
        /// <returns>True, wenn der Wert gefunden werden konnte, False wenn nicht.</returns>
        public static bool TryGetValue<T>(this HashSet<T> hashSet, T equalValue, out T actualValue)
        {
            if (hashSet.Contains(equalValue))
            {
                int i = hashSet.ToList().IndexOf(equalValue);
                actualValue = hashSet.ToList()[i];
                return true;
            }

            actualValue = default(T);
            return false;
        }
        #endregion
    }
}
