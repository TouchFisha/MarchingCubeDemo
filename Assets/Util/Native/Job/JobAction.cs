using Unity.Collections.LowLevel.Unsafe;
using System;

namespace NUtil.NThreadNative
{
    public unsafe struct JobAction : IDisposable
    {
        [NativeDisableUnsafePtrRestriction]
        private void* m_Ptr;
        private ulong m_Handle;

        public JobAction(Action aCallback)
        {
            var aArr = new Action[] { aCallback };
            m_Ptr = UnsafeUtility.PinGCArrayAndGetDataAddress(aArr, out m_Handle);
        }
        public void Invoke()
        {
            UnsafeUtility.ReadArrayElement<Action>(m_Ptr, 0)();
        }
        public void Dispose()
        {
            if (m_Handle != 0)
            {
                UnsafeUtility.ReleaseGCObject(m_Handle);
                m_Ptr = (byte*)0;
                m_Handle = 0;
            }
        }

    }
}