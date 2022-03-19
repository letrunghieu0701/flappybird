using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[CSharpCallLua]
public delegate void OnCollisionEnter2DDelegate(Collision2D other);

public class BridController : MonoBehaviour
{
    LuaEnv luaEnv = null;
    public TextAsset luaScript = null;
    LuaTable scriptEnv = null;

    AwakeDelegate luaAwake = null;
    StartDelegate luaStart = null;
    UpdateDelegate luaUpdate = null;

    OnCollisionEnter2DDelegate luaOnCollisionEnter2D = null;

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

        luaAwake = scriptEnv.Get<AwakeDelegate>("LuaAwake");
        luaStart = scriptEnv.Get<StartDelegate>("LuaStart");
        luaUpdate = scriptEnv.Get<UpdateDelegate>("LuaUpdate");

        luaOnCollisionEnter2D = scriptEnv.Get<OnCollisionEnter2DDelegate>("LuaOnCollisionEnter2D");
    }

    void Start()
    {
        if (luaStart != null)
        {
            luaStart();
        }
    }

    void Update()
    {
        if (luaUpdate != null)
        {
            luaUpdate();
        }
    }

    private void OnCollisionEnter2D(Collision2D other)
    {
        if (luaOnCollisionEnter2D != null)
        {
            luaOnCollisionEnter2D(other);
        }
    }
}
