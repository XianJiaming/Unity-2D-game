using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System;
using Object = System.Object;

public class TimeManager
{

    static Dictionary<int, TimeHelper> timeHelperList = new Dictionary<int, TimeHelper>();
    public static TimeHelper GetTimer(int id)
    {
        return timeHelperList[id];

    }


    static int timeId = 0;
    /// <summary>
    /// 在一定时间后，每隔固定时间执行某个回调方法
    /// </summary>
    /// <param name="callBack">回调方法</param>
    /// <param name="whenTime">多少秒后执行</param>
    /// <param name="interval">每多少秒执行一次</param>
    public static int Regist(Action<int> callBack,float whenTime,
        float interval, int times, bool needStop = false)
    {
        
        TimeHelper timeH;
        timeId++;
        
        if (poor.Count>0)
        {
            timeH = poor[poor.Count - 1];
            poor.RemoveAt(poor.Count - 1);
            timeH.Init(callBack, whenTime, interval, times, needStop);
        }
        else
        {
            timeH = new TimeHelper(callBack, whenTime, interval, times, needStop);
        }           
        timeHelperList.Add(timeId,timeH);
        return timeId;
    }

    static List<TimeHelper> poor = new List<TimeHelper>();


    public static int RegistOneTime(Action<int> callBack, float time, bool need = false)
    {
        return Regist(callBack, time, 0, 1, need);
    }

    public static void Update()
    {

        if (timeHelperList.Count>0)
        {
            List<TimeHelper> tempList = new List<TimeHelper>();
            foreach(var v in timeHelperList.Values)
            {
                tempList.Add(v);
            }
            
            for (int i=0;i< tempList.Count;i++)
            {
                var help = tempList[i];
                if (timeHelperList.ContainsValue(help))
                {

                    help.TryRun();
                    if(help.currentTimes>= help.times)
                    {

                        Remove(help.TimeId);
                    }
                }
                                                         
            }
        }
        
    }
    public static void Remove(int id)
    {
        if(timeHelperList.ContainsKey(id))
        {

            var timer = timeHelperList[id];
            timer.OnRemove();
            poor.Add(timer);
            
            timeHelperList.Remove(id);
        }
        
        if(timeHelperList.Count == 0)
        {
            timeId = 0;
        }
        
    }
    //static List<int> removeIdList = new List<int>();
    public static void SetStop(string id)
    {
        for (int i = 0; i < timeHelperList.Count;i++)
        {
            if (timeHelperList[i].TimeId.Equals(id))
            {
                timeHelperList[i].isStop = true;
                return;
            }
        }
        Debug.LogError($"当前计时方法不存在--{id}");
    }

    public static void SetStart(string id)
    {
        for (int i = 0; i < timeHelperList.Count; i++)
        {
            if (timeHelperList[i].TimeId.Equals(id))
            {
                timeHelperList[i].isStop = false;
                return;
            }
        }
        Debug.LogError($"当前计时方法不存在--{id}");
    }


    public class TimeHelper
    {
        Action<int> callBack;
        public float whenTime; //什么时候执行
        float interval;//间隔时间
        public float currentInterval;
        public int times; //执行多少次
        public int currentTimes; //当前次数
       
        public float birthTime;

        int timeId;
        public int TimeId
        {
            get
            {
                //还未设置timeid算法
                return timeId;
            }
            set
            {
                timeId = value;
            }
        }

        public void Init(Action<int> callBack, float whenTime, float interval, int times, bool needStopInFight = false)
        {
            this.callBack = callBack;
            this.whenTime = whenTime;
            this.interval = interval;
            this.times = times;


            birthTime = Time.time;
            TimeId = TimeManager.timeId;
            currentTimes = 0;
            currentInterval = 0;

            needStopInFihgtAnyOneAct = needStopInFight;
        }

        public bool isStop = false;

        public TimeHelper(Action<int> callBack,float whenTime,float interval,int times,bool needStopInFihgt = false)
        {
            Init(callBack, whenTime, interval, times, needStopInFihgt);
        }

        public void Run()
        {
            callBack(TimeId);
            currentInterval = 0;
            currentTimes++;
            
        }
        public void TryRun()
        {
            UpdateStop();
            if (isStop)
            {
               
                return;
            }
            if (Time.time - birthTime < whenTime)
                return;

            currentInterval += Time.deltaTime;

            if (currentInterval >= interval)
                Run();
        }

        //只限于该项目中与fightManager耦合
        bool needStopInFihgtAnyOneAct = false;

        public void SetNeedStopInFihgtAnyOneAct(bool value )
        {
            needStopInFihgtAnyOneAct = value;
        }

        void UpdateStop()
        {
            if(needStopInFihgtAnyOneAct)
            {
                if( FightManager.instance.isAnyoneAct)
                {
                    isStop = true;
                }
                else
                {
                    isStop = false;
                }
            }
        }


        public void OnRemove()
        {
            callBack = null;
        }
    }
    
}
