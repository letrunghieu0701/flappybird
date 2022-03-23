using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XLua;

[CSharpCallLua]
public delegate void StartGameDelegate();
public class StartMenu : MonoBehaviour
{
    public TextAsset luaScript = null;

    StartGameDelegate luaStartGame = null;

    int gameSceneIndex = 1;

    void Awake()
    {
        LuaTable scriptEnv = XLuaEnvironment.instance.CreateScriptEnv();
        scriptEnv.Set("self", this);

        XLuaEnvironment.luaEnv.DoString(luaScript.text, luaScript.name, scriptEnv);

        luaStartGame = scriptEnv.Get<StartGameDelegate>("LuaStartGame");
    }
    public void StartGame()
    {
        if (luaStartGame != null)
        {
            luaStartGame();
        }
    }
}
