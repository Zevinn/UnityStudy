using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.EventSystems;
using UnityEngine.UI;

public class UI_Button : UI_Base
{
    enum Buttons // These must hold the ACTUAL names of each UI object in the Unity Engine
    {
        PointButton,
    }

    enum Texts
    {
        PointText,
        ScoreText,
    }

    enum GameObjects
    {
        TestObject,
    }

    enum Images
    {
        ItemIcon,
    }

    [SerializeField]
    Text text;



    private void Start()
    {
        Bind<Button>(typeof(Buttons));
        Bind<Text>(typeof(Texts));
        Bind<GameObject>(typeof(GameObjects));
        Bind<Image>(typeof(Images));

        

        GetButton((int)Buttons.PointButton).gameObject.AddUIEvent(OnButtonClicked);

        GameObject go = GetImage((int)Images.ItemIcon).gameObject;
        AddUIEvent(go, (PointerEventData data) => { go.transform.position = data.position; }, Define.UIEvent.Drag);
        // the above line is equal to the below
        // UI_EventHandler evt = go.GetComponent<UI_EventHandler>();
        // evt.OnDragHandler += ((PointerEventData data) => { go.transform.position = data.position; });
    }

    int score = 0;
    public void OnButtonClicked(PointerEventData data)
    {
        score++;
        GetText((int)Texts.ScoreText).text = $"Score: {score}";
    }
}
