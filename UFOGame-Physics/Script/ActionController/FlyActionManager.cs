using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class FlyActionManager : SSActionManager {
    public DiskFlyAction fly;  
    public Controller scene_controller;           

    protected void Start() {
        scene_controller = (Controller)SSDirector.GetInstance().CurrentScenceController;
        scene_controller.action_manager = this;     
    }

    //飞碟飞行
    public void DiskFly(GameObject disk, float power) {
        disk.GetComponent<Rigidbody>().isKinematic = false;
        int lor = 1;
        if (disk.transform.position.x > 0) lor = -1;
        fly = DiskFlyAction.GetSSAction(lor, power);
        this.RunAction(disk, fly, this);
    }
}
