using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Managers : MonoBehaviour
{
    static Managers s_instance; // static type, the only one 'Managers'
    static Managers Instance { get { Init(); return s_instance; } } // to use in Singleton design outside the class

    InputManager _input = new InputManager();
    ResourceManager _resource = new ResourceManager();
    public static InputManager Input { get { return Instance._input; } }
    public static ResourceManager Resrouce { get { return Instance._resource; } }
    
    void Start()
    {
        Init();
    }
    void Update()
    {
        Input.OnUpdate();
    }
    static void Init()
    {
        if (s_instance == null)
        {
            GameObject go = GameObject.Find("@Managers");
            if(go == null) // if "@Managers" not found
            {
                // create new one
                go = new GameObject() { name = "@Managers" };
                go.AddComponent<Managers>();
            }

            DontDestroyOnLoad(go); // Forbidding to DESTROY THE OBJECT as required object in the project
            s_instance = go.GetComponent<Managers>();
        }
    }
}
