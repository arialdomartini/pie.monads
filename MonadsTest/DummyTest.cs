using FluentAssertions;
using Xunit;

namespace MonadsTest
{
    public class DummyTest
    {
        [Fact]
        public void should_pass()
        {
            "friends".Should().Be("friends");
        }
    }
}