/*
 * Created on 2024
 *
 * Copyright (c) 2024 dotmobstudio
 * Support : dotmobstudio@gmail.com
 */
using UnityEngine;
using System.Collections;
using System.Collections.Generic;


public class CoroutineHelper : MonoBehaviour {

    public delegate void Task();

    private static CoroutineHelper instance = null;

    
    public static CoroutineHelper Instance
    {
        get {
            if (instance == null) {
                instance = new GameObject("Coroutine Helper").AddComponent<CoroutineHelper>();
                DontDestroyOnLoad(instance);
            }
            return instance;
        }
    }

    
    public static CoroutineHelper GetInstance(Object obj) {
        if (obj == null) {
            return Instance;
        }

        else if (obj is CoroutineHelper) {
            return obj as CoroutineHelper;
        }
        else if (obj is GameObject || obj is Component || obj is Transform) {
            return ((GameObject)obj).GetComponent<CoroutineHelper>();
        }
        else {
            return Instance;
        }
    }

    private bool excutingQueue = false;
    private List<IEnumerator> taskQueue = new List<IEnumerator>();

    private void Update() {
        
    }

    private void OnDisable() {
        taskQueue.Clear();
        excutingQueue = false;
    }

    void OnDestroy() {
        if (instance == this) {
            instance = null;
        }
    }

    public void Stop(Coroutine coroutine) {
        if (coroutine != null) {
            StopCoroutine(coroutine);
        }
    }

    public void StopAll() {
        StopAllCoroutines();
        taskQueue.Clear();
        excutingQueue = false;
    }

    public Coroutine ExcuteTask(Task task) {
        return StartCoroutine(_ExcuteTask(task));
    }
    private IEnumerator _ExcuteTask(Task task) {
        if (task != null) {
            try
            {
                task.Invoke();

            }
            catch (System.Exception e)
            {
                Debug.Log(e.StackTrace);
                throw;
            }
        }
        else {
            yield return true;
        }
    }

    public Coroutine WaitForEndOfFrame(Task task) {
        return StartCoroutine(_WaitForEndOfFrame(task));
    }
    private IEnumerator _WaitForEndOfFrame(Task task) {
        yield return new WaitForEndOfFrame();
        if (task != null) {           
             task.Invoke();
        }
    }

    public Coroutine WaitForSeconds(float delay, Task task) {
        return StartCoroutine(_WaitForSeconds(delay, task));
    }
    private IEnumerator _WaitForSeconds(float delay, Task task) {
        yield return new WaitForSeconds(delay);
        if (task != null) {
             task.Invoke();
        }
    }

    public void QueueWaitForSeconds(float delay, Task task) {
        taskQueue.Add(_WaitForSeconds(delay, task));

        if (!excutingQueue) {
            excutingQueue = true;
            StartCoroutine(ExcuteQueue());
        }
    }

    public void QueueWaitForEndOfFrame(Task task) {
        taskQueue.Add(_WaitForEndOfFrame(task));

        if (!excutingQueue) {
            excutingQueue = true;
            StartCoroutine(ExcuteQueue());
        }
    }

    private IEnumerator ExcuteQueue() {
        while(taskQueue.Count > 0) {
            IEnumerator task = taskQueue[0];
            taskQueue.RemoveAt(0);
            yield return task;
        }
        excutingQueue = false;
    }

}
