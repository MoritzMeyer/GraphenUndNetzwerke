using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace GraphCollection
{
    public static class StringExtensions
    {
        #region CenterStringWithinLength
        /// <summary>
        /// Nimmt einen String und füllt einen String vorne und hinten mit Leerzeichen auf, sodass dieser eine bestimmte Läge hat.
        /// </summary>
        /// <param name="str">Der String.</param>
        /// <param name="length">Die gewünschte Länge.</param>
        /// <returns>Der modifizierte String.</returns>
        public static string CenterStringWithinLength(this String str, int length)
        {
            if (str.Length > length)
            {
                return str;
            }

            int spaceToAdd = length - str.Length;

            if (spaceToAdd % 2 > 0)
            {
                int halfSpace = (spaceToAdd - 1) / 2;
                return string.Concat(Enumerable.Repeat(" ", halfSpace)) + str + string.Concat(Enumerable.Repeat(" ", halfSpace + 1));
            }
            else
            {
                int halfSpace = spaceToAdd / 2;
                return string.Concat(Enumerable.Repeat(" ", halfSpace)) + str + string.Concat(Enumerable.Repeat(" ", halfSpace));
            }
        }
        #endregion
    }
}
