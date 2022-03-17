using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

public class BridController : MonoBehaviour
{
    // LuaEnv luaenv = null;

    [SerializeField] float velocity = 2f;
    [SerializeField] float jumpForce = 12f;

    Rigidbody2D rb = null;

    // Start is called before the first frame update
    void Start()
    {
        // luaenv = new LuaEnv();
        // luaenv.DoString("require 'main'");

        // luaenv.Global.Get<LuaFunction>("PrintNum");

        rb = gameObject.GetComponent<Rigidbody2D>();
    }

    // Update is called once per frame
    void Update()
    {
        if(Input.GetKeyDown(KeyCode.Space))
        {
            Debug.Log("Space pressed => Jump");
            rb.AddForce(new Vector2(0, jumpForce));
        }
    }
}
