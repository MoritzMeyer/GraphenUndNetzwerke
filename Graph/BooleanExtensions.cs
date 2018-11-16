namespace GraphCollection
{
    /// <summary>
    /// Extensionclass for Booleans.
    /// </summary>
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
