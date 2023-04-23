using System.Collections;
using System.Collections.Generic;
using UnityEditor;
using UnityEngine;

public class MovingObstacle : MonoBehaviour
{
    [System.Serializable]
    public struct TimePositionPair
    {
        public int time;
        public Vector3 position;
    }

    public TimePositionPair[] timePositions = new TimePositionPair[0];

    private TimeResponder timeResponder;
    private Movable movable;

    private void Awake()
    {
        movable = GetComponent<Movable>();
        timeResponder = GetComponent<TimeResponder>();
        timeResponder.onTimeChanged.AddListener(OnTimeChanged);
    }

    private void OnTimeChanged(int newTime, int offset)
    {
        Vector3 targetPosition = movable.effectivePosition;
        for (int i = 0; i < timePositions.Length; i++)
        {
            if (timePositions[i].time <= newTime)
                targetPosition = timePositions[i].position;
        }

        if (targetPosition != movable.effectivePosition)
            movable.MoveTo(targetPosition);
    }

    private void OnDrawGizmos()
    {
        Gizmos.color = Color.yellow;

        for (int i = 1; i < timePositions.Length; i++)
            Gizmos.DrawLine(timePositions[i - 1].position, timePositions[1].position);

        GUIStyle labelStyle = new GUIStyle(GUI.skin.label);
        labelStyle.fontSize = 58;
        Handles.color = Color.yellow;
        for (int i = 0; i < timePositions.Length; i++)
        {
            Handles.SphereHandleCap(0, timePositions[i].position, Quaternion.identity, 0.25f, EventType.Repaint);
            Handles.zTest = UnityEngine.Rendering.CompareFunction.Always;
            Handles.Label(timePositions[i].position, i.ToString(), labelStyle);
        }
    }
}
