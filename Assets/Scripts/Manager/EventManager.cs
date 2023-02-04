using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;
[System.Serializable]
public class EventDictionary : SerializableDictionary<string, UnityEvent>{

}


public class EventManager : Singleton<EventManager>
{
    [SerializeField]
    private EventDictionary eventDictionary;
    public void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new EventDictionary();
        }
    }
    
    private void OnDisable() {
        foreach(var item in eventDictionary){
            if(item.Value == null) return;
            item.Value.RemoveAllListeners();
        }
    }

    public static void AddEvent(string eventName, UnityAction listener){
        if(Instance == null){
            Debug.LogWarning("EventManager does not init");
            return;
        }
        Debug.Log(Instance);
        UnityEvent thisEvent = null;
        if(Instance.eventDictionary.TryGetValue(eventName, out thisEvent)){
            thisEvent.AddListener(listener);
        }
        else{
            thisEvent = new UnityEvent();
            thisEvent.AddListener(listener);
            Instance.eventDictionary.Add(eventName, thisEvent);
        }
    }   

    public static void RemoveEvent(string eventName, UnityAction listener){
        if(Instance == null){
            Debug.LogWarning("EventManager does not init");
            return;
        }
        if (!Instance.enabled){
            Debug.LogWarning("EventManager disabled");
            return;
        }
        UnityEvent thisEvent = null;
        if(Instance.eventDictionary.TryGetValue(eventName, out thisEvent)){
            thisEvent.RemoveListener(listener);
        }
    }
    public static void RemoveAllEvent(string eventName){
        if(Instance == null){
            Debug.LogWarning("EventManager does not init");
            return;
        }
        if (!Instance.enabled){
            Debug.LogWarning("EventManager disabled");
            return;
        }
        UnityEvent thisEvent = null;
        if(Instance.eventDictionary.TryGetValue(eventName, out thisEvent)){
            thisEvent.RemoveAllListeners();
        }
    }
    
    public static void Invoke(string eventName){
        UnityEvent thisEvent = null;
        if(Instance.eventDictionary.TryGetValue(eventName, out thisEvent)){
            thisEvent.Invoke();
        }
        else{
            Debug.LogWarning($"The event: {eventName} does not exist in the EventManager");
        }
    }

    
}
