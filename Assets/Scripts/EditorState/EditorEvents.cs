using System;

namespace Assets.Scripts.System
{
    public class EditorEvents
    {
        public event Action onSelectionChangedEvent;
        public void SelectionChangedEvent() => onSelectionChangedEvent?.Invoke();
    }
}