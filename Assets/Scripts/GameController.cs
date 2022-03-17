using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[CSharpCallLua]
public delegate Vector3 GetVector3DDelegate(float x, float y, float z);

public class GameController : MonoBehaviour
{
    LuaEnv luaEnv = null;
    public GameObject pipeUp;
    public GameObject pipeDown;

    [SerializeField] float pipeSpawnTime = 10;
    float m_pipeSpawnTime = 0;
    [SerializeField] float distanceTwoPipes = 4.5f;
    [SerializeField] float startPosX = 5f;

    // Lua functions
    GetVector3DDelegate getVector3DDelegate;
    LuaFunction GetVector3D = null;

    // Start is called before the first frame update
    void Start()
    {
        luaEnv = new LuaEnv();

        luaEnv.DoString("require 'main'");
        getVector3DDelegate =  luaEnv.Global.Get<GetVector3DDelegate>("GetVector3D");
        // GetVector3D =  luaEnv.Global.Get<LuaFunction>("GetVector3D");

        SpawnPipes(); 
    }

    // Update is called once per frame
    void Update()
    {
        m_pipeSpawnTime += Time.deltaTime;

        if(m_pipeSpawnTime >= pipeSpawnTime)
        {
            Destroy(pipeUp);
            Destroy(pipeDown);
            
            if(pipeUp && pipeDown)
            {
                SpawnPipes();
            }

            m_pipeSpawnTime = 0;
        }
    }

    void SpawnPipes()
    {
        if(pipeUp && pipeDown)
        {
            Vector3 spawnPosPipeUp = getVector3DDelegate(startPosX, Random.Range(0.8f, 4.5f), 0); //(startPosX, Random.Range(0.8f, 4.5f), 0)
            Debug.Log(spawnPosPipeUp);

            // object[] vec3 = GetVector3D.Call(new object[]{11, 22, 33}, new System.Type[]{typeof(Vector3)});
            // Vector3 spawnPos = (Vector3)vec3[0];
            // Debug.Log(spawnPos);


            Vector3 spawnPosPipeDown = spawnPosPipeUp - getVector3DDelegate(0, distanceTwoPipes, 0);
            Debug.Log(spawnPosPipeDown);

            Instantiate(pipeUp, spawnPosPipeUp, Quaternion.identity);
            Instantiate(pipeUp, spawnPosPipeDown, Quaternion.identity);
        }
    }
}
