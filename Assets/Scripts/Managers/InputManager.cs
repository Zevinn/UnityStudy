using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;

public class InputManager
{
    public Action KeyAction = null; // delegate
    public Action<Define.MouseEvent> MouseAction = null;
    
    bool _pressed = false;

    public void OnUpdate()
    {
        if (EventSystem.current.IsPointerOverGameObject()) // if UI clicked, return true;
            return;

        if (Input.anyKey && KeyAction != null)
            KeyAction.Invoke();
        
        if (MouseAction != null)
        {
            if (Input.GetMouseButton(0))
            {
                MouseAction.Invoke(Define.MouseEvent.Press);
                _pressed = true;
            }
            else
            {
                if (_pressed)
                {
                    MouseAction.Invoke(Define.MouseEvent.Click); // Action<Define.MouseEvent> 타입이라 그 자체의 인자를 필요로 하나봄
                }
                _pressed = false;
            }
        }
    }
}
