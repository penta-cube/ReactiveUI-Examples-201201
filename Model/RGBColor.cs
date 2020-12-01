using ReactiveUI;

namespace RxUIExample.Model
{
    public class RGBColor : ReactiveObject
    {
        private byte red;
        private byte green;
        private byte blue;

        public byte Red { get => red; set => this.RaiseAndSetIfChanged(ref red, value); }
        public byte Green { get => green; set => this.RaiseAndSetIfChanged(ref green, value); }
        public byte Blue { get => blue; set => this.RaiseAndSetIfChanged(ref blue, value); }
    }
}
