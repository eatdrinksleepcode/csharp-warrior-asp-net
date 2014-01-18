using NUnit.Framework;

namespace CSharpWarrior
{
    public class SandboxTest
    {
        private const string WorkingCode = @"
public class Player {
    public static string Play() {
        return ""Success"";
    }
}
";
        private const string IncorrectCode = @"
public class Player {
    public static string DontPlay() {
        return ""Failure"";
    }
}
";

        private const string NonCompilingCode = @"
public class Player {
    public static string Play() {
        return ""Success"";
    }

";

        private const string DangerousCode = @"
public class Player {
    public static string Play() {
        return System.IO.File.ReadAllText(@""C:\freefallprotection.log"");
    }
}
";

        private const string FaultyCode = @"
public class Player {
    public static string Play() {
        throw new System.Exception(""Fail!"");
    }
}
";

        private Sandbox sandbox;

        [SetUp]
        public void Before()
        {
            sandbox = new Sandbox();
        }

        [Test]
        public void ShouldRunProvidedCode()
        {
            Assert.That(sandbox.Execute(WorkingCode), Is.EqualTo("Success"));
        }

        [Test]
        public void ShouldFailToRunNonCompilingCode()
        {
            var ex = Assert.Throws<CodeExecutionException>(() => sandbox.Execute(NonCompilingCode));
            StringAssert.StartsWith(Sandbox.BadCodeMessage, ex.Message);
        }

        [Test]
        public void ShouldFailToRunIncorrectCode()
        {
            var ex = Assert.Throws<CodeExecutionException>(() => sandbox.Execute(IncorrectCode));
            Assert.That(ex.Message, Is.EqualTo(Sandbox.IncorrectCodeMessage));
        }

        [Test]
        public void ShouldFailToRunFaultyCode()
        {
            var ex = Assert.Throws<CodeExecutionException>(() => sandbox.Execute(FaultyCode));
            Assert.That(ex.Message, Is.EqualTo(Sandbox.FaultyCodeMessage));
            Assert.That(ex.InnerException, Is.Not.Null);
        }

        [Test]
        public void ShouldFailToRunDangerousCode()
        {
            var ex = Assert.Throws<CodeExecutionException>(() => sandbox.Execute(DangerousCode));
            Assert.That(ex.Message, Is.EqualTo(Sandbox.DangerousCodeMessage));
            Assert.That(ex.InnerException, Is.Not.Null);
        }
    }
}
