using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 具体动作的实现，继承于SSAction动作基类
public class DiskFlyAction : SSAction {
    public float gravity = -0.5f;                                 
    private Vector3 start_vector;                             
    private Vector3 gravity_vector = Vector3.zero;             
    private Vector3 current_angle = Vector3.zero;              
    private float time;                                        
    public float power;

    private DiskFlyAction() { }
    public static DiskFlyAction GetSSAction(int lor, float power) {
        //初始化物体将要运动的初速度向量
        DiskFlyAction action = CreateInstance<DiskFlyAction>();
        if (lor == -1) {
            action.start_vector = Vector3.left * power;
        }
        else {
            action.start_vector = Vector3.right * power;
        }
        action.power = power;
        return action;
    }

    public override void Update() {
        //计算物体的向下的速度,v=at
        // time += Time.fixedDeltaTime;
        // gravity_vector.y = gravity * time * 0.1f;

        // //位移模拟
        // transform.position += (start_vector + gravity_vector) * Time.fixedDeltaTime;
        // current_angle.z = Mathf.Atan((start_vector.y + gravity_vector.y) / start_vector.x) * Mathf.Rad2Deg;
        // transform.eulerAngles = current_angle;

        // if (this.transform.position.y < -10) {
        //     this.destroy = true;
        //     this.callback.SSActionEvent(this);      
        // }
    }

    private void FixedUpdate() {
        if (this.transform.position.y < -10) {
            gameobject.GetComponent<Rigidbody>().velocity = new Vector3(0, 0, 0);
            // gameobject.GetComponent<Rigidbody>().angularVelocity = new Vector3(0, 0, 0);
            // gameobject.GetComponent<Rigidbody>().speed.X = new Vector3(0, 0, 0);
            this.destroy = true;
            this.callback.SSActionEvent(this);
        }
    }

    public override void Start() {
        gameobject.GetComponent<Rigidbody>().AddForce(start_vector*3, ForceMode.Impulse);
    }
}
