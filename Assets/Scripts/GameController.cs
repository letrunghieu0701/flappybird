using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XLua;


[CSharpCallLua]
public delegate void AwakeDelegate();
public delegate void StartDelegate();
public delegate void UpdateDelegate();
public delegate void ShowGameOverPanelDelegate();
public delegate void RestartGameDelegate();
public delegate void QuitGameDelegate();

public class GameController : MonoBehaviour
{
    LuaEnv luaEnv = null;
    public TextAsset luaScript = null;
    LuaTable scriptEnv = null;

    // Lua functions
    StartDelegate luaStart = null;
    UpdateDelegate luaUpdate = null;
    RestartGameDelegate luaRestartGame = null;
    ShowGameOverPanelDelegate luaShowGameOverPanel = null;
    QuitGameDelegate luaQuitGame = null;


    public GameObject bird = null;
    public float restartDelay = 1.5f;
    int points = 7;

    bool gameHasEnded = false;

    public GameObject gameOverPanel = null;

    void Awake()
    {
        luaEnv = new LuaEnv();
        scriptEnv = luaEnv.NewTable();

        LuaTable meta = luaEnv.NewTable();
        meta.Set("__index", luaEnv.Global);
        scriptEnv.SetMetaTable(meta);
        meta.Dispose();

        scriptEnv.Set("self", this);
        scriptEnv.Set("gameOverPanel", gameOverPanel);

        luaEnv.DoString(luaScript.text, luaScript.name, scriptEnv);
        // luaEnv.DoString("require 'GameController'");

        luaStart = scriptEnv.Get<StartDelegate>("LuaStart");
        luaUpdate = scriptEnv.Get<UpdateDelegate>("LuaUpdate");

        luaShowGameOverPanel = scriptEnv.Get<ShowGameOverPanelDelegate>("LuaShowGameOverPanel");
        luaRestartGame = scriptEnv.Get<RestartGameDelegate>("LuaRestartGame");
        luaQuitGame = scriptEnv.Get<QuitGameDelegate>("LuaQuitGame");

        gameOverPanel.SetActive(false);
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

    public void ShowGameOverPanel()
    {
        if (luaShowGameOverPanel != null)
        {
            luaShowGameOverPanel();
        }
    }

    public void Restart()
    {
        if (luaRestartGame != null)
        {
            luaRestartGame();
        }

        Time.timeScale = 1;

        // if (gameHasEnded == true)
        // {
        //     return;
        // }

        // Debug.Log("End Game");
        // gameHasEnded = true;

        // Invoke("Restart", restartDelay);
        // GameOver();
    }

    public void QuitGame()
    {
        if (luaQuitGame != null)
        {
            luaQuitGame();
        }
    }
}
