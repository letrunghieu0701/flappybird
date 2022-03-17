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
}
