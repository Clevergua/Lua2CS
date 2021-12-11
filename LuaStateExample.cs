using System;
using System.Runtime.InteropServices;
using System.Text;
namespace Lua2CS
{
    public class LuaStateExample
    {
        private readonly IntPtr luaState;
        private readonly int errorFuncRef;

        public delegate int LuaFunction(IntPtr luastate);

        public LuaStateExample()
        {
            luaState = LuaAPI.luaL_newstate();
            LuaAPI.luaL_openlibs(luaState);
            // Register error function..
            var fn = Marshal.GetFunctionPointerForDelegate(new LuaFunction(SetTracebackMessage));
            LuaAPI.lua_pushcfunction(luaState, fn);
            errorFuncRef = LuaAPI.luaL_ref(luaState, LuaAPI.LUA_REGISTRYINDEX);
            // Register print function..
            fn = Marshal.GetFunctionPointerForDelegate(new LuaFunction(Print));
            LuaAPI.lua_register(luaState, "print", fn);
        }

        private int Print(IntPtr luaState)
        {
            var n = LuaAPI.lua_gettop(luaState);
            LuaAPI.lua_getglobal(luaState, "tostring");
            var sb = new StringBuilder();
            for (int i = 1; i <= n; i++)
            {
                LuaAPI.lua_pushvalue(luaState, -1);
                LuaAPI.lua_pushvalue(luaState, i);
                LuaAPI.lua_call(luaState, 1, 1);
                var ptr = LuaAPI.lua_tolstring(luaState, -1, out IntPtr lenPtr);
                var str = LuaConverter.ToString(ptr, lenPtr);
                sb.Append(str);
                sb.Append("\t");
                LuaAPI.lua_pop(luaState, 1);
            }
            LuaAPI.lua_pop(luaState, n + 1);
            System.Console.WriteLine(sb.ToString());
            return 0;
        }

        private int SetTracebackMessage(IntPtr luastate)
        {
            LuaAPI.lua_getglobal(luaState, "debug");
            LuaAPI.lua_getfield(luaState, -1, "traceback");
            LuaAPI.lua_remove(luaState, -2);
            LuaAPI.lua_pushvalue(luaState, -2);
            LuaAPI.lua_pushnumber(luaState, 2);
            LuaAPI.lua_call(luaState, 2, 1);
            LuaAPI.lua_remove(luaState, -2);
            return 1;
        }

        public void ExecuteWithNoParamAndNoReturnValue(string content)
        {
            var chunkName = "Chunk";
            var bytes = Encoding.ASCII.GetBytes(content);
            LuaAPI.lua_rawgeti(luaState, LuaAPI.LUA_REGISTRYINDEX, errorFuncRef);
            var errorFunctionIndex = LuaAPI.lua_gettop(luaState);
            if (LuaAPI.luaL_loadbuffer(luaState, bytes, bytes.Length, chunkName) == 0)
            {
                if (LuaAPI.lua_pcall(luaState, 0, 0, errorFunctionIndex) == 0)
                {
                    LuaAPI.lua_pop(luaState, 1);
                }
                else
                {
                    ThrowTopOfStackException(luaState);
                }
            }
            else
            {
                ThrowTopOfStackException(luaState);
            }
        }

        private void ThrowTopOfStackException(IntPtr luaState)
        {
            var ptr = LuaAPI.lua_tolstring(luaState, -1, out IntPtr lengthPtr);
            var message = LuaConverter.ToString(ptr, lengthPtr);
            LuaAPI.lua_pop(luaState, 1);
            throw new Exception(message);
        }
    }
}
