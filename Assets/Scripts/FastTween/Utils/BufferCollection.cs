using System;

namespace FastTween
{
    internal class BufferCollection<T>
    {
        public readonly TweenBuffer<T> scaledUpdateBuffer = new TweenBuffer<T>(0, 1024);
        public readonly TweenBuffer<T> unscaledUpdateBuffer = new TweenBuffer<T>(0, 1024);
        public readonly TweenBuffer<T> scaledLateUpdateBuffer = new TweenBuffer<T>(0, 1024);
        public readonly TweenBuffer<T> unscaledLateUpdateBuffer = new TweenBuffer<T>(0, 1024);
        public readonly TweenBuffer<T> fixedUpdateBuffer = new TweenBuffer<T>(0, 1024);

        public void AddTween(ref T tween, UpdateType update, bool realTime)
        {
            GetBuffer(update, realTime).Add(tween);
        }

        public TweenBuffer<T> GetBuffer(UpdateType update, bool realTime)
        {
            switch (update)
            {
                case UpdateType.Update: return realTime ? unscaledUpdateBuffer : scaledUpdateBuffer;
                case UpdateType.LateUpdate:  return realTime ? unscaledLateUpdateBuffer : scaledLateUpdateBuffer;
                case UpdateType.FixedUpdate: return fixedUpdateBuffer;
                default: throw new ArgumentOutOfRangeException(nameof(update));
            }
        }
    }
}