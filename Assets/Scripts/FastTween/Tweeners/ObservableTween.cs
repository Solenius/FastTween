using System;
using System.Collections.Generic;

namespace FastTween
{
    internal sealed class ObservableTween<T> : IObservable<T>
    {
        private readonly List<IObserver<T>> m_observers = new List<IObserver<T>>();
        private readonly ITween<T> m_sourceTween;
        private readonly UpdateType m_updateType;
        private readonly bool m_realTime;
        private readonly bool m_cancelOnUnsubscribe;
        private bool m_started;
        private bool m_finished;

        public ObservableTween(ITween<T> sourceTween, UpdateType updateType, bool realTime, bool cancelOnUnsubscribe)
        {
            m_sourceTween = sourceTween;
            m_updateType = updateType;
            m_realTime = realTime;
            m_cancelOnUnsubscribe = cancelOnUnsubscribe;

            sourceTween.SetOnUpdate(OnUpdate);
            sourceTween.SetOnComplete(OnComplete);
            sourceTween.SetOnCancel(OnComplete);
        }

        public IDisposable Subscribe(IObserver<T> observer)
        {
            if (m_finished)
            {
                observer.OnCompleted();
                return EmptyDisposable.Default;
            }

            m_observers.Add(observer);
            Unsubscriber unsubscriber = new Unsubscriber(this, observer);

            if (!m_started)
            {
                TweenController.Instance.AddTween(m_sourceTween, m_updateType, m_realTime);
                m_started = true;
            }

            return unsubscriber;
        }

        private void OnUpdate(T value)
        {
            for (int i = 0; i < m_observers.Count; i++)
                m_observers[i].OnNext(value);
        }

        private void OnComplete()
        {
            m_finished = true;

            IObserver<T>[] observers = new IObserver<T>[m_observers.Count];
            for (int i = 0; i < m_observers.Count; i++)
                observers[i] = m_observers[i];

            for (int i = 0; i < observers.Length; i++)
                observers[i].OnCompleted();

            m_observers.Clear();
        }

        private class Unsubscriber : IDisposable
        {
            private readonly ObservableTween<T> m_source;
            private readonly IObserver<T> m_observer;

            public Unsubscriber(ObservableTween<T> source, IObserver<T> observer)
            {
                m_source = source;
                m_observer = observer;
            }

            public void Dispose()
            {
                if (!m_source.m_observers.Remove(m_observer))
                    return;

                if (!m_source.m_cancelOnUnsubscribe || m_source.m_finished)
                    return;

                if (m_source.m_observers.Count != 0)
                    return;

                if (TweenController.HasInstance)
                    TweenController.Instance.CancelTween(m_source.m_sourceTween);
            }
        }
    }

    internal sealed class ObservableTween : IObservable<bool>
    {
        private readonly List<IObserver<bool>> m_observers = new List<IObserver<bool>>();
        private readonly ITween m_sourceTween;
        private readonly UpdateType m_updateType;
        private readonly bool m_realTime;
        private readonly bool m_cancelOnUnsubscribe;
        private bool m_started;
        private bool m_finished;

        public ObservableTween(ITween sourceTween, UpdateType updateType, bool realTime, bool cancelOnUnsubscribe)
        {
            m_sourceTween = sourceTween;
            m_updateType = updateType;
            m_realTime = realTime;
            m_cancelOnUnsubscribe = cancelOnUnsubscribe;

            sourceTween.SetOnComplete(OnComplete);
            sourceTween.SetOnCancel(OnComplete);
        }

        public IDisposable Subscribe(IObserver<bool> observer)
        {
            if (m_finished)
            {
                observer.OnCompleted();
                return EmptyDisposable.Default;
            }

            m_observers.Add(observer);
            Unsubscriber unsubscriber = new Unsubscriber(this, observer);

            if (!m_started)
            {
                TweenController.Instance.AddTween(m_sourceTween, m_updateType, m_realTime);
                m_started = true;
            }

            return unsubscriber;
        }

        private void OnComplete()
        {
            m_finished = true;

            IObserver<bool>[] observers = new IObserver<bool>[m_observers.Count];
            for (int i = 0; i < m_observers.Count; i++)
                observers[i] = m_observers[i];

            for (int i = 0; i < observers.Length; i++)
                observers[i].OnCompleted();

            m_observers.Clear();
        }

        private class Unsubscriber : IDisposable
        {
            private readonly ObservableTween m_source;
            private readonly IObserver<bool> m_observer;

            public Unsubscriber(ObservableTween source, IObserver<bool> observer)
            {
                m_source = source;
                m_observer = observer;
            }

            public void Dispose()
            {
                if (!m_source.m_observers.Remove(m_observer))
                    return;

                if (!m_source.m_cancelOnUnsubscribe || m_source.m_finished)
                    return;

                if (m_source.m_observers.Count != 0)
                    return;

                if (TweenController.HasInstance)
                    TweenController.Instance.CancelTween(m_source.m_sourceTween);
            }
        }
    }
}