using System;
using System.Collections.Generic;
using System.Runtime.CompilerServices;
using System.Text;

[assembly: InternalsVisibleTo("Tests")]
[assembly: InternalsVisibleTo("GraphSearch")]

namespace GraphCollection
{
    public static class BooleanExtensions
    {
        #region ToZeroAndOnes
        /// <summary>
        /// Wandelt ein boolean Wert in 1(True) oder 0(False) um.
        /// </summary>
        /// <param name="boolean">Der umzuwandelnde Wert.</param>
        /// <returns>Die String Repräsentation</returns>
        public static string ToZeroAndOnes(this bool boolean)
        {
            if (boolean)
            {
                return "1";
            }
            else
            {
                return "0";
            }
        }
        #endregion
    }
}
