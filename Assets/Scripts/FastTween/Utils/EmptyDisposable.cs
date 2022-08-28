using System;

namespace FastTween
{
    internal sealed class EmptyDisposable : IDisposable
    {
        public static EmptyDisposable Default { get; } = new EmptyDisposable();
        private EmptyDisposable() { }
        public void Dispose() { }
    }
}