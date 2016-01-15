using System;

namespace PalmDB
{
    /// <summary>
    /// Typical guard class that contains methods to validate method arguments.
    /// </summary>
    internal static class Guard
    {
        /// <summary>
        /// Throws a <see cref="ArgumentNullException"/> if the specified <paramref name="argument"/> is <c>null</c>.
        /// </summary>
        /// <param name="argument">The argument to check for null.</param>
        /// <param name="name">The name of the argument.</param>
        public static void NotNull(object argument, string name)
        {
            if (argument == null)
                throw new ArgumentNullException(name);
        }

        /// <summary>
        /// Throws a <see cref="ArgumentException"/> if the specified <paramref name="argument"/> is less than 0.
        /// </summary>
        /// <param name="argument">The argument.</param>
        /// <param name="name">The name.</param>
        public static void NotNegative(int argument, string name)
        {
            if (argument < 0)
                throw new ArgumentException($"{name} cannot be less than 0.", name);
        }

        /// <summary>
        /// Throws a <see cref="ArgumentException"/> if the specified <paramref name="argument"/> is <c>true</c>.
        /// </summary>
        /// <param name="argument">The argument that should not be true.</param>
        /// <param name="name">The name.</param>
        public static void Not(bool argument, string name)
        {
            if (argument)
                throw new ArgumentException(string.Empty, name);
        }
    }
}