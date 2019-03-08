using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCollection.Extensions
{
    public static class IntExtensions
    {
        #region Faculty
        /// <summary>
        /// Berechnet die Fakultät
        /// </summary>
        /// <param name="i">Die Zahl, deren Fakultät berechnet werden soll.</param>
        /// <returns>Die Fakultät.</returns>
        public static int Faculty(this int i)
        {
            int faculty = 1;
            for (int j = i; j > 0; j--)
            {
                faculty = j * faculty;
            }

            return faculty;
        }
        #endregion
    }
}
