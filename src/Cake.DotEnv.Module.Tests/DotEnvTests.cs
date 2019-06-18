using System;
using System.Linq;

using Xunit;

namespace Cake.DotEnv.Module.Tests
{
    public class DotEnvTests : IDisposable
    {
        private readonly FakeCakeContext context;

        public DotEnvTests()
        {
            context = new FakeCakeContext();
        }

        public void Dispose()
        {
            context.DumpLogs();
        }

        [Fact]
        public void ParseEnvString()
        {
            var data = @"
DOTENV_TEST_ENV1=Value 1

DOTENV_TEST_ENV2=Value 2
    DOTENV_TEST_ENV3 = Value 3
#DOTENV_TEST_ENV4=Value 4";

            CakeAliases.LoadEnvString(context.CakeContext, data);

            Assert.Equal("Value 1", Environment.GetEnvironmentVariable("DOTENV_TEST_ENV1"));
            Assert.Equal("Value 2", Environment.GetEnvironmentVariable("DOTENV_TEST_ENV2"));
            Assert.Equal("Value 3", Environment.GetEnvironmentVariable("DOTENV_TEST_ENV3"));
            Assert.Null(Environment.GetEnvironmentVariable("DOTENV_TEST_ENV4"));

            var vars = Environment.GetEnvironmentVariables().Keys.OfType<string>().Where(x => x.StartsWith("DOTENV_TEST_")).ToList();

            Assert.Equal(3, vars.Count);

            // Delete test vars
            foreach (var variable in vars)
            {
                Environment.SetEnvironmentVariable(variable, null);
            }
        }

        [Fact]
        public void ParseEnvFile()
        {
            Assert.True(CakeAliases.LoadDotEnv(context.CakeContext, "Testdata\\testdata01.env"));

            Assert.Equal(".COM;.EXE;.BAT;.CMD;.VBS;.VBE;.JS;.JSE;.WSF;.WSH;.MSC;.PY;.PYW", Environment.GetEnvironmentVariable("DOTENV_TEST_PATHEXT"));
            Assert.Equal("$P$G", Environment.GetEnvironmentVariable("DOTENV_TEST_PROMPT"));
            Assert.Equal("-Xms256m -Xmx512m", Environment.GetEnvironmentVariable("DOTENV_TEST_JAVA_OPTS"));

            var vars = Environment.GetEnvironmentVariables().Keys.OfType<string>().Where(x => x.StartsWith("DOTENV_TEST_")).ToList();

            Assert.Equal(17, vars.Count);

            // Delete test vars
            foreach (var variable in vars)
            {
                Environment.SetEnvironmentVariable(variable, null);
            }
        }
    }
}
