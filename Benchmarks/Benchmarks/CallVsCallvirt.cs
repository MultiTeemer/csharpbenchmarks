using System.Runtime.CompilerServices;
using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Jobs;
using BenchmarkDotNet.Order;

namespace Benchmarks.Benchmarks
{
    [OrderProvider(SummaryOrderPolicy.FastestToSlowest)]
    public class CallVsCallvirt
    {
        private class Dumb
        {
            [MethodImpl(MethodImplOptions.NoInlining)]
            public static int StaticMethod()
            {
                return 1;
            }

            [MethodImpl(MethodImplOptions.NoInlining)]
            public int NonVirtualMethod()
            {
                return 1;
            }

            [MethodImpl(MethodImplOptions.NoInlining)]
            public virtual int VirtualMethod()
            {
                return 1;
            }
        }
        
        [Benchmark(Baseline = true)]
        public int StaticCall()
        {
            return Dumb.StaticMethod();
        }

        [Benchmark]
        public int CallNonVirtualMethod()
        {
            return new Dumb().NonVirtualMethod();
        }

        [Benchmark]
        public int CallVirtNonVirtualMethod()
        {
            var dumb = new Dumb();
            return dumb.NonVirtualMethod();
        }

        [Benchmark]
        public int CallVirtVirtualMethod()
        {
            var dumb = new Dumb();
            return dumb.VirtualMethod();
        }
    }
}