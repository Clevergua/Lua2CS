using System;
using System.Runtime.InteropServices;
using System.Text;
namespace Lua2CS
{
    public static class LuaConverter
    {
        public static string ToString(IntPtr strPtr, IntPtr lenPtr)
        {
            if (strPtr != IntPtr.Zero)
            {
                var length = lenPtr.ToInt32();
                string r = Marshal.PtrToStringAnsi(strPtr, length);
                if (r == null)
                {
                    byte[] buffer = new byte[length];
                    Marshal.Copy(strPtr, buffer, 0, length);
                    return Encoding.UTF8.GetString(buffer);
                }
                return r;
            }
            else
            {
                return null;
            }
        }
    }
}
