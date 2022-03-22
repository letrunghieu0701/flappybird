using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class Parallax : MonoBehaviour
{
    public TextAsset luaScript = null;

    StartDelegate luaStart = null;
    UpdateDelegate luaUpdate = null;


    public float length, startPos;
    public float parallaxVelocity;

    void Awake()
    {
        LuaTable scriptEnv = XLuaEnvironment.Instance.CreateScriptEnv();
        scriptEnv.Set("self", this);

        XLuaEnvironment.luaEnv.DoString(luaScript.text, luaScript.name, scriptEnv);

        luaStart = scriptEnv.Get<StartDelegate>("LuaStart");
        luaUpdate = scriptEnv.Get<UpdateDelegate>("LuaUpdate");
    }


    // Start is called before the first frame update
    void Start()
    {
        startPos = transform.position.x;
        length = GetComponent<SpriteRenderer>().bounds.size.x;
        // luaStart();
    }

    // Update is called once per frame
    void Update()
    {
        float dist = -parallaxVelocity * Time.deltaTime;

        transform.position = new Vector3(transform.position.x + dist, transform.position.y, transform.position.z);

        if (transform.position.x + length/2 <= startPos)
        {
            transform.position = new Vector3(startPos + length/2, transform.position.y, transform.position.z);
        }
    }
}
