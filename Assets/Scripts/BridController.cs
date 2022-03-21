using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[CSharpCallLua]
public delegate void OnCollisionEnter2DDelegate(Collision2D other);
public delegate void OnTriggerEnter2DDelegate(Collider2D other);

public class BridController : MonoBehaviour
{
    public int score = 0;
    public TextAsset luaScript = null;

    StartDelegate luaStart = null;
    UpdateDelegate luaUpdate = null;

    OnCollisionEnter2DDelegate luaOnCollisionEnter2D = null;
    OnTriggerEnter2DDelegate luaOnTriggerEnter2D = null;

    void Awake()
    {
        LuaTable scriptEnv = XLuaEnvironment.Instance.CreateScriptEnv();
        scriptEnv.Set("self", this);

        XLuaEnvironment.luaEnv.DoString(luaScript.text, luaScript.name, scriptEnv);

        luaStart = scriptEnv.Get<StartDelegate>("LuaStart");
        luaUpdate = scriptEnv.Get<UpdateDelegate>("LuaUpdate");

        luaOnCollisionEnter2D = scriptEnv.Get<OnCollisionEnter2DDelegate>("LuaOnCollisionEnter2D");
        luaOnTriggerEnter2D = scriptEnv.Get<OnTriggerEnter2DDelegate>("LuaOnTriggerEnter2D");
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

    void OnCollisionEnter2D(Collision2D other)
    {
        if (luaOnCollisionEnter2D != null)
        {
            luaOnCollisionEnter2D(other);
        }
    }

    private void OnTriggerEnter2D(Collider2D other)
    {
        if (luaOnTriggerEnter2D != null)
        {
            luaOnTriggerEnter2D(other);
        }

        // gameObject.GetComponent<AudioSource>().Play();
        FindObjectOfType<AudioManager>().Play("coin");
    }

    public void IncreaseScore()
    {
        score++;
    }
}
