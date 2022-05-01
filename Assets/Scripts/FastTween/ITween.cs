using System;

namespace FastTween
{
    public interface ITween
    {
        internal long Id { get; set; }
        internal void SetOnStart(Action onStart);
        internal void SetOnComplete(Action onComplete);
        internal void SetOnCancel(Action onCancel);
        internal void SetLoop(LoopType loop);
        internal void SetEasingMethod(EasingType easing);
        public IDisposable ToDisposable();
    }

    public interface ITween<T> : ITween
    {
        internal void SetOnUpdate(Action<T> onUpdate);
    }
}