using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerCollide : MonoBehaviour {
    private IUserAction action = SSDirector.GetInstance().CurrentScenceController as IUserAction;
    void OnCollisionEnter(Collision other) {
        //当玩家与侦察兵相撞
        if (other.gameObject.tag == "Guard") {
            if (action.GetEquipment()) {
                // Destroy(other.gameObject);
                action.setEquipment(false);
            }
            else Singleton<GameEventManager>.Instance.PlayerGameover();
        }

        if(other.gameObject.tag == "Treasure"){
            Destroy(other.gameObject);
            Singleton<GameEventManager>.Instance.PlayerWining();
        }

        if (other.gameObject.tag == "NPC"){
            if (action.GetWinning()) Singleton<GameEventManager>.Instance.TreasureReceive();
        }

        if(other.gameObject.tag == "Equipment"){
            Destroy(other.gameObject);
            Singleton<GameEventManager>.Instance.EquipmentReceive();
        }
    }
}

