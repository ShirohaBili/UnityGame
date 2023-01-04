using System.Collections;
using System.Collections.Generic;
using UnityEngine;

namespace ChobiAssets.KTP{
    public class Collide_Detect : MonoBehaviour
    {
        // Start is called before the first frame update
        private GameObject obj;
        Damage_Control_CS DamageScript;
        public Fire_Control_CS fireScript;

        void Start()
        {
            obj = this.gameObject;
        }

        // Update is called once per frame
        void Update()
        {
            
        }

        void OnCollisionEnter(Collision e) {
            if (e.gameObject.tag == "Player" && obj.tag == "medkit"){   //碰到医疗包的时候
                DamageScript = e.transform.root.GetComponent<Damage_Control_CS>();
                DamageScript.addHealth();
                Destroy(this.gameObject);
            }
            else if (e.gameObject.tag == "Player" && obj.tag =="Ammo"){     //碰到弹药包的时候
                fireScript.addAmmo();
                Destroy(this.gameObject);   //碰到了都需要删除
            }
        }
    }
}