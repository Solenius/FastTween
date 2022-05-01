using System;
using UnityEngine;

namespace FastTween
{
    internal struct ScaleTween : ITween
    {
        public TweenDescriptor descriptor;
        public Action onStart;
        public Action onComplete;
        public Action onCancel;
        public Vector3 from;
        public Vector3 to;
        public Transform target;
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
            this.onCancel = onCancel;
        }

        void ITween.SetOnComplete(Action onComplete)
        {
            this.onComplete = onComplete;
        }

        void ITween.SetOnStart(Action onStart)
        {
            this.onStart = onStart;
        }

        IDisposable ITween.ToDisposable()
        {
            return new DisposableTween(this);
        }

        public void OnUpdate(float deltaTime)
        {
            float value = descriptor.Update(deltaTime);
            target.localScale = (value * (to - from)) + from;
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
                case LoopType.None: target.localScale = to; break;
                case LoopType.PingPong: target.localScale = from; break;
            }

            onComplete?.Invoke();
            ClearCallbacks();
        }

        private void ClearCallbacks()
        {
            onStart = null;
            onCancel = null;
            onComplete = null;
        }
    }
}