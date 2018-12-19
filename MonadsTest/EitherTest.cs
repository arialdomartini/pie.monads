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

        [Fact]
        public void should_match_on_the_right_with_actions()
        {
            Either<int, string> sut = Right("some value");

            var invoked = false;

            var result = sut.Match(l => { invoked = false; }, r => { invoked = true; });

            invoked.Should().Be(true);
            result.Should().Be(unit);
        }

        [Fact]
        public void should_match_on_the_left_with_actions()
        {
            Either<int, string> sut = Left(180);

            var invoked = false;

            var result = sut.Match(l => { invoked = true; }, r => { invoked = false; });

            invoked.Should().Be(true);
            result.Should().Be(unit);
        }

        [Fact]
        public void should_be_converted_to_string_providing_information_on_the_right_value()
        {
            Either<int, string> sut = Right("some value");

            var result = sut.ToString();

            result.Should().Contain("some value");
        }

        [Fact]
        public void should_be_converted_to_string_providing_information_on_the_left_value()
        {
            Either<int, string> sut = Left(100);

            var result = sut.ToString();

            result.Should().Contain("100");
        }

        [Fact]
        public void Right_should_convert_to_string_referencing_its_value()
        {
            var sut = Right("some value");

            var result = sut.ToString();

            result.Should().Be("Right(some value)");
        }

        [Fact]
        public void Right_should_convert_to_string_delegating_conversion_to_its_value()
        {
            var sut = Right(new Foo());

            var result = sut.ToString();

            result.Should().Be("Right(MonadsTest.Foo)");
        }

        [Fact]
        public void Left_should_convert_to_string_referencing_its_value()
        {
            var sut = Left("some value");

            var result = sut.ToString();

            result.Should().Be("Left(some value)");
        }

        [Fact]
        public void Left_should_convert_to_string_delegating_conversion_to_its_value()
        {
            var sut = Left(new Foo());

            var result = sut.ToString();

            result.Should().Be("Left(MonadsTest.Foo)");
        }
    }

    public class Foo
    {
    }
}