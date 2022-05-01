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
        }

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