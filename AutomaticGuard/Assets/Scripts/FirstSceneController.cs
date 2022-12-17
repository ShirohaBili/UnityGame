using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.SceneManagement;


public class FirstSceneController : MonoBehaviour, IUserAction, ISceneController {
    public GuardFactory guard_factory;                               //巡逻者工厂
    public ScoreRecorder recorder;                                   //记录员
    public GuardActionManager action_manager;                        //运动管理器
    public int playerSign = -1;                                      //当前玩家所处哪个格子
    public GameObject player;                                        //玩家
    public UserGUI gui;                                             //交互界面
    public GameObject treasure;
    public GameObject npc;      
    public GameObject equipment;                                      

    private List<GameObject> guards;                                 //场景中巡逻者列表
    private bool game_over = false;                                  //游戏结束
    private bool Wining = false;
    private bool receive = false;
    private bool equipmentRec = false;
    private bool equipmentGet = false;

    
    void Awake() {
        SSDirector director = SSDirector.GetInstance();
        director.CurrentScenceController = this;
        guard_factory = Singleton<GuardFactory>.Instance;
        action_manager = gameObject.AddComponent<GuardActionManager>() as GuardActionManager;
        gui = gameObject.AddComponent<UserGUI>() as UserGUI;
        LoadResources();
        recorder = Singleton<ScoreRecorder>.Instance;
    }

    void Update() {
        for (int i = 0; i < guards.Count; i++) {
            guards[i].gameObject.GetComponent<GuardData>().playerSign = playerSign;
        }
    }


    public void LoadResources() {
        Instantiate(Resources.Load<GameObject>("Prefabs/Plane"));
        player = Instantiate(
            Resources.Load("Prefabs/Player"), 
            new Vector3(10, 0, -10), Quaternion.identity) as GameObject;
        
        treasure = Instantiate(Resources.Load("Prefabs/treasure"), new Vector3(0f,0.33f,-19f), Quaternion.identity) as GameObject;
        npc = Instantiate(Resources.Load("Prefabs/NPC"), new Vector3(12f,0f,-12f),Quaternion.identity) as GameObject;
        equipment = Instantiate(Resources.Load("Prefabs/equipment"), new Vector3(9f,1f,10f),Quaternion.identity) as GameObject;
        guards = guard_factory.GetPatrols();

        for (int i = 0; i < guards.Count; i++) {
            action_manager.GuardPatrol(guards[i], player);
        }
    }

    public int GetScore() {
        return recorder.GetScore();
    }

    public bool GetGameover() {
        return game_over;
    }

    public bool GetWinning(){
        return Wining;
    }

    public bool GetReceive(){
        return receive;
    }

    public bool GetEquipment(){
        return equipmentRec;
    }

    public bool HadEquipment(){
        return equipmentGet;
    }

    public void Restart() {
        SceneManager.LoadScene("Scenes/mySence");
    }

    void OnEnable() {
        GameEventManager.ScoreChange += AddScore;
        GameEventManager.GameoverChange += Gameover;
        GameEventManager.GameWiningChange += GameWining;
        GameEventManager.TreasureReceiveChange += ReceiveChange;
        GameEventManager.EquipmentReceiveChange += EquipmentChange;
    }
    void OnDisable() {
        GameEventManager.ScoreChange -= AddScore;
        GameEventManager.GameoverChange -= Gameover;
        GameEventManager.GameWiningChange -= GameWining;
        GameEventManager.TreasureReceiveChange -= ReceiveChange;
        GameEventManager.EquipmentReceiveChange -= EquipmentChange;
    }

    void AddScore() {
        recorder.AddScore();
    }

    void Gameover() {
        game_over = true;
    }

    void GameWining(){
        Wining = true;
    }

    void ReceiveChange(){
        receive = true;
    }

    void EquipmentChange(){
        equipmentRec = true;
        equipmentGet = true;
    }

    public void setEquipment(bool status){
        equipmentRec = status;
    }
}

