using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Controller : MonoBehaviour, ISceneController, IUserAction
{
    // Start is called before the first frame update
    public DiskFactory factory = new DiskFactory();
    public BoomFactory boom_fac = new BoomFactory();
    public Score record = new Score();
    public UserGUI User_GUI = new UserGUI(); 
    public FlyActionManager action_manager;
    Vector3 startPos = new Vector3(0,-10f,0);
    private int count = 0;

    void Start()
    {
        SSDirector director = SSDirector.GetInstance();     
        director.CurrentScenceController = this;
        factory = Singleton<DiskFactory>.Instance;
        boom_fac = Singleton<BoomFactory>.Instance;
        record = Singleton<Score>.Instance;

        action_manager = gameObject.AddComponent<FlyActionManager>() as FlyActionManager;
        User_GUI = gameObject.AddComponent<UserGUI>() as UserGUI;
    }

    bool run = false;
    int trial = 0;
    public int round = 1;
    float BoomTimer = 0;
    float maxTime = 2;
    // Update is called once per frame
    void Update()
    {   
        if (Input.GetButtonDown("Fire1")){
            Vector3 mp = Input.mousePosition;
            hit(mp);
        }

        // Debug.Log(record.getScore());

        if (run){
            count++;
            if (round == 1){    //第一回合
                if (count == 300){
                    float num = Random.Range(0f,1f);
                    if (num <= 0.75f) send(1);
                    else if (num > 0.75f && num < 0.95f) send(2);
                    else send(3);

                    count = 0;
                    trial++;

                    if (trial == 10) {
                        round++;
                        trial = 0;
                    }
                }
            }
            else if (round == 2){   //第二回合
                if (count == 250){
                    float num = Random.Range(0f,1f);
                    if (num <= 0.5f) send(1);
                    else if (num > 0.5f && num < 0.9f) send(2);
                    else send(3);

                    count =  0;
                    trial++;

                    if (trial == 10){
                        round++;
                        trial = 0;
                    }
                }
            }
            else if (round == 3){   //第三回合
                if (count == 200){
                    float num = Random.Range(0f,1f);
                    if (num <= 0.4f) send(1);
                    else if (num > 0.4f && num < 0.8f) send(2);
                    else send(3);

                    count = 0;
                    trial++;

                    if (trial == 10){
                        run = false;
                    }
                }
            }
            factory.FreeDisk();
        }

        if(!boom_fac.getEmpty()){
            BoomTimer +=Time.deltaTime;
            if (BoomTimer >= maxTime){
                boom_fac.OutList();
                BoomTimer = 0;
            }
        }
    }

    public void send(int type){
        GameObject curDisk = factory.GetDisk(type);

        float ran_y = 0;
        float ran_x = Random.Range(-1f, 1f);
        if (ran_x < 0) ran_x = -1f;
        else ran_x = 1f;

        float power = 0;
        float angle = 0;

        if (type == 1){     //蓝色飞盘分数为1，简单一点，速度慢，倾角大
            ran_y = Random.Range(0f,2f);
            power = Random.Range(1f,2f);
            angle = Random.Range(20f,30f);
        }
        else if (type == 2){       //红色飞盘分数为2，稍难一点，速度稍快，倾角适中
            ran_y = Random.Range(0f,2f);
            power = Random.Range(2f,3f);
            angle = Random.Range(10f,20f);
        }
        else if (type == 3){     //绿色飞盘分数为3，最难，速度最快，倾角最小
            ran_y = Random.Range(0f,3f);
            power = Random.Range(3f,4f);
            angle = Random.Range(0f,10f);
        }
        curDisk.transform.position = new Vector3(ran_x*16f, ran_y, 0);
        action_manager.DiskFly(curDisk, angle, power);
    }

    public void hit(Vector3 pos){
        Ray ray = Camera.main.ScreenPointToRay(pos);

        RaycastHit hit;
        
        if (Physics.Raycast(ray,out hit)){
            if (hit.collider.gameObject.GetComponent<Disk>() !=null){
                Vector3 targetPos = hit.collider.gameObject.transform.position;
                boom_fac.getExp(targetPos);
                record.add(hit.collider.gameObject);
                hit.collider.gameObject.transform.position = startPos;
            }
        }
    }

    public void Reset(){
        run = true;
        round = 1;
        trial = 0;
        record.clear();
    }

    public float getScore(){
        return record.getScore();
    }

    public int getRound(){
        return round;
    }

    public int getTrial(){
        return trial;
    }
    public void clearTrial(){
        trial = 0;
    }
}
