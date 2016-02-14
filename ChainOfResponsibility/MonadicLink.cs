// =================================================
// <copyright file="MonadicLink.cs" company="seb!">
//     Copyright (c) 2016
// </copyright>
// <author>s.mach</author>
// =================================================

using System;

namespace FuncPatterns.ChainOfResponsibility
{
    public static class MonadicLink
    {
        public static Func<MonadicLink<TInput, TOutput>> Bind<TInput, TOutput>(
            this Func<MonadicLink<TInput, TOutput>> link,
            Func<MonadicLink<TInput, TOutput>, Func<MonadicLink<TInput, TOutput>>> bindTo)
            => () => bindTo(link())();
    }

    public abstract class MonadicLink<TInput, TOutput>
    {
        MonadicLink<TInput, TOutput> _successor;

        public TOutput Process(TInput input)
        {
            return IsApplicableFor(input) ? Resolve(input) : _successor.Process(input);
        }

        public static Func<MonadicLink<TInput, TOutput>> Unit<TChainLink>()
            where TChainLink : MonadicLink<TInput, TOutput>, new()
            => () => new TChainLink();

        public static Func<MonadicLink<TInput, TOutput>> Unit<TChainLink>(
            MonadicLink<TInput, TOutput> successor)
            where TChainLink : MonadicLink<TInput, TOutput>, new()
            => () => new TChainLink {_successor = successor};

        protected internal abstract bool IsApplicableFor(TInput input);

        protected internal abstract TOutput Resolve(TInput input);
    }
}