using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;
using XLua;


[CSharpCallLua]
public delegate void AwakeDelegate();
public delegate void StartDelegate();
public delegate void UpdateDelegate();
public delegate void EndGameDelegate();

public class GameController : MonoBehaviour
{
    bool gameHasEnded = false;

    LuaEnv luaEnv = null;
    public TextAsset luaScript = null;
    LuaTable scriptEnv = null;

    // Lua functions
    StartDelegate luaStart = null;
    UpdateDelegate luaUpdate = null;
    EndGameDelegate luaEndGame = null;


    public GameObject bird = null;
    public float restartDelay = 1.5f;
    public GaneOverScreen gameOverScreen;
    int points = 7;

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
        // luaEnv.DoString("require 'GameController'");

        luaStart = scriptEnv.Get<StartDelegate>("LuaStart");
        luaUpdate = scriptEnv.Get<UpdateDelegate>("LuaUpdate");
        luaEndGame = scriptEnv.Get<EndGameDelegate>("LuaEndGame");
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

    public void EndGame()
    {
        luaEndGame();

        // if (gameHasEnded == true)
        // {
        //     return;
        // }

        // Debug.Log("End Game");
        // gameHasEnded = true;

        // Invoke("Restart", restartDelay);
        // GameOver();
    }

    void Restart()
    {
        SceneManager.LoadScene(SceneManager.GetActiveScene().name);
    }

    void GameOver()
    {
        gameOverScreen.Setup(points);
    }
}
