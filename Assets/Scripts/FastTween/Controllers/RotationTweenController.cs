using UnityEngine;

namespace FastTween
{
    internal partial class TweenController : MonoBehaviour
    {
        private void UpdateRotationTweenBuffer(TweenBuffer<RotationTween> tweenBuffer, float deltaTime)
        {
            RotationTween[] buffer = tweenBuffer.Buffer;

            for (int i = 0; i < tweenBuffer.Count; i++)
            {
                Update(ref buffer[i], deltaTime, ref i);
            }

            void Update(ref RotationTween tween, float deltaTime, ref int index)
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

        private void CancelRotationTween(RotationTween tween)
        {
            long id = tween.id;
            (UpdateType updateType, bool realTime) = GetInfo(id);
            TweenBuffer<RotationTween> tweenBuffer = m_rotationTweenBuffers.GetBuffer(updateType, realTime);
            RotationTween[] internalBuffer = tweenBuffer.Buffer;
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