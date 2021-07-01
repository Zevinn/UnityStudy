using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class UI_Base : MonoBehaviour
{
    Dictionary<Type, UnityEngine.Object[]> objects = new Dictionary<Type, UnityEngine.Object[]>();

    
    protected void Bind<T>(Type type) where T : UnityEngine.Object // specifying it for FindChild<T>() function as FindChild<T>() is also so specified.
    {
        // function that binds a button and a matching text object. ex) PointButton + ButtonText
        // Making it as 'Generic' is to specify what component it should look for and work with.

        string[] names = Enum.GetNames(type); // Retrieve all names in the enum 'type' (Buttons or Texts)
        UnityEngine.Object[] objs = new UnityEngine.Object[names.Length]; // Create an array to store them
        objects.Add(typeof(T), objs); // as the type is determined accordingly, type is T, which is the Key

        for (int i = 0; i < names.Length; i++)
        {
            if (typeof(T) == typeof(GameObject))
                objs[i] = Utils.FindChild(gameObject, names[i], true);
            else
                objs[i] = Utils.FindChild<T>(gameObject, names[i], true);

            if (objs[i] == null)
                Debug.Log($"Failed to bind: {names[i]}");
            // the 'gameObject' here is the object this script is attached to. -> 'this' object
        }
    }

    protected T Get<T>(int index) where T : UnityEngine.Object
    {
        UnityEngine.Object[] objs = null;
        if (objects.TryGetValue(typeof(T), out objs) == false)
            return null;

        return objs[index] as T; // casting it to 'T' as the 'objs[index]' was 'UnityEngin.Object' type
    }

    protected Text GetText(int index) { return Get<Text>(index); }
    protected Button GetButton(int index) { return Get<Button>(index); }
    protected Image GetImage(int index) { return Get<Image>(index); }
}
