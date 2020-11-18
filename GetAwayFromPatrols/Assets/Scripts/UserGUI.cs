using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour
{

    private IUserAction action;

    private GUIStyle style1 = new GUIStyle();
    private GUIStyle over_style = new GUIStyle();
    void Start()
    {
        action = SSDirector.GetInstance().CurrentScenceController as IUserAction;
        style1.normal.textColor = new Color(1, 1, 1, 1);
        style1.fontSize = 16;
        over_style.fontSize = 25;
    }

    void Update()
    {
        float translationX = Input.GetAxis("Horizontal");
        float translationZ = Input.GetAxis("Vertical");
        action.MovePlayer(translationX, translationZ);
    }
    private void OnGUI()
    {
        GUI.Label(new Rect(10, 5, 200, 50), "分数:", style1);
        GUI.Label(new Rect(55, 5, 200, 50), action.GetScore().ToString(), style1);
        if (action.GetGameover())
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.width / 2 - 150, 100, 50), "重新开始"))
            {
                action.Restart();
                return;
            }

    }
}
