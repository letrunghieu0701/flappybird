using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class Parallax : MonoBehaviour
{
    public TextAsset luaScript = null;

    StartDelegate luaStart = null;
    UpdateDelegate luaUpdate = null;


    public float length, startPos;
    public float parallaxVelocity;

    void Awake()
    {
        LuaTable scriptEnv = XLuaEnvironment.instance.CreateScriptEnv();
        scriptEnv.Set("self", this);

        XLuaEnvironment.luaEnv.DoString(luaScript.text, luaScript.name, scriptEnv);

        luaStart = scriptEnv.Get<StartDelegate>("LuaStart");
        luaUpdate = scriptEnv.Get<UpdateDelegate>("LuaUpdate");
    }


    // Start is called before the first frame update
    void Start()
    {
        if (luaStart != null)
        {
            luaStart();
        }
    }

    // Update is called once per frame
    void Update()
    {
        if (luaUpdate != null)
        {
            luaUpdate();
        }
    }
}
