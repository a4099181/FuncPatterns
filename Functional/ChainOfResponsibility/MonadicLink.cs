using System;

namespace FuncPatterns.Functional.ChainOfResponsibility
{
    public static class MonadicLink
    {
        public static Func<MonadicLink<TInput, TOutput>> BindTo<TInput, TOutput>(
            this Func<MonadicLink<TInput, TOutput>> current,
            Func<MonadicLink<TInput, TOutput>, Func<MonadicLink<TInput, TOutput>>> successor)
        {
            var link = current();

            if (link is ChainResult<TInput, TOutput>) return () => link;

            var result = link.Process();

            if (Equals(result, default(TOutput))) return successor(link);

            return () => new ChainResult<TInput, TOutput>(result);
        }

        sealed class ChainResult<TInput, TOutput> : MonadicLink<TInput, TOutput>
        {
            readonly TOutput _result;

            internal ChainResult(TOutput result)
            {
                _result = result;
            }

            public override TOutput Process() => _result;
        }
    }

    public abstract class MonadicLink<TInput, TOutput>
    {
        protected internal TInput Input { get; private set; }

        public abstract TOutput Process();

        public static Func<MonadicLink<TInput, TOutput>> Create<TChainLink>(TInput input)
            where TChainLink : MonadicLink<TInput, TOutput>, new()
            => () => new TChainLink { Input = input };

        public static Func<MonadicLink<TInput, TOutput>> Create<TChainLink>(
            MonadicLink<TInput, TOutput> previous)
            where TChainLink : MonadicLink<TInput, TOutput>, new()
            => () => new TChainLink { Input = previous.Input };
    }
}
