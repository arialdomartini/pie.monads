using FluentAssertions;
using Pie.Monads;
using Xunit;
using static Pie.Monads.Functional;

namespace Pie.MonadsTest
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

            result.Should().Be("Right(Pie.MonadsTest.Foo)");
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

            result.Should().Be("Left(Pie.MonadsTest.Foo)");
        }

        [Fact]
        public void mapping_on_the_right_should_apply_the_function()
        {
            Either<Foo, int> sut = Right(10);

            var result = sut.Map(v => v * 2)
                .Match(l => -1, r => r);

            result.Should().Be(20);
        }

        [Fact]
        public void mapping_on_left_values_should_result_in_left()
        {
            Either<int, int> sut = Left(10);

            var result = sut.Map(v => v * 2)
                .Match(l => -1, r => r);

            result.Should().Be(-1);
        }

        [Fact]
        public void should_bind_on_right_values()
        {
            Either<string, int> sut = Right(10);

            Either<string, int> Double(int i)
            {
                return Right(i * 2);
            }

            var result = sut.Bind(Double);

            var match = result.Match(l => -1, r => r);

            match.Should().Be(20);
        }

        [Fact]
        public void should_bind_on_right_values_on_failing_functions()
        {
            Either<string, int> sut = Right(10);

            Either<string, int> Double(int i)
            {
                return Left("some error");
            }

            var result = sut.Bind(Double);

            var match = result.Match(l => l, r => "should not happen");

            match.Should().Be("some error");
        }

        [Fact]
        public void should_bind_on_left_values()
        {
            Either<string, int> sut = Left("some error");

            Either<string, int> Double(int i)
            {
                return Right(i * 2);
            }

            var result = sut.Bind(Double);

            var match = result.Match(l => l, r => "should not happen");

            match.Should().Be("some error");
        }
    }

    public class Foo
    {
    }
}