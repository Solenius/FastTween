using UnityEngine;

namespace FastTween
{
    internal partial class TweenController : MonoBehaviour
    {
        private void UpdatePositionTweenBuffer(TweenBuffer<PositionTween> tweenBuffer, float deltaTime)
        {
            PositionTween[] buffer = tweenBuffer.Buffer;

            for (int i = 0; i < tweenBuffer.Count; i++)
            {
                Update(ref buffer[i], deltaTime, ref i);
            }

            void Update(ref PositionTween tween, float deltaTime, ref int index)
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

        private void CancelPositionTween(PositionTween tween)
        {
            long id = tween.id;
            (UpdateType updateType, bool realTime) = GetInfo(id);
            TweenBuffer<PositionTween> tweenBuffer = m_positionTweenBuffers.GetBuffer(updateType, realTime);
            PositionTween[] internalBuffer = tweenBuffer.Buffer;
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