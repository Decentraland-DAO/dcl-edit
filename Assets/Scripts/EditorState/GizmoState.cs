using Assets.Scripts.Events;
using Assets.Scripts.SceneState;
using UnityEngine;
using Zenject;

namespace Assets.Scripts.EditorState
{
    public class GizmoState
    {
        public class GizmoDirection
        {
            public readonly bool isX;
            public readonly bool isY;
            public readonly bool isZ;


            public GizmoDirection(Vector3Int gizmoDirection)
            {
                isX = gizmoDirection.x > 0;
                isY = gizmoDirection.y > 0;
                isZ = gizmoDirection.z > 0;
            }

            public bool isOnlyX()
            {
                return isX && !isY && !isZ;
            }

            public bool isOnlyY()
            {
                return !isX && isY && !isZ;
            }

            public bool isOnlyZ()
            {
                return !isX && !isY && isZ;
            }

            public bool isXandY()
            {
                return isX && isY && !isZ;
            }

            public bool isXandZ()
            {
                return isX && !isY && isZ;
            }

            public bool isYandZ()
            {
                return !isX && isY && isZ;
            }

            public bool isXYZ()
            {
                return isX && isY && isZ;
            }


            public Vector3 GetDirectionVector()
            {
                return new Vector3(isX ? 1 : 0, isY ? 1 : 0, isZ ? 1 : 0);
            }
        }

        public enum MouseContextRelevance
        {
            OnlyPrimaryAxis,
            EntirePlane
        }

        // Dependencies
        private EditorEvents _editorEvents;

        [Inject]
        private void Construct(EditorEvents editorEvents)
        {
            _editorEvents = editorEvents;
        }

        public enum Mode
        {
            Translate,
            Rotate,
            Scale
        }

        private Mode _currentMode;

        public Mode CurrentMode
        {
            get => _currentMode;
            set
            {
                _currentMode = value;
                _editorEvents.InvokeGizmoModeChangeEvent();
                _editorEvents.InvokeSelectionChangedEvent();
            }
        }

        // While moving states

        /// <summary>
        /// The transform, where the gizmo operations are applied to 
        /// </summary>
        public DclTransformComponent affectedTransform;

        /// <summary>
        /// Describes the direction of the held gizmo tool
        /// </summary>
        public GizmoDirection gizmoDirection;

        /// <summary>
        /// Describes the center point of the mouse context
        /// </summary>
        /// <remarks>
        /// Mouse position context. The center and the two Vectors describe the plane, on that the mouse position will be mapped in 3D space
        /// </remarks>
        public Vector3 mouseContextCenter;

        /// <summary>
        /// Describes the X Vector. When the mouse points exactly at this vectors position, the resulting mouse movement coordinate will be (1,0). 
        /// </summary>
        /// <remarks>
        /// Mouse position context. The center and the two Vectors describe the plane, on that the mouse position will be mapped in 3D space
        /// </remarks>
        public Vector3 mouseContextPrimaryVector;

        /// <summary>
        /// Describes the Y Vector. When the mouse points exactly at this vectors position, the resulting mouse movement coordinate will be (0,1). 
        /// </summary>
        /// <remarks>
        /// Mouse position context. The center and the two Vectors describe the plane, on that the mouse position will be mapped in 3D space
        /// </remarks>
        public Vector3 mouseContextSecondaryVector;

        /// <summary>
        /// Tells, if only one axis or the entire plane is relevant
        /// </summary>
        public MouseContextRelevance mouseContextRelevance;

        /// <summary>
        /// The plane, that is defined by the position and the vectors
        /// </summary>
        public Plane mouseContextPlane => new Plane(mouseContextCenter, mouseContextCenter + mouseContextPrimaryVector, mouseContextCenter + mouseContextSecondaryVector);

        /// <summary>
        /// The starting position of the mouse on the plane
        /// </summary>
        public Vector3 mouseStartingPosition;
    }
}