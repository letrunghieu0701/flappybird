using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;


public delegate void LuaAwakeDelegate();
public delegate void LuaStartDelegate();
public delegate void LuaUpdateDelegate();

public class BridController : MonoBehaviour
{
    LuaEnv luaEnv = null;
    public TextAsset luaScript = null;
    LuaTable scriptEnv = null;

    LuaAwakeDelegate LuaAwake = null;
    LuaStartDelegate LuaStart = null;
    LuaUpdateDelegate LuaUpdate = null;

    void Awake()
    {
        luaEnv = new LuaEnv();
        scriptEnv = luaEnv.NewTable();

        LuaTable meta = luaEnv.NewTable();
        meta.Set("__index", luaEnv.Global);
        scriptEnv.SetMetaTable(meta);
        meta.Dispose();
        
        scriptEnv.Set("self", this);

        luaEnv.DoString(luaScript.text, luaScript.name, scriptEnv);
        //luaEnv.DoString("require 'BirdController'");

        LuaAwake = scriptEnv.Get<LuaAwakeDelegate>("LuaAwake");
        LuaStart = scriptEnv.Get<LuaStartDelegate>("LuaStart");
        LuaUpdate = scriptEnv.Get<LuaUpdateDelegate>("LuaUpdate");
    }

    void Start()
    {
        if (LuaStart != null)
        {
            LuaStart();
        }
    }

    void Update()
    {
        if (LuaUpdate != null)
        {
            LuaUpdate();
        }
    }
}
