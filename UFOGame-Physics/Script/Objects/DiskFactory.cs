using System.Collections;
using System.Collections.Generic;
using UnityEngine;
// 工厂
public class DiskFactory : MonoBehaviour {
    private List<Disk> used = new List<Disk>();
    private List<Disk> free = new List<Disk>();
    Vector3 startPos = new Vector3(0,-10f,0);

    public GameObject GetDisk(int type) {
        GameObject curdisk = null;
        if (free.Count>0) {
            for(int i = 0; i < free.Count; i++) {
                if(!(free[i].type==1||free[i].type==2||free[i].type==3))
                    Debug.Log("error");
                if (free[i].type == type) {
                    curdisk = free[i].gameObject;
                    free.Remove(free[i]);
                    break;
                }
            }     
        }
        //Debug.Log(curdisk == null);
        if(curdisk == null) {
            if (type == 1) {
                curdisk = Instantiate(Resources.Load<GameObject>("Prefabs/BlueDisk"),startPos, Quaternion.identity);
            }            
            else if(type == 2) {
                curdisk = Instantiate(Resources.Load<GameObject>("Prefabs/RedDisk"),startPos, Quaternion.identity);
            }
            else if (type == 3){
                curdisk = Instantiate(Resources.Load<GameObject>("Prefabs/GreenDisk"),startPos, Quaternion.identity);
            }
            curdisk.GetComponent<Renderer>().material.color = curdisk.GetComponent<Disk>().color;
        }

        used.Add(curdisk.GetComponent<Disk>());
        curdisk.SetActive(true);
        return curdisk;
    }

    public void FreeDisk() {
        // Debug.Log("before");
        // Debug.Log(used.Count);
        // Debug.Log(free.Count);
        for(int i=0; i<used.Count; i++) {
            if (used[i].gameObject.transform.position.y <= -9f) {
                Disk t = used[i];
                free.Add(t);
                used.Remove(used[i]);
            }
        }      
        // Debug.Log("after");
        // Debug.Log(used.Count);
        // Debug.Log(free.Count);    
    }

    public void Reset() {
        FreeDisk();
    }

}
