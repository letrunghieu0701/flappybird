using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;


public class PipeController : MonoBehaviour
{
    public TextAsset luaScript = null;

    StartDelegate luaStart = null;
    UpdateDelegate luaUpdate = null;


    void Awake()
    {
        LuaTable scriptEnv = XLuaEnvironment.instance.CreateScriptEnv();
        scriptEnv.Set("self", this);

        XLuaEnvironment.luaEnv.DoString(luaScript.text, luaScript.name, scriptEnv);

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
    }
}
