using System;
using System.Runtime.InteropServices;
namespace Lua2CS
{
    public static class LuaAPI
    {
        public const int LUA_REGISTRYINDEX = -1001000;
        private const string dllName = "lua5.3.6";

#pragma warning disable IDE1006
        public static int luaL_loadbuffer(IntPtr l, byte[] buff, int size, string name)
        {
            return luaL_loadbufferx(l, buff, size, name, IntPtr.Zero);
        }
        public static void lua_pop(IntPtr l, int n)
        {
            lua_settop(l, -n - 1);
        }
        public static int lua_pcall(IntPtr l, int nargs, int nresults, int errfunc)
        {
            return lua_pcallk(l, nargs, nresults, errfunc, 0, IntPtr.Zero);
        }
        public static void lua_call(IntPtr l, int nargs, int nresults)
        {
            lua_callk(l, nargs, nresults, 0, IntPtr.Zero);
        }
        public static void lua_pushcfunction(IntPtr l, IntPtr fn)
        {
            lua_pushcclosure(l, fn, 0);
        }
        public static void lua_remove(IntPtr l, int idx)
        {
            lua_rotate(l, idx, -1);
            lua_pop(l, 1);
        }
        public static void lua_register(IntPtr l, string name, IntPtr fn)
        {
            lua_pushcfunction(l, fn);
            lua_setglobal(l, name);
        }
        public static double lua_tonumber(IntPtr l, int idx)
        {
            return lua_tonumberx(l, idx, IntPtr.Zero);
        }
#pragma warning restore IDE1006
        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static IntPtr luaL_newstate();
        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void lua_close(IntPtr l);
        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void luaL_openlibs(IntPtr l);
        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int lua_pcallk(IntPtr l, int nargs, int nresults, int errfunc, int ctx, IntPtr k);
        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_callk(IntPtr l, int nargs, int nresults, int ctx, IntPtr k);
        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void lua_pushnumber(IntPtr l, double n);
        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void lua_gettable(IntPtr l, int idx);
        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int lua_getglobal(IntPtr l, string name);
        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static int luaL_loadbufferx(IntPtr l, byte[] buff, int size, string name, IntPtr mode);
        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public extern static void lua_settop(IntPtr l, int idx);
        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lua_gettop(IntPtr l);
        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_pushcclosure(IntPtr l, IntPtr fn, int n);
        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lua_getfield(IntPtr l, int idx, string k);
        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_pushvalue(IntPtr l, int idx);
        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int luaL_ref(IntPtr l, int t);
        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_rotate(IntPtr l, int idx, int n);
        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern IntPtr lua_tolstring(IntPtr l, int idx, out IntPtr len);
        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lua_type(IntPtr l, int idx);
        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_setglobal(IntPtr l, string name);
        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_pushstring(IntPtr l, string s);
        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern double lua_tonumberx(IntPtr l, int idx, IntPtr pisnum);
        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_pushinteger(IntPtr l, int n);
        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lua_rawgeti(IntPtr l, int idx, int n);
        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern int lua_rawget(IntPtr l, int idx);
        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_rawset(IntPtr l, int idx);
        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_rawseti(IntPtr l, int idx, int n);
        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_settable(IntPtr l, int idx);
        [DllImport(dllName, CallingConvention = CallingConvention.Cdecl)]
        public static extern void lua_pushboolean (IntPtr l, int b);
    }
}


