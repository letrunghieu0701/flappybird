using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
using UnityEngine.SceneManagement;
using XLua;

[CSharpCallLua]
public delegate void GameOverDelegate();

public class GameOverScene : MonoBehaviour
{
    LuaEnv luaEnv = null;
    LuaTable scriptEnv = null;
    public TextAsset luaScript = null;

    static GameOverDelegate luaGameOver = null;
    public Text pointsText;

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

        luaGameOver = scriptEnv.Get<GameOverDelegate>("LuaGameOver");
    }

    public void Setup(int score)
    {
        gameObject.SetActive(true);
        pointsText.text = score.ToString() + " POINTS";
    }

    public static void GameOver()
    {
        int gameOverSceneIndex = 2;
        SceneManager.LoadScene(gameOverSceneIndex);
        // if (luaGameOver != null)
        // {
        //     luaGameOver();
        // }
    }
}
