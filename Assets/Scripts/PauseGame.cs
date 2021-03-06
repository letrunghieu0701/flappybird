using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using XLua;


public class PauseGame : MonoBehaviour
{
    private bool isPausing;
    private GameController gameController = null;
    private Image image = null;

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

        gameObject.SetActive(true);
    }

    // Start is called before the first frame update
    void Start()
    {
        if (luaStart != null)
        {
            luaStart();
        }

        // image = GetComponent<Image>();
        // Component a = new Component();
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
