// =================================================
// <copyright file="WhenMonadicLinkIsMonade.cs">
//     Copyright (c) 2016 seb!
// </copyright>
// <author>seb!</author>
// =================================================

using System;
using FuncPatterns.Functional.ChainOfResponsibility;
using Machine.Specifications;

namespace FuncPatterns.Tests
{
    [Subject("Three monade laws")]
    sealed class WhenMonadBasedOnMonadicLinkIs
    {
        static FoundLinkFake _a;
        static MissedLinkFake _b;

        Establish _context = () =>
        {
            _a = new FoundLinkFake();
            _b = new MissedLinkFake();
        };

        //return a >>= f = f a
        It _shouldSatisfyFirstLawOfMonads = () =>
        {
            Func<MonadicLink<string, int>, Func<MonadicLink<string, int>>> f = MonadicLink.Create;
            var left = MonadicLink.Create(_a).BindTo(f);
            var right = f(_a);

            left().ShouldEqual(right());
        };

        //m >>= return = m
        It _shouldSatisfySecondLawOfMonads = () =>
        {
            var m = MonadicLink.Create(_a);
            var left = m.BindTo(MonadicLink.Create);

            left.ShouldEqual(m);
        };

        //m >>= (\x -> f x >>= g) = (m >>= f) >>= g
        It _shouldSatisfyThirdLawOfMonads = () =>
        {
            var m = MonadicLink.Create(_b);
            Func<MonadicLink<string, int>, Func<MonadicLink<string, int>>> f = x => MonadicLink.Create(_b);
            Func<MonadicLink<string, int>, Func<MonadicLink<string, int>>> g = x => MonadicLink.Create(_a);

            var left = m.BindTo(x => f(x).BindTo(g));
            var right = m.BindTo(f).BindTo(g);

            left().ShouldEqual(right());
        };

        sealed class FoundLinkFake : MonadicLink<string, int>
        {
            protected override int ProcessCore() => 1;
        }

        sealed class MissedLinkFake : MonadicLink<string, int>
        {
            protected override int ProcessCore() => default(int);
        }
    }
}
