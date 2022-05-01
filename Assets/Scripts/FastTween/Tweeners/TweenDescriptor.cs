using System;

namespace FastTween
{
    internal struct TweenDescriptor
    {
        public float duration;
        public float time;
        public LoopType loop;
        public EasingType easing;

        public bool IsCompleted()
        {
            switch (loop)
            {
                case LoopType.Loop:
                case LoopType.PingPongLoop:
                    return false;
                default:
                    return time >= duration;
            }
        }

        public float Update(float deltaTime)
        {
            float value;
            switch (loop)
            {
                case LoopType.None:
                {
                    value = time / duration;
                    time += deltaTime;
                    break;
                }
                case LoopType.Loop:
                {
                    value = time / duration;
                    time = (time + deltaTime) % duration;
                    break;
                }
                case LoopType.PingPong:
                {
                    float half = duration * 0.5f;
                    value = (time > half ? duration - ((time - half) * 2f) : time * 2f) / duration;
                    time += deltaTime;
                    break;
                }
                case LoopType.PingPongLoop:
                {
                    float half = duration * 0.5f;
                    value = (time > half ? duration - ((time - half) * 2f) : time * 2f) / duration;
                    time = (time + deltaTime) % duration;
                    break;
                }
                default:
                    throw new InvalidOperationException();
            }

            switch (easing)
            {
                case EasingType.EaseLinear: return EasingMethod.EaseLinear(value);
                case EasingType.EaseInSine: return EasingMethod.EaseInSine(value);
                case EasingType.EaseOutSine: return EasingMethod.EaseOutSine(value);
                case EasingType.EaseInOutSine: return EasingMethod.EaseInOutSine(value);
                case EasingType.EaseInQuad: return EasingMethod.EaseInQuad(value);
                case EasingType.EaseOutQuad: return EasingMethod.EaseOutQuad(value);
                case EasingType.EaseInOutQuad: return EasingMethod.EaseInOutQuad(value);
                case EasingType.EaseInCubic: return EasingMethod.EaseInCubic(value);
                case EasingType.EaseOutCubic: return EasingMethod.EaseOutCubic(value);
                case EasingType.EaseInOutCubic: return EasingMethod.EaseInOutCubic(value);
                case EasingType.EaseInQuart: return EasingMethod.EaseInQuart(value);
                case EasingType.EaseOutQuart: return EasingMethod.EaseOutQuart(value);
                case EasingType.EaseInOutQuart: return EasingMethod.EaseInOutQuart(value);
                case EasingType.EaseInQuint: return EasingMethod.EaseInQuint(value);
                case EasingType.EaseOutQuint: return EasingMethod.EaseOutQuint(value);
                case EasingType.EaseInOutQuint: return EasingMethod.EaseInOutQuint(value);
                case EasingType.EaseInExpo: return EasingMethod.EaseInExpo(value);
                case EasingType.EaseOutExpo: return EasingMethod.EaseOutExpo(value);
                case EasingType.EaseInOutExpo: return EasingMethod.EaseInOutExpo(value);
                case EasingType.EaseInCirc: return EasingMethod.EaseInCirc(value);
                case EasingType.EaseOutCirc: return EasingMethod.EaseOutCirc(value);
                case EasingType.EaseInOutCirc: return EasingMethod.EaseInOutCirc(value);
                case EasingType.EaseInBack: return EasingMethod.EaseInBack(value);
                case EasingType.EaseOutBack: return EasingMethod.EaseOutBack(value);
                case EasingType.EaseInOutBack: return EasingMethod.EaseInOutBack(value);
                case EasingType.EaseInElastic: return EasingMethod.EaseInElastic(value);
                case EasingType.EaseOutElastic: return EasingMethod.EaseOutElastic(value);
                case EasingType.EaseInOutElastic: return EasingMethod.EaseInOutElastic(value);
                case EasingType.EaseInBounce: return EasingMethod.EaseInBounce(value);
                case EasingType.EaseOutBounce: return EasingMethod.EaseOutBounce(value);
                case EasingType.EaseInOutBounce: return EasingMethod.EaseInOutBounce(value);
                default: throw new InvalidOperationException();
            }
        }
    }
}