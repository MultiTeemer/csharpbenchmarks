using BenchmarkDotNet.Attributes;
using BenchmarkDotNet.Attributes.Jobs;

namespace Benchmarks.Benchmarks
{
    [LegacyJitX86Job, LegacyJitX64Job, RyuJitX64Job]
    public class ReadonlyStructVsStruct
    {
        private struct Pad
        {
            public readonly long z;
            public readonly long zz;
            public readonly long zzz;
            public readonly long zzzz;
            public readonly long zzzzz;
            public readonly long zzzzzz;
            public readonly long zzzzzzz;
            public readonly long zzzzzzzz;
            public readonly long zzzzzzzzz;
            public readonly long zzzzzzzzzz;
            public readonly long zzzzzzzzzzz;
            public readonly long zzzzzzzzzzzz;
            public readonly long zzzzzzzzzzzzz;
            public readonly long zzzzzzzzzzzzzz;
        }
        
        private struct SomeData
        {
            public readonly long X;
            public readonly long Y;
            public readonly Pad p1;
            public readonly Pad p2;
            public readonly Pad p3;
            public readonly Pad p4;
            public readonly Pad p5;
            public readonly Pad p6;
            public readonly Pad p7;
            
            public SomeData(long x, long y)
            {
                X = x;
                Y = y;
                p1 = new Pad();
                p2 = new Pad();
                p3 = new Pad();
                p4 = new Pad();
                p5 = new Pad();
                p6 = new Pad();
                p7 = new Pad();
            }
        }
        
        private readonly SomeData d1 = new SomeData(1, 2);
        private SomeData d2 = new SomeData(3, 4);

        [Benchmark]
        public long ReadonlyAccess()
        {
            return d1.X + d1.Y + d1.p2.zzzzzzzzzzzzzz + d1.p7.zz + d1.p3.zzzzzz + d1.p5.zzzzzzzzzzzzzz;
        }

        [Benchmark(Baseline = true)]
        public long Access()
        {
            return d2.X + d2.Y + d2.p2.zzzzzzzzzzzzzz + d2.p7.zz + d2.p3.zzzzzz + d2.p5.zzzzzzzzzzzzzz;
        }
    }
}