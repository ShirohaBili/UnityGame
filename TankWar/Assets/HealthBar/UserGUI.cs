using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChobiAssets.KTP
{
    public class UserGUI : MonoBehaviour {
        private GUIStyle score_style = new GUIStyle();
        private GUIStyle text_style = new GUIStyle();

        public Fire_Control_CS fireScript;
        void Start () {
            text_style.normal.textColor = new Color(0, 0, 0, 1);
            text_style.fontSize = 16;
            score_style.normal.textColor = new Color(1,0.92f,0.016f,1);
            score_style.fontSize = 16;
        }

        private void OnGUI() {
            GUI.Label(new Rect(Screen.width - 250, 10, 200, 50), "子弹数量:", text_style);

            if (fireScript.getAmmo() !=0) GUI.Label(new Rect(Screen.width - 170, 10, 200, 50), "" + fireScript.getAmmo(), text_style);
            else GUI.Label(new Rect(Screen.width - 170, 10, 200, 50), "弹药不足！！" , score_style);
        }
    }
}


