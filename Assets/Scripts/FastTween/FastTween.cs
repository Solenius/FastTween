using UnityEngine;

namespace FastTween
{
    public static class Tween
    {
        private static TweenBuilder<float> s_valueTweenBuilder = new TweenBuilder<float>();
        private static TweenBuilder s_tweenBuilder = new TweenBuilder();

        public static TweenBuilder<float> CreateValueTween(float from, float to, float duration)
        {
            TweenDescriptor descriptor = new TweenDescriptor() { duration = duration };
            s_valueTweenBuilder.FromTween(new FloatTween() { descriptor = descriptor, from = from, to = to });
            return s_valueTweenBuilder;
        }

        public static TweenBuilder CreateRotationTween(Transform target, Quaternion from, Quaternion to, float duration)
        {
            TweenDescriptor descriptor = new TweenDescriptor() { duration = duration };
            s_tweenBuilder.FromTween(new RotationTween() { descriptor = descriptor, target = target, from = from, to = to });
            return s_tweenBuilder;
        }

        public static TweenBuilder CreatePositionTween(Transform target, Vector3 from, Vector3 to, float duration)
        {
            TweenDescriptor descriptor = new TweenDescriptor() { duration = duration };
            s_tweenBuilder.FromTween(new PositionTween() { descriptor = descriptor, target = target, from = from, to = to });
            return s_tweenBuilder;
        }

        public static TweenBuilder CreateScalingTween(Transform target, Vector3 from, Vector3 to, float duration)
        {
            TweenDescriptor descriptor = new TweenDescriptor() { duration = duration };
            s_tweenBuilder.FromTween(new ScaleTween() { descriptor = descriptor, target = target, from = from, to = to });
            return s_tweenBuilder;
        }

        public static void Cancel(ITween tween)
        {
            if (TweenController.HasInstance)
                TweenController.Instance.CancelTween(tween);
        }

        public static void Start(ITween tween, UpdateType update = UpdateType.Update, bool realTime = false)
        {
            if (TweenController.HasInstance)
                TweenController.Instance.AddTween(tween, update, realTime);
        }
    }
}