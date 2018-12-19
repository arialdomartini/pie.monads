using FluentAssertions;
using Monads;
using Xunit;
using static Monads.Functional;

namespace MonadsTest
{
    public class EitherTest
    {
        [Fact]
        public void should_implicitly_convert_from_a_right_value()
        {
            Either<int, string> sut = "some value";

            var result = sut.Match(l => null, r => r);

            result.Should().Be("some value");
        }

        [Fact]
        public void should_implicitly_convert_from_a_left_value()
        {
            Either<int, string> sut = 400;

            var result = sut.Match(l => l, r => -1);

            result.Should().Be(400);
        }

        [Fact]
        public void should_implicitly_convert_from_Right()
        {
            Either<int, string> sut = Right("some value");

            var result = sut.Match(l => null, r => r);

            result.Should().Be("some value");
        }

        [Fact]
        public void should_implicitly_convert_from_Left()
        {
            Either<int, string> sut = Left(180);

            var result = sut.Match(l => l, r => -1);

            result.Should().Be(180);
        }
    }
}