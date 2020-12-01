using ReactiveUI;
using RxUIExample.Model;

namespace RxUIExample.ViewModel
{
    public class Example2 : ReactiveObject
    {
        public RGBColor ColorA { get; } = new RGBColor();
        public RGBColor ColorB { get; } = new RGBColor();

        public RGBColor Mixed { get; } = new RGBColor();

        public Example2()
        {
            // A와 B의 Red를 더해 반으로 나눈 다음 Mixed의 Red로 설정 (Green, Blue도 동일)
            this.WhenAnyValue(x => x.ColorA.Red, x => x.ColorB.Red, SumHalf)
                .BindTo(this, x => x.Mixed.Red);

            this.WhenAnyValue(x => x.ColorA.Green, x => x.ColorB.Green, SumHalf)
                .BindTo(this, x => x.Mixed.Green);

            this.WhenAnyValue(x => x.ColorA.Blue, x => x.ColorB.Blue, SumHalf)
                .BindTo(this, x => x.Mixed.Blue);
        }
        public static byte SumHalf(byte a, byte b) => (byte)((a + b) / 2);
    }
}
