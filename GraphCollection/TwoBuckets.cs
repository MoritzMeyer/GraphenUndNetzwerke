using System;

namespace GraphCollection
{
    /// <summary>
    /// Klasse die zwei Eimer repräsentiert.
    /// </summary>
    public class TwoBuckets
    {
        #region fields
        /// <summary>
        /// Die größe des ersten Buckets.
        /// </summary>
        public int SizeBucket1 { get; set; }

        /// <summary>
        /// Die größe des zweiten Buckets.
        /// </summary>
        public int SizeBucket2 { get; set; }

        /// <summary>
        /// Inhalt des ersten Buckets
        /// </summary>
        internal int contentBucket1;

        /// <summary>
        /// Inhalt des zweiten Buckets.
        /// </summary>
        internal int contentBucket2;
        #endregion

        #region ctors
        /// <summary>
        /// Erzeugt eine neue Instanz der Klasse.
        /// </summary>
        /// <param name="bucket1">Die größe des ersten Buckets.</param>
        /// <param name="bucket2">Die größe des zweiten Buckets.</param>
        public TwoBuckets(int sizeBucket1, int sizeBucket2)
        {
            this.SizeBucket1 = sizeBucket1;
            this.SizeBucket2 = sizeBucket2;
        }

        /// <summary>
        /// Erzeugt eine neue Instanz der Klasse.
        /// </summary>
        /// <param name="sizeBucket1">Die größe des ersten Buckets.</param>
        /// <param name="sizeBucket2">Die größe des zweiten Buckets.</param>
        /// <param name="contentBucket1">Der Inhalt des ersten Buckets.</param>
        /// <param name="contentBucket2">Der Inhalt des zweiten Buckets.</param>
        public TwoBuckets(int sizeBucket1, int sizeBucket2, int contentBucket1, int contentBucket2)
            : this(sizeBucket1, sizeBucket2)
        {
            this.contentBucket1 = contentBucket1;
            this.contentBucket2 = contentBucket2;
        }
        #endregion

        #region FillB1
        /// <summary>
        /// Füllt den Bucket1
        /// </summary>
        /// <returns>Das neue TwoBucket Objekt, bei dem der erste Bucket gefüllt ist.</returns>
        public TwoBuckets FillB1()
        {
            if (!this.CanFillB1())
            {
                return null;
                //throw new InvalidOperationException("Bucket1 kann nicht gefüllt werden.");
            }
            else
            {
                return new TwoBuckets(this.SizeBucket1, this.SizeBucket2, this.SizeBucket1, this.contentBucket2);
            }
        }
        #endregion

        #region FillB2
        /// <summary>
        /// Füllt den Bucket2
        /// </summary>
        /// <returns>Das neue TwoBucket Objekt, bei dem der zweite Bucket gefüllt ist.</returns>
        public TwoBuckets FillB2()
        {
            if (!this.CanFillB2())
            {
                return null;
                //throw new InvalidOperationException("Bucket2 kann nicht gefüllt werden.");
            }
            else
            {
                return new TwoBuckets(this.SizeBucket1, this.SizeBucket2, this.contentBucket1, this.SizeBucket2);
            }
        }
        #endregion

        #region FillB1FromB2
        /// <summary>
        /// Erzeugt einen Vertex, bei dem der Inhalt der des zweiten Buckets, dem des ersten Buckets hinzugefügt wird.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Wenn der Inhalt des zweiten Buckets größer ist als der Platz in Bucket1</exception>
        public TwoBuckets FillB1FromB2()
        {
            if (!this.CanFillB1() || this.contentBucket2 < 1)
            {
                return null;
                //throw new InvalidOperationException("Es ist zu wenig Platz in Bucket1");
            }

            if (this.contentBucket2 > (this.SizeBucket1 - this.contentBucket1))
            {
                return new TwoBuckets(this.SizeBucket1, this.SizeBucket2, this.SizeBucket1, this.contentBucket2 - (this.SizeBucket1 - this.contentBucket1));
            }
            else
            {
                return new TwoBuckets(this.SizeBucket1, this.SizeBucket2, this.contentBucket1 + this.contentBucket2, 0);
            }            
        }
        #endregion

        #region FillB2FromB1
        /// <summary>
        /// Erzeugt einen Vertex, bei dem der Inhalt der des ersten Buckets, dem des zweiten Buckets hinzugefügt wird.
        /// </summary>
        /// <exception cref="ArgumentOutOfRangeException">Wenn der Inhalt des ersten Buckets größer ist als der Platz in Bucket2</exception>
        public TwoBuckets FillB2FromB1()
        {
            if (!this.CanFillB2() || this.contentBucket1 < 1)
            {
                return null;
                //throw new InvalidOperationException("Es ist zu wenig Platz in Bucket2");
            }
            
            if (this.contentBucket1 > (this.SizeBucket2 - this.contentBucket2))
            {
                return new TwoBuckets(this.SizeBucket1, this.SizeBucket2, this.contentBucket1 - (this.SizeBucket2 - this.contentBucket2), this.SizeBucket2);
            }
            else
            {
                return new TwoBuckets(this.SizeBucket1, this.SizeBucket2, 0, this.contentBucket1 + this.contentBucket2);
            }
        }
        #endregion

        #region EmptyB1
        /// <summary>
        /// Erzeugt ein neues TwoBucket Objekt, bei dem der erste Bucket geleert wurde.
        /// </summary>
        /// <returns>Den neu erzeugten Bucket.</returns>
        public TwoBuckets EmptyB1()
        {
            if (!this.CanEmptyB1())
            {
                return null;
                //throw new InvalidOperationException("Bucket1 ist bereits leer.");
            }
            else
            {
                return new TwoBuckets(this.SizeBucket1, this.SizeBucket2, 0, this.contentBucket2);
            }
        }
        #endregion

        #region EmptyB2
        /// <summary>
        /// Erzeugt ein neues TwoBucket Objekt, bei dem der erste Bucket geleert wurde.
        /// </summary>
        /// <returns>Den neu erzeugten Bucket.</returns>
        public TwoBuckets EmptyB2()
        {
            if (!this.CanEmptyB2())
            {
                return null;
                //throw new InvalidOperationException("Bucket1 ist bereits leer.");
            }
            else
            {
                return new TwoBuckets(this.SizeBucket1, this.SizeBucket2, this.contentBucket1, 0);
            }
        }
        #endregion

        #region CanFillB1
        /// <summary>
        /// Prüft, ob Bucket1 gefüllt werden kann.
        /// </summary>
        /// <returns>True, wenn Bucket1 gefüllt werden kann, false sonst.</returns>
        public bool CanFillB1()
        {
            return this.contentBucket1 < this.SizeBucket1;            
        }
        #endregion

        #region CanFillB2
        /// <summary>
        /// Prüft, ob Bucket2 gefüllt werden kann.
        /// </summary>
        /// <returns>True, wenn Bucket2 gefüllt werden kann, false sonst.</returns>
        public bool CanFillB2()
        {
            return this.contentBucket2 < this.SizeBucket2;
        }
        #endregion

        #region CanFillB1FromB2
        /// <summary>
        /// Prüft, ob der Bucket1 von Bucket2 aus gefüllt werden kann.
        /// </summary>
        /// <returns>True, wenn genug Platz in Bucket1 ist, false wennn nicht.</returns>
        [Obsolete]
        public bool CanFillB1FromB2()
        {
            return (this.contentBucket1 + this.contentBucket2) <= this.SizeBucket1;
        }
        #endregion

        #region CanFillB2FromB1
        /// <summary>
        /// Prüft, ob der Bucket2 von Bucket1 aus gefüllt werden kann.
        /// </summary>
        /// <returns>True, wenn genug Platz in Bucket2 ist, false wenn nicht.</returns>
        [Obsolete]
        public bool CanFillB2FromB1()
        {
            return (this.contentBucket1 + this.contentBucket2) <= this.SizeBucket2;
        }
        #endregion

        #region CanEmptyB1
        /// <summary>
        /// Prüft, ob Bucket1 geleert werden kann.
        /// </summary>
        /// <returns></returns>
        public bool CanEmptyB1()
        {
            return this.contentBucket1 > 0;
        }
        #endregion

        #region CanEmptyB2
        /// <summary>
        /// Prüft, ob Bucket2 geleert werden kann.
        /// </summary>
        /// <returns></returns>
        public bool CanEmptyB2()
        {
            return this.contentBucket2 > 0;
        }
        #endregion

        #region Equals
        /// <summary>
        /// Überschreibt die Equals-Methode.
        /// </summary>
        /// <param name="obj">Das Objekt mit dem verglichen wird.</param>
        /// <returns>True wenn this und obj gleich, false wenn nicht.</returns>
        public override bool Equals(object obj)
        {
            if (obj == null || !this.GetType().Equals(obj.GetType()))
            {
                return false;
            }
            else
            {
                TwoBuckets other = (TwoBuckets)obj;
                return (other.SizeBucket1 == this.SizeBucket1 &&
                        other.SizeBucket2 == this.SizeBucket2 &&
                        other.contentBucket1 == this.contentBucket1 &&
                        other.contentBucket2 == this.contentBucket2);
            }
        }
        #endregion

        #region GetHasCode
        /// <summary>
        /// Überschreibt die GetHashCode Methode für diese Klasse.
        /// </summary>
        /// <returns>Der HashCode.</returns>
        public override int GetHashCode()
        {
            return this.SizeBucket1.GetHashCode() ^ this.SizeBucket1.GetHashCode() ^ this.contentBucket1.GetHashCode() ^ this.contentBucket2.GetHashCode();
        }
        #endregion

        #region ToString
        /// <summary>
        /// Die Stringrepräsentation des Objekts
        /// </summary>
        /// <returns></returns>
        public override string ToString()
        {
            return this.contentBucket1 + "-" + this.contentBucket2;
        }
        #endregion
    }
}