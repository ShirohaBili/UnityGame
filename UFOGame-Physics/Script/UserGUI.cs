using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class UserGUI : MonoBehaviour
{
    // Start is called before the first frame update
    private IUserAction action;
    private GUIStyle font = new GUIStyle();
    private GUIStyle label = new GUIStyle();
    private int targetNum = 0;


    void Start()
    {
        action = SSDirector.GetInstance().CurrentScenceController as IUserAction;

        font.fontSize = 30;
        label.fontSize = 20;
        label.normal.textColor = new Color(1,0,0);
    }

    private bool run = false;
    bool choose = false;
    // Update is called once per frame
    void OnGUI()
    {
        GUI.Label(new Rect(0, 5, 20, 20), "当前分数：" + action.getScore().ToString(),label);
        GUI.Label(new Rect(140, 5, 20, 20),"当前轮数：" + action.getRound().ToString(),label);
        GUI.Label(new Rect(280, 5, 20, 20),"目标分数：" + targetNum.ToString(),label);
        // Debug.Log(action.getEnd());
        // Debug.Log(run);
        if (!run){
            GUI.Label(new Rect(Screen.width / 2 - 100, Screen.height / 2 - 100, 100, 100), "打飞碟小游戏",font);
                if (GUI.Button(new Rect(Screen.width / 2 - 200, Screen.height / 2, 100, 50), "简单难度")) {
                    targetNum = 8;
                    choose = true;
                }
                if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2, 100, 50), "一般难度")) {
                    targetNum = 12;
                    choose = true;
                }
                if (GUI.Button(new Rect(Screen.width / 2 + 100, Screen.height / 2, 100, 50), "困难难度")) {
                    targetNum = 18;
                    choose = true;
                }
                
                if (choose){
                    run = true;
                    action.Reset();
                }
        }
        if (action.getRound() == 3 && action.getTrial() == 10){
            if (action.getScore() >= targetNum) GUI.Label(new Rect(Screen.width / 2 - 60, Screen.height / 2 - 100, 100, 100), "游戏胜利！",font);
            else GUI.Label(new Rect(Screen.width / 2 - 60, Screen.height / 2 - 100, 100, 100), "未达成目标，游戏失败！",font);
            GUI.Label(new Rect(Screen.width / 2 - 60, Screen.height / 2 - 50, 100, 100), "你的得分是：" + action.getScore(),font);
            if (GUI.Button(new Rect(Screen.width / 2 - 50, Screen.height / 2, 100, 50), "再来一局！")){
                run = false;
                choose = false;
                action.clearTrial();
            }
        }
    }
}
