namespace DM.Log.Common
{
    using System;
    using System.Data;

    public static class CheckNotNullExtension
    {
        #region CheckPara

        /// <summary>
        /// obj == null => throw
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        public static void CheckPara<T>(this T obj, string name = null) where T : class
        {
            if (obj is string strObj && string.IsNullOrWhiteSpace(strObj))
            {
                throw new ArgumentNullException(name ?? typeof(T).Name);
            }
            else if (obj == null)
            {
                throw new ArgumentNullException(name ?? typeof(T).Name);
            }
        }

        /// <summary>
        /// number == 0 => throw
        /// </summary>
        /// <param name="number"></param>
        /// <param name="name"></param>
        public static void CheckNumber(this int number, string name)
        {
            if (number == 0)
            {
                throw new ArgumentNullException(name);
            }
        }
        public static void CheckNumber(this long number, string name)
        {
            if (number == 0)
            {
                throw new ArgumentNullException(name);
            }
        }

        /// <summary>
        /// number == 0 => throw
        /// </summary>
        /// <param name="number"></param>
        /// <param name="name"></param>
        public static void CheckNumber(this double number, string name)
        {
            if (number == 0)
            {
                throw new ArgumentNullException(name);
            }
        }

        #endregion


        /// <summary>
        /// obj == null => throw
        /// </summary>
        /// <param name="obj"></param>
        /// <param name="name"></param>
        public static void CheckData<T>(this T obj, string name = null) where T : class
        {
            if (obj == null)
            {
                throw new DataException(string.Format("No { 0 } find.", name ?? typeof(T).Name));
            }
        }

    }
}
