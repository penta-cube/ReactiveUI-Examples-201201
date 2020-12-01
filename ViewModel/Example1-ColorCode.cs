using System;
using System.Reactive.Linq;
using System.Threading;
using ReactiveUI;

namespace RxUIExample.ViewModel
{
    public class Example1 : ReactiveObject
    {
        private readonly ObservableAsPropertyHelper<string> _code;

        private byte _red;
        private byte _green;
        private byte _blue;

        public byte Red
        {
            get => _red;
            set => this.RaiseAndSetIfChanged(ref _red, value);
        }

        public byte Green
        {
            get => _green;
            set => this.RaiseAndSetIfChanged(ref _green, value);
        }

        public byte Blue
        {
            get => _blue;
            set => this.RaiseAndSetIfChanged(ref _blue, value);
        }

        public string Code => _code.Value;

        public Example1()
        {
            this.WhenAnyValue(x => x.Red, x => x.Green, x => x.Blue)
                // Red, Green 혹은 Blue가 바뀌고 150ms마다 실행
                .Throttle(TimeSpan.FromMilliseconds(150))
                // Background Thread에서 실행한다.
                .ObserveOn(RxApp.TaskpoolScheduler)
                // RGB Code를 생성하고 (1s, in Background)
                .Select(t => FormatCode(t.Item1, t.Item2, t.Item3))
                // Main Thread에서 Property를 변경한다.
                .ToProperty(this, x => x.Code, out _code, scheduler: RxApp.MainThreadScheduler);
        }

        public static string FormatCode(byte r, byte g, byte b)
        {
            // 처리에 1초가 걸리는 Heavy Function
            Thread.Sleep(1000);
            return $"#{r:X2}{g:X2}{b:X2}";
        }
    }
}
