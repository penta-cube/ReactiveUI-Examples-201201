using System;
using System.Reactive;
using System.Reactive.Linq;
using System.Threading.Tasks;
using System.Windows;
using ReactiveUI;

namespace RxUIExample.ViewModel
{
    public class Example3 : ReactiveObject
    {
        private static readonly Random _rnd = new Random(DateTime.Now.Millisecond);

        private readonly ObservableAsPropertyHelper<bool> _commandExecuting;
        private int _myValue;

        public int MyValue
        {
            get => _myValue;
            set => this.RaiseAndSetIfChanged(ref _myValue, value);
        }

        public ReactiveCommand<Unit, string> MyCommand { get; }
        public ReactiveCommand<string, Unit> MyCommand2 { get; }

        public bool CommandExecuting => _commandExecuting.Value;

        public Example3()
        {
            // 100 이상이면 자동으로 true로 바뀜
            var canExecuteCommand = this.WhenAnyValue(x => x.MyValue).Select(v => v >= 100);

            MyCommand = ReactiveCommand.CreateFromTask(MyCommandAction, canExecuteCommand);
            // MyCommand가 실행중일 때 CommandExecuting 프로퍼티를 true로 설정한다.
            MyCommand.IsExecuting.ToProperty(this, x => x.CommandExecuting, out _commandExecuting);

            MyCommand2 = ReactiveCommand.Create<string>(ShowMessage);

            // MyCommand가 실행되고 반환된 값을 MyCommand2에 넣어서 실행해준다.
            MyCommand.InvokeCommand(MyCommand2);
        }

        public async Task<string> MyCommandAction()
        {
            await Task.Delay(3000);
            return $"Random Number ({_rnd.Next()})";
        }

        public void ShowMessage(string msg)
        {
            MessageBox.Show($"Message: {msg ?? string.Empty}");
        }
    }
}
