using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;


[CSharpCallLua]
public delegate void StartDelegate();
public delegate void UpdateDelegate();

public class GameController : MonoBehaviour
{
    LuaEnv luaEnv = null;

    // Lua functions
    StartDelegate luaStart = null;
    UpdateDelegate luaUpdate = null;

    public GameObject bird = null;

    void Awake()
    {
        luaEnv = new LuaEnv();
        luaEnv.DoString("require 'GameController'");

        luaStart = luaEnv.Global.Get<StartDelegate>("LuaStart");
        luaUpdate = luaEnv.Global.Get<UpdateDelegate>("LuaUpdate");
    }


    void Start()
    {
        luaStart();

        bird.AddComponent<Rigidbody2D>();
    }


    void Update()
    {
        // bird.GetComponent<Rigidbody2D>().velocity = Vector2.up * 2;
        // bird.GetComponent<Rigidbody2D>().AddForce();

        luaUpdate();
    }
}
