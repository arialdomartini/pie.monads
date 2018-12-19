using System;
using System.Collections.Generic;
using FluentAssertions;
using Monads;
using static Monads.Functional;
using Xunit;

namespace MonadsTest
{
    public class ActionTest
    {
        [Fact]
        public void should_convert_action_with_1_parameter_to_functions()
        {
            var invokedWith = "not invoked";

            var action = new Action<string>(s => { invokedWith = s; });

            var function = action.ToFunction();

            var result = function("some value");

            invokedWith.Should().Be("some value");
            result.Should().Be(unit);
        }

        [Fact]
        public void should_convert_action_with_2_parameters_to_functions()
        {
            var arguments = new List<string>();

            var action = new Action<string, string>((s1, s2) => { arguments = new List<string> {s1, s2}; });

            var function = action.ToFunction();

            var result = function(
                "value 1",
                "value 2");

            arguments.Should().BeEquivalentTo(new List<string>
            {
                "value 1",
                "value 2"
            });
            result.Should().Be(unit);
        }
        
        [Fact]
        public void should_convert_action_with_3_parameters_to_functions()
        {
            var arguments = new List<string>();

            var action = new Action<string, string, string>((s1, s2, s3) => { arguments = new List<string> {s1, s2, s3}; });

            var function = action.ToFunction();

            var result = function(
                "value 1",
                "value 2",
                "value 3");

            arguments.Should().BeEquivalentTo(new List<string>
            {
                "value 1",
                "value 2",
                "value 3"
            });
            result.Should().Be(unit);
        }

        [Fact]
        public void should_convert_action_with_4_parameters_to_functions()
        {
            var arguments = new List<string>();

            var action = new Action<string, string, string, string>((s1, s2, s3, s4) => { arguments = new List<string> {s1, s2, s3, s4}; });

            var function = action.ToFunction();

            var result = function(
                "value 1",
                "value 2",
                "value 3",
                "value 4");

            arguments.Should().BeEquivalentTo(new List<string>
            {
                "value 1",
                "value 2",
                "value 3",
                "value 4"
            });
            result.Should().Be(unit);
        }
    }
}