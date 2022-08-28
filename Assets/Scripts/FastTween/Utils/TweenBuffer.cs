using System;
using UnityEngine;

namespace FastTween
{
    internal sealed class TweenBuffer<T>
    {
        private T[] m_buffer;
        private int m_count;
        private readonly int m_maxGrowth;

        public T[] Buffer => m_buffer;
        public int Count => m_count;

        public TweenBuffer(int initialSize, int maxGrowth)
        {
            m_buffer = new T[initialSize];
            m_maxGrowth = maxGrowth;
        }

        public int Add(T item)
        {
            if (m_buffer.Length == m_count)
                Grow();

            m_buffer[m_count] = item;
            return m_count++;
        }

        public void Remove(int index)
        {
            m_buffer[index] = m_buffer[--m_count];
        }

        private void Grow()
        {
            int size = Mathf.Clamp(m_buffer.Length * 2, 16, m_maxGrowth);
            Array.Resize(ref m_buffer, m_buffer.Length + size);
        }
    }
}