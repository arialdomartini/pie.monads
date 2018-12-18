using FluentAssertions;
using Xunit;
using static Monads.Unit;
using Unit = Monads.Unit;

// ReSharper disable ArrangeStaticMemberQualifier
namespace MonadsTest
{
    public class UnitTest
    {
        [Fact]
        public void there_should_be_only_one_instance_of_unit()
        {
            var a = Unit.Build();
            var b = Unit.Build();

            a.Should().Be(b);
        }

        [Fact]
        public void it_should_possible_to_create_a_Unit_with_one_single_word()
        {
            var a = unit;
            var b = Unit.Build();

            a.Should().Be(b);
        }
    }
}