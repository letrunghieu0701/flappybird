using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XLua;


public class ScoreLine : MonoBehaviour
{
    LuaEnv luaEnv = null;
    LuaTable scriptEnv = null;
    public TextAsset luaScript = null;
    OnTriggerEnter2DDelegate luaOnTriggerEnter2D = null;
    StartDelegate luaStart = null;

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

        luaStart = scriptEnv.Get<StartDelegate>("LuaStart");
        luaOnTriggerEnter2D = scriptEnv.Get<OnTriggerEnter2DDelegate>("LuaOnTriggerEnter2D");

    }

    void Start()
    {
        if (luaStart != null)
        {
            luaStart();
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (luaOnTriggerEnter2D != null)
        {
            luaOnTriggerEnter2D(other);
        }
    } 
}
