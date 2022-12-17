using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class BoomFactory : MonoBehaviour
{
    public Queue<GameObject> que = new Queue<GameObject>();
    // Start is called before the first frame update
    public void getExp(Vector3 pos){
        GameObject effect = Instantiate(Resources.Load("Prefabs/Boom") as GameObject, pos, Quaternion.identity);
        que.Enqueue(effect);
    }

    public void OutList(){
        GameObject curEff = que.Dequeue();
        Destroy(curEff);
    }

    public bool getEmpty(){
        bool judge = false;
        if (que.Count == 0)  judge = true; 
        return judge;
    }
}
