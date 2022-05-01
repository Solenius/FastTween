using UnityEngine;

namespace FastTween
{
    internal partial class TweenController : MonoBehaviour
    {
        private void UpdateScaleTweenBuffer(TweenBuffer<ScaleTween> tweenBuffer, float deltaTime)
        {
            ScaleTween[] buffer = tweenBuffer.Buffer;

            for (int i = 0; i < tweenBuffer.Count; i++)
            {
                Update(ref buffer[i], deltaTime, ref i);
            }

            void Update(ref ScaleTween tween, float deltaTime, ref int index)
            {
                if (tween.descriptor.IsCompleted())
                {
                    tween.OnComplete();
                    tweenBuffer.Remove(index);
                    index--;
                }
                else
                {
                    tween.OnUpdate(deltaTime);
                }
            }
        }

        private void CancelScaleTween(ScaleTween tween)
        {
            long id = tween.id;
            (UpdateType updateType, bool realTime) = GetInfo(id);
            TweenBuffer<ScaleTween> tweenBuffer = m_scaleTweenBuffers.GetBuffer(updateType, realTime);
            ScaleTween[] internalBuffer = tweenBuffer.Buffer;
            for (int i = 0; i < tweenBuffer.Count; i++)
            {
                if (internalBuffer[i].id == id)
                {
                    tweenBuffer.Remove(i);
                    tween.OnCancel();
                    break;
                }
            }
        }
    }
}