using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Score : MonoBehaviour
{
    // Start is called before the first frame update
    public float score;
    void Start()
    {
        score = 0;
    }

    public void add(GameObject obj){
        float num = obj.GetComponent<Disk>().score;
        score +=num;
    }

    public float getScore(){
        return score; 
    }

    public void clear(){
        score = 0;
    }
}
