using System;

namespace FastTween
{
    internal struct FloatTween : ITween<float>
    {
        public TweenDescriptor descriptor;
        public Action onStart;
        public Action onComplete;
        public Action onCancel;
        public Action<float> onUpdate;
        public float from;
        public float to;
        public long id;

        long ITween.Id
        {
            get => id;
            set => id = value;
        }

        void ITween.SetEasingMethod(EasingType easing)
        {
            descriptor.easing = easing;
        }

        void ITween.SetLoop(LoopType loop)
        {
            descriptor.loop = loop;
        }

        void ITween.SetOnCancel(Action onCancel)
        {
            this.onCancel += onCancel;
        }

        void ITween.SetOnComplete(Action onComplete)
        {
            this.onComplete += onComplete;
        }

        void ITween.SetOnStart(Action onStart)
        {
            this.onStart += onStart;
        }

        void ITween<float>.SetOnUpdate(Action<float> onUpdate)
        {
            this.onUpdate += onUpdate;
        }

        public void OnUpdate(float deltaTime)
        {
            float value = descriptor.Update(deltaTime);
            value = (value * (to - from)) + from;
            onUpdate?.Invoke(value);
        }

        public void OnCancel()
        {
            onCancel?.Invoke();
            ClearCallbacks();
        }

        public void OnComplete()
        {
            switch (descriptor.loop)
            {
                case LoopType.None: onUpdate?.Invoke(to); break;
                case LoopType.PingPong: onUpdate?.Invoke(from); break;
            }

            onComplete?.Invoke();
            ClearCallbacks();
        }

        private void ClearCallbacks()
        {
            onStart = null;
            onCancel = null;
            onComplete = null;
            onUpdate = null;
        }
    }
}