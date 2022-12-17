using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GameEventManager : MonoBehaviour {
    public delegate void ScoreEvent();
    public static event ScoreEvent ScoreChange;
    
    public delegate void GameoverEvent();
    public static event GameoverEvent GameoverChange;

    public delegate void GameWiningEvent();
    public static event GameWiningEvent GameWiningChange;

    public delegate void TreasureReceiveEvent();
    public static event TreasureReceiveEvent TreasureReceiveChange;

    public delegate void EquipmentReceiveEvent();
    public static event EquipmentReceiveEvent EquipmentReceiveChange;

    public void PlayerEscape() {
        if (ScoreChange != null) {
            ScoreChange();
        }
    }

    public void PlayerGameover(){
        if (GameoverChange != null) {
            GameoverChange();
        }
    }

    public void PlayerWining(){
        if (GameWiningChange !=null){
            GameWiningChange();
        }
    }

    public void TreasureReceive(){
        if (TreasureReceiveChange !=null){
            TreasureReceiveChange();
        }
    }

    public void EquipmentReceive(){
        if (EquipmentReceiveChange != null){
            EquipmentReceiveChange();
        }
    }
}


