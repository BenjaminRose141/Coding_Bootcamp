using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEditor;

[CustomEditor(typeof(PlatformMoving))]
public class PlatformMovingEditor : Editor
{
    Tool LastTool = Tool.None;
 
    void OnEnable()
    {
        LastTool = Tools.current;
        Tools.current = Tool.None;
    }
 
    void OnDisable()
    {
        Tools.current = LastTool;
    }
    
    PlatformMoving m_target;
    void OnSceneGUI()
    {
        m_target = (PlatformMoving)target;

        Handles.color = Color.yellow;

        Transform startPoint = m_target.transform.GetChild(0);
        Transform endPoint = m_target.transform.GetChild(1);

        startPoint.position = Handles.PositionHandle(startPoint.position, startPoint.rotation);
        endPoint.position = Handles.PositionHandle(endPoint.position, endPoint.rotation);

        Handles.DrawDottedLine(startPoint.position, endPoint.position, 4f);
    }
}
