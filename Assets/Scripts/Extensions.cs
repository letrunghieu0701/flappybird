using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using XLua;
using System.IO;

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
        Rigidbody2D rb = go.GetComponent<Rigidbody2D>();
        rb.freezeRotation = true;
    
        return rb;
    }
}

public static class ChangeLuaExt
{
    // public static void Main()
    // {
    //     // string dir = "F:\\Unity\\FlappyBird\\Assets\\Scripts\\Resources";
    //     string dir = Directory.GetCurrentDirectory() + "\\Assets\\Scripts\\Resources\\";
    //     // Debug.Log(dir + "\\Assets\\Scripts\\Resources\\");
    //     string[] files = Directory.GetFiles(dir, "*.txt");
    //     Debug.Log(files[0]);

    //     foreach(string file in files)
    //     {
    //         // string temp = Path.ChangeExtension(file, ".lua.txt");
    //         // Debug.Log(temp);
    //         string newFileName = Path.ChangeExtension(file, ".lua.txt");

    //     }
    // }
}
