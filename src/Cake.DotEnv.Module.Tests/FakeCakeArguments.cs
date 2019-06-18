﻿using System;
using System.Collections.Generic;

using Cake.Core;

namespace Cake.DotEnv.Module.Tests
{
    internal sealed class FakeCakeArguments : ICakeArguments
    {
        private readonly Dictionary<string, string> _arguments;

        /// <summary>
        /// Gets the arguments.
        /// </summary>
        /// <value>The arguments.</value>
        public IReadOnlyDictionary<string, string> Arguments => _arguments;

        /// <summary>
        /// Initializes a new instance of the <see cref="FakeCakeArguments"/> class.
        /// </summary>
        public FakeCakeArguments()
        {
            _arguments = new Dictionary<string, string>(StringComparer.OrdinalIgnoreCase);
        }

        /// <summary>
        /// Initializes the argument list.
        /// </summary>
        /// <param name="arguments">The arguments.</param>
        public void SetArguments(IDictionary<string, string> arguments)
        {
            if (arguments == null)
            {
                throw new ArgumentNullException(nameof(arguments));
            }

            _arguments.Clear();

            foreach ((string key, string value) in arguments)
            {
                _arguments.Add(key, value);
            }
        }

        /// <summary>
        /// Determines whether or not the specified argument exist.
        /// </summary>
        /// <param name="name">The argument name.</param>
        /// <returns>
        ///   <c>true</c> if the argument exist; otherwise <c>false</c>.
        /// </returns>
        public bool HasArgument(string name)
        {
            return _arguments.ContainsKey(name);
        }

        /// <summary>
        /// Gets an argument.
        /// </summary>
        /// <param name="name">The argument name.</param>
        /// <returns>The argument value.</returns>
        public string GetArgument(string name)
        {
            return _arguments.ContainsKey(name) ? _arguments[name] : null;
        }
    }
}
