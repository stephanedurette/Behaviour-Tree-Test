using UnityEngine;
using UnityEngine.Events;
using Obvious.Soap;

[AddComponentMenu("Soap/EventListeners/EventListener"+nameof(JobComponent))]
public class EventListenerJobComponent : EventListenerGeneric<JobComponent>
{
    [SerializeField] private EventResponse[] _eventResponses = null;
    protected override EventResponse<JobComponent>[] EventResponses => _eventResponses;

    [System.Serializable]
    public class EventResponse : EventResponse<JobComponent>
    {
        [SerializeField] private ScriptableEventJobComponent _scriptableEvent = null;
        public override ScriptableEvent<JobComponent> ScriptableEvent => _scriptableEvent;

        [SerializeField] private JobComponentUnityEvent _response = null;
        public override UnityEvent<JobComponent> Response => _response;
    }

    [System.Serializable]
    public class JobComponentUnityEvent : UnityEvent<JobComponent>
    {
        
    }
}
