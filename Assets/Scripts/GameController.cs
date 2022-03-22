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
    public TextAsset luaScript = null;

    // Lua functions
    AwakeDelegate luaAwake = null;
    StartDelegate luaStart = null;
    UpdateDelegate luaUpdate = null;
    RestartGameDelegate luaRestartGame = null;
    ShowGameOverPanelDelegate luaShowGameOverPanel = null;
    QuitGameDelegate luaQuitGame = null;


    public GameObject gameOverPanel = null;

    void Awake()
    {
        LuaTable scriptEnv = XLuaEnvironment.Instance.CreateScriptEnv();
        scriptEnv.Set("gameOverPanel", gameOverPanel);

        XLuaEnvironment.luaEnv.DoString(luaScript.text, luaScript.name, scriptEnv);

        luaAwake = scriptEnv.Get<AwakeDelegate>("LuaAwake");
        luaStart = scriptEnv.Get<StartDelegate>("LuaStart");
        luaUpdate = scriptEnv.Get<UpdateDelegate>("LuaUpdate");

        luaShowGameOverPanel = scriptEnv.Get<ShowGameOverPanelDelegate>("LuaShowGameOverPanel");
        luaRestartGame = scriptEnv.Get<RestartGameDelegate>("LuaRestartGame");
        luaQuitGame = scriptEnv.Get<QuitGameDelegate>("LuaQuitGame");

        luaAwake();
        gameOverPanel.SetActive(false);
    }

    void Start()
    {
        if (luaStart != null)
        {
            luaStart();
        }
        // Debug.Log(UnityEngine.Screen.width + 1);
        // float halfScreenWidthInUnits = UnityEngine.Camera.main.orthographicSize * UnityEngine.Screen.width / UnityEngine.Screen.height;
        // Debug.Log(UnityEngine.Camera.main.transform.position.x + halfScreenWidthInUnits + 1);
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
