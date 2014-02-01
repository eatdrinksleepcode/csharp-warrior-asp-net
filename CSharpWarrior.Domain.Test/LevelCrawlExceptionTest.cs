using FluentAssertions;
using NUnit.Framework;

namespace CSharpWarrior
{
    public class LevelCrawlExceptionTest
    {
        [Test]
        public void ShouldSerializeAndDeserialize()
        {
            var ex = new LevelCrawlException("Serialization test");

            var clone = ex.RoundTrip();

            clone.Message.Should().Be(ex.Message);
        }
    }
}
