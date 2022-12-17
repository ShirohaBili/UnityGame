using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PlayerInput : MonoBehaviour {
    [Header("---- KeyCode Settings ----")]
    public string keyUp = "w";
    public string keyDown = "s";
    public string keyLeft = "a";
    public string keyRight = "d";

    public string keyA = "left shift";
    public string keyB = "space";
    public string keyC = "k";
    public string keyD;

    [Header("---- Output Settings ----")]
    public float Dup;
    public float Dright;
    public float Dmag;//方向
    public Vector3 Dvec;//速度

    public float Jup;
    public float Jright;

    //1.pressing signal
    public bool run;
    public bool jump;
    private bool lastJump;
    //2.trigger signal
    //3.double signal

    [Header("---- Other Settings ----")]
    public bool inputEnabled = true;

    private float targetDup;
    private float targetDright;
    private float velocityDup;
    private float velocityDright;

    void Start() {}

    void Update() {
        targetDup = (Input.GetKey(KeyCode.W) ? 1.0f : 0) - (Input.GetKey(KeyCode.S) ? 1.0f : 0);
        targetDright = (Input.GetKey(KeyCode.D) ? 1.0f : 0) - (Input.GetKey(KeyCode.A) ? 1.0f : 0);

        if(!inputEnabled) {
            targetDup = 0;
            targetDright = 0;
        }
        //平滑变动
        Dup = Mathf.SmoothDamp(Dup, targetDup, ref velocityDup, 0.1f);
        Dright = Mathf.SmoothDamp(Dright, targetDright, ref velocityDright, 0.1f);

        /*矩形坐标转圆坐标*/
        Vector2 tempDAxis = SquareToCircle(new Vector2(Dup, Dright));
        float Dup2 = tempDAxis.x;
        float Dright2 = tempDAxis.y;

        Dmag = Mathf.Sqrt((Dup2 * Dup2) + (Dright2 * Dright2));
        Dvec = Dright * transform.right + Dup * transform.forward;
        //run = Input.GetKey(keyA);
        run = Input.GetKey(KeyCode.LeftShift);

        /*跳跃*/
        //bool newJump = Input.GetKey(keyB);
        bool newJump = Input.GetKey(KeyCode.Space);
        lastJump = jump;
        if(lastJump == false && newJump == true) {
            jump = true;
        }
        else {
            jump = false;
        }
    }

    /*矩形坐标转圆坐标*/
    private Vector2 SquareToCircle(Vector2 input) {
        Vector2 output = Vector2.zero;
        output.x = input.x * Mathf.Sqrt(1 - (input.y * input.y) / 2.0f);
        output.y = input.y * Mathf.Sqrt(1 - (input.x * input.x) / 2.0f);
        return output;
    }
}

