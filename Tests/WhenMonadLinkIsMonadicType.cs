// =================================================
// <copyright file="WhenMonadLinkIsMonadicType.cs" company="seb!">
//     Copyright (c) 2016
// </copyright>
// <author>s.mach</author>
// =================================================

using System;
using FuncPatterns.ChainOfResponsibility;
using Machine.Specifications;
using static FuncPatterns.ChainOfResponsibility.MonadicLink<string, int>;

namespace FuncPatterns.Tests
{
    [Subject("Three laws of monads")]
    sealed class WhenMonadLinkIsMonadicType
    {
        //return a >>= f = f a
        It _shouldSatisfyFirstLawOfMonads = () =>
        {
            Func<MonadicLink<string, int>, Func<MonadicLink<string, int>>> f = Unit<FoundLinkFake>;
            var left = Unit<FoundLinkFake>().Bind(f);
            var right = f(new FoundLinkFake());

            left().Process("me").ShouldEqual(right().Process("me"));
        };

        //m >>= return = m
        It _shouldSatisfySecondLawOfMonads = () =>
        {
            var m = Unit<FoundLinkFake>();
            var left = m.Bind(Unit<FoundLinkFake>);

            left().Process("me").ShouldEqual(m().Process("me"));
        };

        //m >>= (\x -> f x >>= g) = (m >>= f) >>= g
        It _shouldSatisfyThirdLawOfMonads = () =>
        {
            var m = Unit<MissedLinkFake>();
            Func<MonadicLink<string, int>, Func<MonadicLink<string, int>>> f = Unit<MissedLinkFake>;
            Func<MonadicLink<string, int>, Func<MonadicLink<string, int>>> g = Unit<FoundLinkFake>;

            var left = m.Bind(x => f(x).Bind(g));
            var right = m.Bind(f).Bind(g);

            left().Process("me").ShouldEqual(right().Process("me"));
        };

        sealed class FoundLinkFake : MonadicLink<string, int>
        {
            protected internal override bool IsApplicableFor(string input) => true;

            protected internal override int Resolve(string input) => input.Length;
        }

        sealed class MissedLinkFake : MonadicLink<string, int>
        {
            protected internal override bool IsApplicableFor(string input) => false;

            protected internal override int Resolve(string input) => 0;
        }
    }
}