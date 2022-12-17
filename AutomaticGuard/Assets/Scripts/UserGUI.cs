using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour {
    private IUserAction action;
    private GUIStyle score_style = new GUIStyle();
    private GUIStyle text_style = new GUIStyle();
    private GUIStyle over_style = new GUIStyle();
    void Start () {
        action = SSDirector.GetInstance().CurrentScenceController as IUserAction;
        text_style.normal.textColor = new Color(0, 0, 0, 1);
        text_style.fontSize = 16;
        score_style.normal.textColor = new Color(1,0.92f,0.016f,1);
        score_style.fontSize = 16;
        over_style.fontSize = 25;
    }

    private void OnGUI() {
        GUI.Label(new Rect(Screen.width - 170, 10, 200, 50), "分数:", text_style);
        GUI.Label(new Rect(Screen.width - 130, 10, 200, 50), action.GetScore().ToString(), score_style);

        if (action.HadEquipment() && action.GetEquipment()) GUI.Label(new Rect(Screen.width - 500, 10, 200, 50), "已获得装备！", text_style);
        if (action.HadEquipment() && !action.GetEquipment()) GUI.Label(new Rect(Screen.width - 500, 10, 200, 50), "装备损坏！", text_style);
        if (action.GetWinning()) GUI.Label(new Rect(Screen.width - 700, 10, 200, 50), "已获得宝物！", text_style);

        if (action.GetGameover()) {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.width / 2 - 250, 100, 100), "游戏结束", over_style);
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.width / 2 - 150, 100, 50), "重新开始")) {
                action.Restart();
                return;
            }
        }

        if (action.GetWinning() && action.GetReceive()) {
            GUI.Label(new Rect(Screen.width / 2 - 50, Screen.width / 2 - 250, 100, 100), "游戏胜利！", over_style);
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.width / 2 - 150, 100, 50), "重新开始")) {
                action.Restart();
                return;
            }
        }
    }
}


