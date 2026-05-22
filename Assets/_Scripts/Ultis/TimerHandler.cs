using System;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class TimerHandler 
{
    
    private static List<FunctionTimer> activeTimers;
    public static GameObject initGameObject;

    private static void InitIfNeed()
    {
        if(initGameObject == null)
        {
            initGameObject=new GameObject("TimeHandler_InitChecker");
            activeTimers=new List<FunctionTimer>();
        }
    }
    public static FunctionTimer CreateTimer(Action action, float time, string name=null)
    {
        GameObject gameObject= new GameObject("Timer",typeof(MonoBehaviourHook));
        FunctionTimer timer=new FunctionTimer(action,time,gameObject,name);
        gameObject.GetComponent<MonoBehaviourHook>().onUpdate=timer.Update;
        InitIfNeed();
        activeTimers.Add(timer);
        return timer;

    }
    public static void StopTimer(string timerName)
    {
        for(int i=0;i < activeTimers.Count; i++)
        {
            if (activeTimers[i].name==timerName)
            {
                activeTimers[i].OnDestroy();
                i--;
            }
        }
    }
    public static void StopTimer(FunctionTimer timer)
    {
        timer.OnDestroy();
    }

    private static void RemoveTimer(FunctionTimer timer)
    {
        InitIfNeed();
        activeTimers.Remove(timer);
    }

    public class FunctionTimer
    {
        private float time;
        private Action action;
        private GameObject gameObject;
        private bool isDestroyed;
        public string name;
        public FunctionTimer(Action action, float time, GameObject gameObject,string name)
        {
            this.action=action;
            this.time=time;
            this.isDestroyed=false;
            this.gameObject=gameObject;
            this.name=name;
        }

        public void Update()
        {
            if (isDestroyed)
            {
                return;
            }

            time-=Time.deltaTime;
            if (time <= 0)
            {
                action();
                OnDestroy();
            }
        }

        public void OnDestroy()
        {
            isDestroyed=true;
            TimerHandler.RemoveTimer(this);
            GameObject.Destroy(gameObject);
        }

    }
    
    private class MonoBehaviourHook : MonoBehaviour
    {
        public Action onUpdate;

        private void Update()
        {
            if (onUpdate != null)
            {
               onUpdate(); 
            }
        }
    }
}
