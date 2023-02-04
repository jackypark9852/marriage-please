

# EventManager
1. To register UnityEvent, in OnEnable(), use EventManager.Instance.AddEvent(string EventName, UnityAction action)
2. To remove UnityEvent, in OnDisable(), use EventManager.Instance.RemoveEvent(string EventName, UnityAction action)
3. To trigger Event registered in UnityEvent, use EventManager.Instance.Invoke(string EventName)