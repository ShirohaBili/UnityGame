using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.AI;

namespace ChobiAssets.KTP{
    public class PlaceTarget : MonoBehaviour
    {
        public GameObject target; 
        NavMeshAgent mr;  
        Damage_Control_CS DamageScript;
        private int destPoint = 0;
        private bool found = false;
        private static Vector3[] points = { new Vector3(12.7f,-3.52f,140.4f),  new Vector3(91.72f, -2.427363f, -3.54f)};
                        
        // Use this for initialization
        void Start()
        {
            mr = GetComponent<NavMeshAgent>();  //获取到自身的NavMeshAgent组件
            DamageScript = transform.root.GetComponent<Damage_Control_CS>(); //获取到自身的Damage_Control_CS脚本，用于判断该坦克是否已经爆炸
        }

        // Update is called once per frame
        void Update()
        {
            if (!DamageScript.getStatus() && (found || Vector3.Distance(transform.position, target.transform.position) <= 30)){
                mr.SetDestination(target.transform.position);
                mr.autoBraking = true;
                found = true;
            }
            else if (!DamageScript.getStatus() && Vector3.Distance(transform.position, target.transform.position) > 30){
                if (this.gameObject.tag == "guard")  return;
                patrol();
            }
        }

        private void patrol()
        {
            if(!mr.pathPending && mr.remainingDistance < 10f){
                GotoNextPoint();
                Debug.Log(mr.pathStatus);
            }
            mr.SetDestination(points[destPoint]);
        }

        private void GotoNextPoint()
        {
            Debug.Log(mr.SetDestination(points[destPoint]));
            destPoint = (destPoint + 1) % points.Length;
        }
    }
}
