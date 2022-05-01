using UnityEngine;

namespace FastTween
{
    internal partial class TweenController : MonoBehaviour
    {
        private void UpdateFloatTweenBuffer(TweenBuffer<FloatTween> tweenBuffer, float deltaTime)
        {
            FloatTween[] buffer = tweenBuffer.Buffer;

            for (int i = 0; i < tweenBuffer.Count; i++)
            {
                Update(ref buffer[i], deltaTime, ref i);
            }

            void Update(ref FloatTween tween, float deltaTime, ref int index)
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

        private void CancelFloatTween(FloatTween floatTween)
        {
            long id = floatTween.id;
            (UpdateType updateType, bool realTime) = GetInfo(id);
            TweenBuffer<FloatTween> tweenBuffer = m_floatTweenBuffers.GetBuffer(updateType, realTime);
            FloatTween[] internalBuffer = tweenBuffer.Buffer;
            for (int i = 0; i < tweenBuffer.Count; i++)
            {
                if (internalBuffer[i].id == id)
                {
                    tweenBuffer.Remove(i);
                    floatTween.OnCancel();
                    break;
                }
            }
        }
    }
}