using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public interface ISceneController {
    //void LoadResources();               
}

public interface IUserAction {
    void Reset();
    float getScore();
    int getRound();
    int getTrial();
    void clearTrial();
}

public enum SSActionEventType : int { Started, Competeted }

public interface ISSActionCallback {
    void SSActionEvent(SSAction source, 
        SSActionEventType events = SSActionEventType.Competeted,
        int intParam = 0, string strParam = null, Object objectParam = null);
}
