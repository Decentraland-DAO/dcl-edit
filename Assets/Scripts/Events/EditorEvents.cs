using System;

namespace Assets.Scripts.Events
{
    public class EditorEvents
    {
        public event Action onSelectionChangedEvent;
        public void InvokeSelectionChangedEvent() => onSelectionChangedEvent?.Invoke();


        public event Action onCameraStateChangedEvent;
        public void InvokeCameraStateChangedEvent() => onCameraStateChangedEvent?.Invoke();


        public event Action onHoverChangedEvent;
        public void InvokeHoverChangedEvent() => onHoverChangedEvent?.Invoke();


        public event Action onHierarchyChangedEvent;
        public void InvokeHierarchyChangedEvent() => onHierarchyChangedEvent?.Invoke();


        public event Action onSettingsChangedEvent;
        public void InvokeSettingsChangedEvent() => onSettingsChangedEvent?.Invoke();


        public event Action onUpdateMenuBarEvent;
        public void InvokeUpdateMenuBarEvent() => onUpdateMenuBarEvent?.Invoke();


        public event Action onUpdateContextMenuEvent;
        public void InvokeUpdateContextMenuEvent() => onUpdateContextMenuEvent?.Invoke();


        public event Action<int> onMouseButtonDownEvent;
        public void InvokeOnMouseButtonDownEvent(int button) => onMouseButtonDownEvent?.Invoke(button);
    }
}