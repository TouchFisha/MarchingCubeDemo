using Unity.Collections.LowLevel.Unsafe;
using System;

namespace NUtil.NThreadNative
{
    public unsafe struct MyPtr : IDisposable
    {
        [NativeDisableUnsafePtrRestriction]
        private void* m_Ptr;
        private ulong m_Handle;
        public MyPtr(void* ptr, ulong handle)
        {
            m_Ptr = ptr;
            m_Handle = handle;
        }
        public T Read<T>(int index = 0)
        {
            return UnsafeUtility.ReadArrayElement<T>(m_Ptr, index);
        }
        public static MyPtr Translate<T>(T obj)
        {
            void* ptr = UnsafeUtility.PinGCArrayAndGetDataAddress(new T[] { obj }, out ulong m_Handle);
            return new MyPtr(ptr, m_Handle);
        }
        public static T Read<T>(MyPtr ptr, int index = 0)
        {
            return ptr.Read<T>(index);
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