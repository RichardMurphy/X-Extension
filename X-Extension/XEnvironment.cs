using System;
using Microsoft.SmallBasic.Library;


namespace Jibba
{
    /// <summary>
    /// Provides some Environment properties and methods
    /// </summary>
    [SmallBasicType]
    class XEnvironment
    {
        /// <summary>
        /// Gets the newline character for the current environment
        /// </summary>
        /// <returns>a newline</returns>
        public static Primitive NewLine
        {
            get { return Environment.NewLine; }
        }

        /// <summary>
        /// Gets the Users name for the current environment
        /// </summary>
        /// <returns>Users name</returns>
        public static Primitive UserName
        {
            get { return Environment.UserName; }
        }
            
    }
}
