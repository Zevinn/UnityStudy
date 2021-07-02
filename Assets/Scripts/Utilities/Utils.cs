using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Utils
{
    public static T GetorAddComponent<T>(GameObject go) where T : UnityEngine.Component
    {
        T component = go.GetComponent<T>();
        if (component == null) component = go.AddComponent<T>();

        return component;
    }
    public static GameObject FindChild(GameObject go, string name = null, bool recursive = false) // for GameObject
    {
        Transform transform = FindChild<Transform>(go, name, recursive);
        
        if (transform == null)
            return null;
        else
            return transform.gameObject;
    }

    public static T FindChild<T>(GameObject go, string name = null, bool recursive = false) where T : UnityEngine.Object // for Component
    {
        // A function that finds child of a Game Object(go)
        if (go == null)
            return null;

        if (!recursive)
        {
            for (int i = 0; i < go.transform.childCount; i++)
            {
                Transform transform = go.transform.GetChild(i); // get the i-th child
                if (string.IsNullOrEmpty(name) || transform.name == name)
                {
                    T component = transform.GetComponent<T>();

                    if (component != null)
                        return component;
                }
            }
        }
        else
        {
            foreach (T component in go.GetComponentsInChildren<T>())
            {
                if (string.IsNullOrEmpty(name) || component.name == name) // if name is not specified but, the same type, return it.
                    return component;
            }
        }

        return null;
    }
}
