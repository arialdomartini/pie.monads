using FluentAssertions;
using Monads;
using Xunit;
using static Monads.Either<int, string>;

namespace MonadsTest
{
    public class EitherTest
    {
        [Fact]
        public void should_match_on_right()
        {
            var sut = Right("some value");

            var result = sut.Match(l => "left", r => "right");

            result.Should().Be("right");
        }

        [Fact]
        public void should_match_on_left()
        {
            var sut = Left(12);

            var result = sut.Match(l => -1, r => 1);

            result.Should().Be(-1);
        }
                     
        [Fact]
        public void should_implicitly_convert_from_right()
        {
            Either<int, string> sut = "some value";

            var result = sut.Match(l => null, r => r);

            result.Should().Be("some value");
        }

        [Fact]
        public void should_implicitly_convert_from_left()
        {
            Either<int, string> sut = 400;

            var result = sut.Match(l => l, r => -1);

            result.Should().Be(400);
        }
    }
}