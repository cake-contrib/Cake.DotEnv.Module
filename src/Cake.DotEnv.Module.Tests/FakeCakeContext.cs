using System;

using Cake.Core;
using Cake.Core.IO;
using Cake.Core.Tooling;
using Cake.Testing;

using Path = System.IO.Path;
using NSubstitute;

namespace Cake.DotEnv.Module.Tests
{

    public class FakeCakeContext
    {
        private readonly ICakeContext context;
        private readonly FakeLog log;
        private readonly DirectoryPath testsDir;

        public FakeCakeContext()
        {
            testsDir = new DirectoryPath(Path.GetFullPath(AppContext.BaseDirectory));

            var environment = FakeEnvironment.CreateUnixEnvironment(false);

            var fileSystem = new FakeFileSystem(environment);
            var globber = new Globber(fileSystem, environment);
            log = new FakeLog();
            var args = new FakeCakeArguments();
            var registry = new WindowsRegistry();

            var config = new FakeConfiguration();
            var tools = new ToolLocator(environment, new ToolRepository(environment), new ToolResolutionStrategy(fileSystem, environment, globber, config));
            var processRunner = new ProcessRunner(fileSystem, environment, log, tools, config);
            var data = Substitute.For<ICakeDataService>();
            context = new CakeContext(fileSystem, environment, globber, log, args, processRunner, registry, tools, data, config);
            context.Environment.WorkingDirectory = testsDir;
        }

        public DirectoryPath WorkingDirectory => testsDir;

        public ICakeContext CakeContext => context;

        public string GetLogs()
        {
            return string.Join(Environment.NewLine, log.Entries);
        }

        public void DumpLogs()
        {
            foreach (var m in log.Entries)
                Console.WriteLine(m);
        }
    }
}
