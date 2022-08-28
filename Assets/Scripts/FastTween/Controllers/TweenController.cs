using System;
using System.Collections.Generic;
using UnityEngine;

namespace FastTween
{
    internal partial class TweenController : MonoBehaviour
    {
        private uint m_counter;
        private bool m_updating;
        private readonly List<ITween> m_cancelList = new List<ITween>();
        private readonly BufferCollection<FloatTween> m_floatTweenBuffers = new BufferCollection<FloatTween>();
        private readonly BufferCollection<PositionTween> m_positionTweenBuffers = new BufferCollection<PositionTween>();
        private readonly BufferCollection<RotationTween> m_rotationTweenBuffers = new BufferCollection<RotationTween>();
        private readonly BufferCollection<ScaleTween> m_scaleTweenBuffers = new BufferCollection<ScaleTween>();

        private static TweenController s_instance;
        public static TweenController Instance
        {
            get
            {
                if (s_instance == null)
                {
                    GameObject gameObject = new GameObject("TweenController");
                    gameObject.hideFlags = HideFlags.HideAndDontSave;
                    s_instance = gameObject.AddComponent<TweenController>();
                }

                return s_instance;
            }
        }

        public static bool HasInstance => s_instance != null;

        public void AddTween(ITween tween, UpdateType update, bool realTime)
        {
            if (tween.Id != 0)
                throw new ArgumentException("Tween was previously started");

            tween.Id = CreateUniqueId(update, realTime);

            if (tween is FloatTween floatTween)
                m_floatTweenBuffers.AddTween(ref floatTween, update, realTime);
            else if (tween is PositionTween positionTween)
                m_positionTweenBuffers.AddTween(ref positionTween, update, realTime);
            else if (tween is RotationTween rotationTween)
                m_rotationTweenBuffers.AddTween(ref rotationTween, update, realTime);
            else if (tween is ScaleTween scaleTween)
                m_scaleTweenBuffers.AddTween(ref scaleTween, update, realTime);
            else
                throw new InvalidOperationException($"'{tween.GetType().Name}' is not a valid tween");
        }

        public void CancelTween(ITween tween)
        {
            if (tween.Id == 0)
                throw new ArgumentException("Not a valid tween");

            if (m_updating)
            {
                m_cancelList.Add(tween);
            }
            else
            {
                if (tween is FloatTween floatTween)
                    CancelFloatTween(floatTween);
                else if (tween is PositionTween positionTween)
                    CancelPositionTween(positionTween);
                else if (tween  is RotationTween rotationTween)
                    CancelRotationTween(rotationTween);
                else if (tween is ScaleTween scaleTween)
                    CancelScaleTween(scaleTween);
                else
                    Debug.LogWarning($"Could not cancel tween {tween}");
            }
        }

        private void Update()
        {
            m_updating = true;
            UpdateFloatTweenBuffer(m_floatTweenBuffers.scaledUpdateBuffer, Time.deltaTime);
            UpdateFloatTweenBuffer(m_floatTweenBuffers.unscaledUpdateBuffer, Time.unscaledDeltaTime);
            UpdatePositionTweenBuffer(m_positionTweenBuffers.scaledUpdateBuffer, Time.deltaTime);
            UpdatePositionTweenBuffer(m_positionTweenBuffers.unscaledUpdateBuffer, Time.unscaledDeltaTime);
            UpdateRotationTweenBuffer(m_rotationTweenBuffers.scaledUpdateBuffer, Time.deltaTime);
            UpdateRotationTweenBuffer(m_rotationTweenBuffers.unscaledUpdateBuffer, Time.unscaledDeltaTime);
            UpdateScaleTweenBuffer(m_scaleTweenBuffers.scaledUpdateBuffer, Time.deltaTime);
            UpdateScaleTweenBuffer(m_scaleTweenBuffers.unscaledUpdateBuffer, Time.unscaledDeltaTime);
            m_updating = false;

            CheckCancelList();
        }

        private void LateUpdate()
        {
            m_updating = true;
            UpdateFloatTweenBuffer(m_floatTweenBuffers.scaledLateUpdateBuffer, Time.deltaTime);
            UpdateFloatTweenBuffer(m_floatTweenBuffers.unscaledLateUpdateBuffer, Time.unscaledDeltaTime);
            UpdatePositionTweenBuffer(m_positionTweenBuffers.scaledLateUpdateBuffer, Time.deltaTime);
            UpdatePositionTweenBuffer(m_positionTweenBuffers.unscaledLateUpdateBuffer, Time.unscaledDeltaTime);
            UpdateRotationTweenBuffer(m_rotationTweenBuffers.scaledLateUpdateBuffer, Time.deltaTime);
            UpdateRotationTweenBuffer(m_rotationTweenBuffers.unscaledLateUpdateBuffer, Time.unscaledDeltaTime);
            UpdateScaleTweenBuffer(m_scaleTweenBuffers.scaledLateUpdateBuffer, Time.deltaTime);
            UpdateScaleTweenBuffer(m_scaleTweenBuffers.unscaledLateUpdateBuffer, Time.unscaledDeltaTime);
            m_updating = false;

            CheckCancelList();
        }

        private void FixedUpdate()
        {
            m_updating = true;
            UpdateFloatTweenBuffer(m_floatTweenBuffers.fixedUpdateBuffer, Time.fixedDeltaTime);
            UpdatePositionTweenBuffer(m_positionTweenBuffers.fixedUpdateBuffer, Time.fixedDeltaTime);
            UpdateRotationTweenBuffer(m_rotationTweenBuffers.fixedUpdateBuffer, Time.fixedDeltaTime);
            UpdateScaleTweenBuffer(m_scaleTweenBuffers.fixedUpdateBuffer, Time.fixedDeltaTime);
            m_updating = false;

            CheckCancelList();
        }

        private void CheckCancelList()
        {
            for (int i = 0; i < m_cancelList.Count; i++)
            {
                CancelTween(m_cancelList[i]);
            }

            m_cancelList.Clear();
        }

        private long CreateUniqueId(UpdateType update, bool realtime)
        {
            long id = 0L;
            id |= realtime ? 1L << 63 : 0L;
            id |= 1L << 62;
            id |= (long)update << 32;
            id |= m_counter++;
            return id;
        }

        private (UpdateType updateType, bool realTime) GetInfo(long id)
        {
            bool realtime = (id >> 63) == 1;
            UpdateType updateType = (UpdateType)((int)(id >> 32) & 0x3FFFFFFF);
            return (updateType, realtime);
        }
    }
}
