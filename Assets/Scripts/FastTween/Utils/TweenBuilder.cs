using System;

namespace FastTween
{
    public class TweenBuilder
    {
        private ITween m_target;

        internal void FromTween(ITween target)
        {
            m_target = target;
        }

        public TweenBuilder SetOnStart(Action onStart)
        {
            m_target.SetOnStart(onStart);
            return this;
        }

        public TweenBuilder SetOnComplete(Action onComplete)
        {
            m_target.SetOnComplete(onComplete);
            return this;
        }

        public TweenBuilder SetOnCancel(Action onCancel)
        {
            m_target.SetOnCancel(onCancel);
            return this;
        }

        public TweenBuilder SetLoop(LoopType loop)
        {
            m_target.SetLoop(loop);
            return this;
        }

        public TweenBuilder SetEasing(EasingType easing)
        {
            m_target.SetEasingMethod(easing);
            return this;
        }

        public ITween Create()
        {
            return m_target;
        }

        public ITween CreateAndRun(UpdateType update = UpdateType.Update, bool realTime = false)
        {
            TweenController.Instance.AddTween(m_target, update, realTime);
            return m_target;
        }
    }

    public class TweenBuilder<T>
    {
        private ITween<T> m_target;

        internal void FromTween(ITween<T> target)
        {
            m_target = target;
        }

        public TweenBuilder<T> SetOnStart(Action onStart)
        {
            m_target.SetOnStart(onStart);
            return this;
        }

        public TweenBuilder<T> SetOnComplete(Action onComplete)
        {
            m_target.SetOnComplete(onComplete);
            return this;
        }

        public TweenBuilder<T> SetOnCancel(Action onCancel)
        {
            m_target.SetOnCancel(onCancel);
            return this;
        }

        public TweenBuilder<T> SetOnUpdate(Action<T> onUpdate)
        {
            m_target.SetOnUpdate(onUpdate);
            return this;
        }

        public TweenBuilder<T> SetLoop(LoopType loop)
        {
            m_target.SetLoop(loop);
            return this;
        }

        public TweenBuilder<T> SetEasing(EasingType easing)
        {
            m_target.SetEasingMethod(easing);
            return this;
        }

        public ITween<T> Create()
        {
            return m_target;
        }

        public ITween<T> CreateAndRun(UpdateType update = UpdateType.Update, bool realTime = false)
        {
            TweenController.Instance.AddTween(m_target, update, realTime);
            return m_target;
        }
    }
}