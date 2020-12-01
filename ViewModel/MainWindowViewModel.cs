using System;
using System.Linq.Expressions;
using ReactiveUI;

namespace RxUIExample.ViewModel
{
    public class MainWindowViewModel : ReactiveObject
    {
        /// <summary>
        /// 오래 걸리는 코드를 Throttling하고, Background 스레드에서 동작하는 예제
        /// </summary>
        public Example1 ColorCode { get; } = new Example1();
        /// <summary>
        /// Nested Property를 참조해 일종의 중재자 패턴을 구현한 예제
        /// </summary>
        public Example2 MixColor { get; } = new Example2();
        /// <summary>
        /// ReactiveCommand로 다양한 처리를 하는 예제
        /// </summary>
        public Example3 Command { get; } = new Example3();
    }
}