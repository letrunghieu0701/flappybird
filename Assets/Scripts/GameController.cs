using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;


[CSharpCallLua]
public delegate void AwakeDelegate();
public delegate void StartDelegate();
public delegate void UpdateDelegate();

public class GameController : MonoBehaviour
{
    LuaEnv luaEnv = null;
    public TextAsset luaScript = null;
    LuaTable scriptEnv = null;

    // Lua functions
    StartDelegate luaStart = null;
    UpdateDelegate luaUpdate = null;

    public GameObject bird = null;

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
        // luaEnv.DoString("require 'GameController'");

        luaStart = scriptEnv.Get<StartDelegate>("LuaStart");
        luaUpdate = scriptEnv.Get<UpdateDelegate>("LuaUpdate");
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

        // Rigidbody2D rb = new Rigidbody2D();
        
        // rb.gravityScale = 0.0f;
    }

    public void EndGame()
    {
        Debug.Log("End Game");
    }
}
