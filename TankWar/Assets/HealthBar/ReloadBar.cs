using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

namespace ChobiAssets.KTP
{
    public class ReloadBar : MonoBehaviour
    {
        public Slider slider;
        float factor = 0.1f;
        public bool active = true;
        public Fire_Control_CS FireScript;  //获取发射状态

        private void Start() {
            slider.value = 100;
        }
        
        void change() {
            if (!FireScript.Loading() && active){   //发射完之后，清零
                while (slider.value != 0f) slider.value = slider.value - Time.deltaTime*1000;
                active = false;
            }
        }
        void Update () {
            this.transform.LookAt (Camera.main.transform.position);
            change();

            if (slider.value != 100f) {     //缓慢恢复
                slider.value = slider.value + Time.deltaTime*100;
                active = true;
            }
            Color current = slider.fillRect.transform.GetComponent<Image>().color;  //和血条一样
            if (slider.value <= 30) {
                slider.fillRect.transform.GetComponent<Image>().color = Color.Lerp(current, Color.red, factor);
            }
            else if (slider.value <=60){
                slider.fillRect.transform.GetComponent<Image>().color = Color.Lerp(current, Color.yellow, factor);
            }    
            else{
                slider.fillRect.transform.GetComponent<Image>().color = Color.Lerp(current, Color.green, factor);
            }
        }
    }
}
