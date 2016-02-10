// =================================================
// <copyright file="MonadicLink.cs">
//     Copyright (c) 2016 seb!
// </copyright>
// <author>seb!</author>
// =================================================

using System;

namespace FuncPatterns.Functional.ChainOfResponsibility
{
    public static class MonadicLink
    {
        public static Func<MonadicLink<TInput, TOutput>> Create<TInput, TOutput>(
            MonadicLink<TInput, TOutput> previous)

            => () => previous;

        public static Func<MonadicLink<TInput, TOutput>> BindTo<TInput, TOutput>(
            this Func<MonadicLink<TInput, TOutput>> current,
            Func<MonadicLink<TInput, TOutput>, Func<MonadicLink<TInput, TOutput>>> successor)

            => Equals(current().Process(), default(TOutput)) ? successor(current()) : current;
    }

    public abstract class MonadicLink<TInput, TOutput>
    {
        public TInput Input { get; internal set; }

        public TOutput Process() => ProcessCore();

        protected abstract TOutput ProcessCore();
    }
}