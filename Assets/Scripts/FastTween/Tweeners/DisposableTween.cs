using System;

namespace FastTween
{
    internal sealed class DisposableTween : IDisposable
    {
        private readonly ITween m_tween;
        private bool m_disposed;

        public DisposableTween(ITween tween)
        {
            m_tween = tween ?? throw new ArgumentNullException(nameof(tween));
            m_tween.SetOnComplete(OnTweenCompleted);
            m_tween.SetOnCancel(OnTweenCanceled);
        }

        private void OnTweenCompleted() => m_disposed = true;
        private void OnTweenCanceled() => m_disposed = true;

        public void Dispose()
        {
            if (m_disposed)
                return;

            m_disposed = true;

            if (TweenController.HasInstance)
                TweenController.Instance.CancelTween(m_tween);
        }
    }
}