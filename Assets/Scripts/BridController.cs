using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class BridController : MonoBehaviour
{
    LuaEnv luaEnv = null;
    Rigidbody2D rb = null;

    public delegate void LuaStartDelegate();
    public delegate void LuaUpdateDelegate();

    LuaStartDelegate LuaStart = null;
    LuaUpdateDelegate LuaUpdate = null;


    void Awake()
    {
        luaEnv = new LuaEnv();
        luaEnv.DoString("require 'BirdController'");

        LuaStart = luaEnv.Global.Get<LuaStartDelegate>("LuaStart");
        LuaUpdate = luaEnv.Global.Get<LuaUpdateDelegate>("LuaUpdate");
    }

    void Start()
    {
        if (LuaStart != null)
        {
            LuaStart();
        }
        // rb = gameObject.GetComponent<Rigidbody2D>();
    }

    void Update()
    {
        if (LuaUpdate != null)
        {
            LuaUpdate();
        }
    }
}
