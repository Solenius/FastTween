using System.Runtime.CompilerServices;
using UnityEngine;

namespace FastTween
{
    internal static class EasingMethod
    {
        private const float PI = Mathf.PI;
        private const float HALF_PI = Mathf.PI / 2f;

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float EaseLinear(float value)
        {
            return value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float EaseInSine(float value)
        {
            return 1f - Mathf.Cos(value * HALF_PI);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float EaseOutSine(float value)
        {
            return Mathf.Sin(value * HALF_PI);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float EaseInOutSine(float value)
        {
            return -(Mathf.Cos(PI * value) - 1f) / 2f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float EaseInQuad(float value)
        {
            return value * value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float EaseOutQuad(float value)
        {
            return (2f - value) * value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float EaseInOutQuad(float value)
        {
            return value < 0.5f ? 2f * value * value : 1f - (Mathf.Pow((-2f * value) + 2f, 2f) / 2f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float EaseInCubic(float value)
        {
            return value * value * value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float EaseOutCubic(float value)
        {
            return 1f - Mathf.Pow(1 - value, 3f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float EaseInOutCubic(float value)
        {
            return value < 0.5f ? 4f * value * value * value : 1f - (Mathf.Pow((-2f * value) + 2f, 3f) / 2f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float EaseInQuart(float value)
        {
            return value * value * value * value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float EaseOutQuart(float value)
        {
            return 1f - Mathf.Pow(1f - value, 4f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float EaseInOutQuart(float value)
        {
            return value < 0.5f ? 8f * value * value * value * value : 1f - (Mathf.Pow((-2f * value) + 2f, 4f) / 2f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float EaseInQuint(float value)
        {
            return value * value * value * value * value;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float EaseOutQuint(float value)
        {
            return 1f - Mathf.Pow(1f - value, 5f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float EaseInOutQuint(float value)
        {
            return value < 0.5f ? 16f * value * value * value * value * value : 1f - (Mathf.Pow((-2f * value) + 2f, 5f) / 2f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float EaseInExpo(float value)
        {
            return value == 0f ? 0f : Mathf.Pow(2f, (10f * value) - 10f);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float EaseOutExpo(float value)
        {
            return value == 1f ? 1f : 1f - Mathf.Pow(2f, -10f * value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float EaseInOutExpo(float value)
        {
            if (value == 0f) return 0f;
            if (value == 1f) return 1f;
            return value < 0.5f ? Mathf.Pow(2f, (20f * value) - 10f) / 2f : (2f - Mathf.Pow(2f, (-20f * value) + 10f)) / 2f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float EaseInCirc(float value)
        {
            return 1f - Mathf.Sqrt(1f - Mathf.Pow(value, 2f));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float EaseOutCirc(float value)
        {
            return Mathf.Sqrt(1f - Mathf.Pow(value - 1f, 2f));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float EaseInOutCirc(float value)
        {
            return value < 0.5f
                ? (1f - Mathf.Sqrt(1f - Mathf.Pow(2f * value, 2f))) / 2f
                : (Mathf.Sqrt(1 - Mathf.Pow((-2f * value) + 2f, 2f)) + 1f) / 2f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float EaseInBack(float value)
        {
            const float C1 = 1.70158f;
            const float C3 = C1 + 1f;
            return (C3 * value * value * value) - (C1 * value * value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float EaseOutBack(float value)
        {
            const float C1 = 1.70158f;
            const float C3 = C1 + 1f;
            return 1f + (C3 * Mathf.Pow(value - 1f, 3f)) + (C1 * Mathf.Pow(value - 1f, 2f));
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float EaseInOutBack(float value)
        {
            const float C1 = 1.70158f;
            const float C2 = C1 * 1.525f;
            return value < 0.5f
                ? Mathf.Pow(2f * value, 2f) * (((C2 + 1f) * 2f * value) - C2) / 2f
                : ((Mathf.Pow((2f * value) - 2f, 2f) * (((C2 + 1f) * ((value * 2f) - 2f)) + C2)) + 2f) / 2f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float EaseInElastic(float value)
        {
            const float C4 = 2f * PI / 3f;
            if (value == 0f) return 0f;
            if (value == 1f) return 1f;
            return -Mathf.Pow(2f, (10f * value) - 10f) * Mathf.Sin(((value * 10f) - 10.75f) * C4);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float EaseOutElastic(float value)
        {
            const float C4 = 2f * PI / 3f;
            if (value == 0f) return 0f;
            if (value == 1f) return 1f;
            return (Mathf.Pow(2f, -10f * value) * Mathf.Sin(((value * 10f) - 0.75f) * C4)) + 1f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float EaseInOutElastic(float value)
        {
            const float C5 = 2f * PI / 4.5f;
            if (value == 0f) return 0f;
            if (value == 1f) return 1f;
            return value < 0.5f
                ? -(Mathf.Pow(2f, (20f * value) - 10f) * Mathf.Sin(((20f * value) - 11.125f) * C5)) / 2
                : (Mathf.Pow(2f, (-20f * value) + 10f) * Mathf.Sin(((20f * value) - 11.125f) * C5) / 2) + 1;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float EaseInBounce(float value)
        {
            return 1f - EaseOutBounce(1f - value);
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float EaseOutBounce(float value)
        {
            const float N1 = 7.5625f;
            const float D1 = 2.75f;

            if (value < 1f / D1)
                return N1 * value * value;
            else if (value < 2f / D1)
                return (N1 * (value -= 1.5f / D1) * value) + 0.75f;
            else if (value < 2.5f / D1)
                return (N1 * (value -= 2.25f / D1) * value) + 0.9375f;
            else
                return (N1 * (value -= 2.625f / D1) * value) + 0.984375f;
        }

        [MethodImpl(MethodImplOptions.AggressiveInlining)]
        internal static float EaseInOutBounce(float value)
        {
            return value < 0.5f ? (1f - EaseOutBounce(1f - (2f * value))) / 2f : (1f + EaseOutBounce((2f * value) - 1f)) / 2f;
        }
    }
}
