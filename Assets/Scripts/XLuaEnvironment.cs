using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class XLuaEnvironment: MonoBehaviour
{
    public static XLuaEnvironment instance = null;
    public static LuaEnv luaEnv = null;

    // public static XLuaEnvironment Instance
    // {
    //     get
    //     {
    //         if (instance == null)
    //         {
    //             instance = new XLuaEnvironment();
    //         }
    //         return instance;
    //     }
    // }

    private void Awake()
    {
        if (instance == null)
        {
            instance = this;
            luaEnv = new LuaEnv();

            DontDestroyOnLoad(gameObject);
        }
        else
        {
            Destroy(gameObject);
        }
        return;
    }

    public LuaTable CreateScriptEnv()
    {
        LuaTable scriptEnv = luaEnv.NewTable();

        LuaTable meta = luaEnv.NewTable();
        meta.Set("__index", luaEnv.Global);
        scriptEnv.SetMetaTable(meta);
        meta.Dispose();

        return scriptEnv;
    }
}
