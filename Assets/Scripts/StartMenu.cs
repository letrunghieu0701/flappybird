using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XLua;

[CSharpCallLua]
public delegate void StartGameDelegate();
public class StartMenu : MonoBehaviour
{
    LuaEnv luaEnv = null;
    LuaTable scriptEnv = null;
    public TextAsset luaScript = null;

    StartGameDelegate luaStartGame = null;

    int gameSceneIndex = 1;

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

        luaStartGame = scriptEnv.Get<StartGameDelegate>("LuaStartGame");
    }
    public void StartGame()
    {
        // SceneManager.LoadScene(gameSceneIndex);
        if (luaStartGame != null)
        {
            luaStartGame();
        }
    }
}
