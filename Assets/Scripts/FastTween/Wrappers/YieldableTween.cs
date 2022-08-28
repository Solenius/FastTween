using System.Threading;
using UnityEngine;

namespace FastTween
{
    internal sealed class YieldableTween : CustomYieldInstruction
    {
        private readonly CancellationToken m_cancel;
        private readonly TweenController m_tweenController;
        private readonly ITween m_tween;
        private bool m_running = true;

        public override bool keepWaiting
        {
            get
            {
                if (m_running && m_cancel.IsCancellationRequested)
                {
                    m_tweenController.CancelTween(m_tween);
                    return false;
                }
                else
                {
                    return m_running;
                }
            }
        }
        public YieldableTween(ITween tween, TweenController tweenController, UpdateType update, bool realTime, CancellationToken cancel)
        {
            m_cancel = cancel;
            m_tweenController = tweenController;
            m_tween = tween;

            if (!m_cancel.IsCancellationRequested)
            {
                m_cancel.Register(OnRequestTweenCancel);
                tween.SetOnCancel(OnCancel);
                tween.SetOnComplete(OnComplete);
                m_tweenController.AddTween(tween, update, realTime);
            }
            else
            {
                m_running = false;
            }
        }

        private void OnComplete() => m_running = false;
        private void OnCancel() => m_running = false;
        private void OnRequestTweenCancel()
        {
            if (m_running)
            {
                m_tweenController.CancelTween(m_tween);
                m_running = false;
            }
        }
    }
}