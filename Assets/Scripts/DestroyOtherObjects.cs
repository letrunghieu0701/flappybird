using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class DestroyOtherObjects : MonoBehaviour
{
    public TextAsset luaScript = null;
    OnTriggerEnter2DDelegate luaOnTriggerEnter2D = null;

    void Awake()
    {
        LuaTable scriptEnv = XLuaEnvironment.instance.CreateScriptEnv();
        scriptEnv.Set("self", this);

        XLuaEnvironment.luaEnv.DoString(luaScript.text, luaScript.name, scriptEnv);

        luaOnTriggerEnter2D = scriptEnv.Get<OnTriggerEnter2DDelegate>("LuaOnTriggerEnter2D");
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (luaOnTriggerEnter2D != null)
        {
            luaOnTriggerEnter2D(other);
        }
    }
}
