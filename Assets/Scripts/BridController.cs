using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class BridController : MonoBehaviour
{
    LuaEnv luaEnv = null;
    public TextAsset luaScript;
    LuaTable scriptEnv = null;
    Rigidbody2D rb = null;

    public delegate void LuaAwakeDelegate();
    public delegate void LuaStartDelegate();
    public delegate void LuaUpdateDelegate();

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
        LuaStart();
        // if (LuaStart != null)
        // {
        //     LuaStart();
        // }
        // rb = gameObject.GetComponent<Rigidbody2D>();

        // gameObject.GetComponent<Rigidbody2D>();

        // this.GetComponent<Rigidbody2D>();
        gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        // if (LuaUpdate != null)
        // {
        //     LuaUpdate();
        // }
    }
}
