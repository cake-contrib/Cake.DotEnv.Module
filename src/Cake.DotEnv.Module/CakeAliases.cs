using System;
using System.Diagnostics.CodeAnalysis;
using System.IO;
using System.Text;

using Cake.Core;
using Cake.Core.Annotations;
using Cake.Core.Diagnostics;
using Cake.Core.IO;

namespace Cake.DotEnv.Module
{
    /// <summary>
    /// Class CakeAliases.
    /// </summary>
    [CakeAliasCategory("Environment")]
    [SuppressMessage("ReSharper", "UnusedMember.Global")]
    public static class CakeAliases
    {
        /// <summary>
        /// Loads the dot env.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="dotEnvFile">The dot env file. Is <c>null</c>, the filename ".env" is assumed.</param>
        /// <param name="encoding">The encoding or the default encoding if <c>null</c></param>
        /// <returns><c>true</c> if the dotenv file is found and processed, <c>false</c> otherwise.</returns>
        /// <exception cref="ArgumentNullException">context is <c>null</c></exception>
        [CakeMethodAlias]
        [CakeAliasCategory("DotEnv")]
        public static bool LoadDotEnv(
            this ICakeContext context,
            FilePath dotEnvFile = null,
            Encoding encoding = null
        )
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (dotEnvFile == null)
            {
                dotEnvFile = new FilePath(".env");
            }

            var filename = dotEnvFile.FullPath;
            if (!File.Exists(filename))
            {
                context.Log.Error(".env file \"{0}\" not found", filename);
                return false;
            }

            var fileEncoding = encoding ?? Encoding.Default;

            var fileContent = File.ReadAllText(filename, fileEncoding);

            context.LoadEnvString(fileContent);

            return true;
        }

        /// <summary>
        /// Loads the env string.
        /// </summary>
        /// <param name="context">The context.</param>
        /// <param name="envData">The env data.</param>
        /// <exception cref="ArgumentNullException">context or envData is null/empty</exception>
        [CakeMethodAlias]
        [CakeAliasCategory("DotEnv")]
        public static void LoadEnvString(
            this ICakeContext context,
            string envData
        )
        {
            if (context == null)
            {
                throw new ArgumentNullException(nameof(context));
            }

            if (string.IsNullOrWhiteSpace(envData))
            {
                throw new ArgumentNullException(nameof(envData));
            }

            var fileContent = envData.SplitLines();

            foreach (var fileContentLine in fileContent)
            {
                var line = fileContentLine.Trim();

                // Skip empty or comment lines
                if (string.IsNullOrWhiteSpace(line) || line.StartsWith("#"))
                    continue;

                // Find first equal char
                var assignmentIndex = line.IndexOf('=');

                if (assignmentIndex == 0)
                    continue;

                var key = line.Substring(0, assignmentIndex).Trim();
                var value = line.Substring(assignmentIndex + 1, line.Length - (assignmentIndex + 1)).Trim();

                if (key.Length == 0)
                    continue;

                if (value.Length > 0)
                {
                    context.Log.Information(Verbosity.Diagnostic, "DotEnv: Setting environment variable \"{0}\" to \"{1}\"", key, value);
                    Environment.SetEnvironmentVariable(key, value);
                }
                else
                {
                    context.Log.Information(Verbosity.Diagnostic, "DotEnv: Unsetting environment variable \"{0}\"", key);
                    Environment.SetEnvironmentVariable(key, null);
                }
            }
        }
    }
}
