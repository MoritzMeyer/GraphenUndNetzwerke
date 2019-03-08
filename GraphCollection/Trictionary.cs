using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GraphCollection
{
    public class Trictionary<Tkey1, Tkey2, TValue>
    {
        #region fields
        private List<Tkey1> key1s;
        private List<Tkey2> key2s;
        private List<TValue> values;
        #endregion

        #region ctors
        public Trictionary()
        {
            key1s = new List<Tkey1>();
            key2s = new List<Tkey2>();
            values = new List<TValue>();
        }
        #endregion

        #region Add
        /// <summary>
        /// Fügt dem Dictionary einen neuen Eintrag hinzu.
        /// </summary>
        /// <param name="key1">Key1</param>
        /// <param name="key2">Key2</param>
        /// <param name="value">Der Wert</param>
        /// <returns>True, wenn der Eintrag hinzugefügt werden konnte, false wenn nicht.</returns>
        public bool Add(Tkey1 key1, Tkey2 key2, TValue value)
        {
            key1s.Add(key1);
            key2s.Add(key2);
            values.Add(value);

            for(int i = 0; i < key1s.Count; i++)
            {
                if (key1s[i].Equals(key1) && key2s[i].Equals(key2) && values[i].Equals(value))
                {
                    return true;
                }
            }

            key1s.Remove(key1);
            key2s.Remove(key2);
            values.Remove(value);

            return false;
        }
        #endregion

        #region Remove
        /// <summary>
        /// Entfernt einen Eintrag aus dem Trictionary
        /// </summary>
        /// <param name="key1">Key1</param>
        /// <param name="key2">Key2</param>
        /// <param name="value">Der Wert</param>
        /// <returns>True, wenn der Eintrag entfernt werden konnte, false wenn nicht.</returns>
        public bool Remove(Tkey1 key1, Tkey2 key2, TValue value)
        {
            if (!this.HasValueForKeys(key1, key2))
            {
                return false;
            }

            return key1s.Remove(key1) && key2s.Remove(key2) && values.Remove(value);
        }
        #endregion

        #region Contains
        /// <summary>
        /// Prüft ob ein Wert vorhanden ist.
        /// </summary>
        /// <param name="key1">Key1</param>
        /// <param name="key2">Key2</param>
        /// <returns>True, wenn das Triple vohanden ist, false wenn nicht.</returns>
        public bool Contains(TValue value)
        {
            return values.Contains(value);
        }
        #endregion

        #region HasValueForKeys
        /// <summary>
        /// Prüft, ob für die angegebenen Schlüssel ein Wert existiert.
        /// </summary>
        /// <param name="key1">Key1</param>
        /// <param name="key2">Key2</param>
        /// <returns>True, wenn ein Wert existiert, false wenn nicht.</returns>
        public bool HasValueForKeys(Tkey1 key1, Tkey2 key2)
        {
            for (int i = 0; i < key1s.Count; i++)
            {
                if (key1s[i].Equals(key1) && key2s[i].Equals(key2))
                {
                    return true;
                }
            }

            return false;
        }
        #endregion

        #region Get
        /// <summary>
        /// Liefert den Wert zu einem Schlüsselpaar.
        /// </summary>
        /// <param name="key1">Key1</param>
        /// <param name="key2">Key2</param>
        /// <returns>Den Wert für das Schlüsselpaar.</returns>
        public TValue Get(Tkey1 key1, Tkey2 key2)
        {
            if (!this.HasValueForKeys(key1, key2))
            {
                throw new KeyNotFoundException("Für dieses Schlüsselpaar existiert kein Wert");
            }

            for (int i = 0; i < key1s.Count; i++)
            {
                if (key1s[i].Equals(key1) && key2s[i].Equals(key2))
                {
                    return values[i];
                }
            }

            return default(TValue);
        }
        #endregion

        #region Set
        /// <summary>
        /// Setzt den Wert für ein vorhandenes Schlüsselpaar.
        /// </summary>
        /// <param name="key1">Key1</param>
        /// <param name="key2">Key2</param>
        /// <param name="value">Der Wert</param>
        public void Set(Tkey1 key1, Tkey2 key2, TValue value)
        {
            if (!this.HasValueForKeys(key1, key2))
            {
                throw new KeyNotFoundException("Für dieses Schlüsselpaar existiert kein Wert");
            }

            for (int i = 0; i < key1s.Count; i++)
            {
                if (key1s[i].Equals(key1) && key2s[i].Equals(key2))
                {
                    values[i] = value;
                }
            }
        }
        #endregion

        #region GetMaxStringLength
        /// <summary>
        /// Liefert die maximale Stringlänge der Werte in diesem Trictionary.
        /// </summary>
        /// <returns>Die Stringlänge</returns>
        public int GetMaxStringLength()
        {
            int length = 0;
            foreach(TValue value in values)
            {
                if (length < value.ToString().Length)
                {
                    length = value.ToString().Length;
                }
            }

            return length;
        }
        #endregion
    }
}
