using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.Events;


public class EventManager : Singleton<EventManager>
{
    private Dictionary<string, UnityEvent> eventDictionary;
    public void Init()
    {
        if (eventDictionary == null)
        {
            eventDictionary = new Dictionary<string, UnityEvent>();
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
            //Debug.LogWarning("EventManager does not init");
            return;
        }
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
