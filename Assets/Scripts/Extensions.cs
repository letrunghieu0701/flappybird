using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;

[LuaCallCSharp]
public static class Extensions
{
    public static GameObject LoadPrefab(string name){
        GameObject go = Resources.Load<GameObject>(name);
        return go;
    }

    public static Rigidbody2D GetRigidbody2D(GameObject go)
    {
        return go.GetComponent<Rigidbody2D>();
    }

    public static Rigidbody2D AddRigidbody2D(GameObject go)
    {
        go.AddComponent<Rigidbody2D>();
        return go.GetComponent<Rigidbody2D>();
    }
}
