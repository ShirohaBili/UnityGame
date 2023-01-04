using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ChobiAssets.KTP
{
    public class HealthBar : MonoBehaviour
    {
        public Slider slider;
        float factor = 0.1f;
        public bool active = true;
        public Damage_Control_CS DamageScript;
        GameObject gameObject;
        float initialDurability;
        float currentDurability;

        private void Start() {
            gameObject = GameObject.Find("SD_Tiger-I_2.0");     //找到玩家的操作的坦克
            DamageScript = gameObject.GetComponent<Damage_Control_CS>();    //找到其上的Damage_Control_CS脚本
            initialDurability = 300f;
            slider.value = DamageScript.getHealth() / initialDurability * 100;  //初始化slider.value为100
        }
        
        void change() {
            if (DamageScript == null) slider.value = Mathf.Lerp(slider.value,0,0.1f);   //当玩家被摧毁时，为了避免出现空指针访问，这里直接判断值为0
            else slider.value = Mathf.Lerp(slider.value,DamageScript.getHealth() / initialDurability * 100,0.1f);   //当玩家没有被摧毁时，根据当前的生命值计算slider的值
            Debug.Log(slider.value);
        }
        void Update () {
            this.transform.LookAt (Camera.main.transform.position);
            change();

            Color current = slider.fillRect.transform.GetComponent<Image>().color;  //设置不同生命值下的血条颜色
            if (slider.value <= 30) {   //生命值在30%以下时，血条为红色
                slider.fillRect.transform.GetComponent<Image>().color = Color.Lerp(current, Color.red, factor);
            }
            else if (slider.value <=60){   //生命值在30%-60%之间时，血条为黄色
                slider.fillRect.transform.GetComponent<Image>().color = Color.Lerp(current, Color.yellow, factor);
            }    
            else{       //生命值大于60%时，血条为绿色
                slider.fillRect.transform.GetComponent<Image>().color = Color.Lerp(current, Color.green, factor);
            }
        }
    }
}
