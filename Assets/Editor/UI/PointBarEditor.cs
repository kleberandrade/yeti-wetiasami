using UnityEditor;
using UnityEditor.AnimatedValues;
using UnityEngine;

[CustomEditor(typeof(PointBar))]
[CanEditMultipleObjects]
public class PointBarEditor : Editor
{
    public bool m_DebugMode;

    private float m_IncreaseValue;

    private float m_ReduceValue;

    private PointBar m_PointBar;

    private void OnEnable()
    {
        m_PointBar = (PointBar)target;
    }

    public override void OnInspectorGUI()
    {
        m_PointBar.m_MaxValue = EditorGUILayout.FloatField("Mac Value", m_PointBar.m_MaxValue);

        m_PointBar.m_UseTransitionTime = EditorGUILayout.Toggle("User Transition Time", m_PointBar.m_UseTransitionTime);

        if (m_PointBar.m_UseTransitionTime)
        {
            GUILayout.BeginHorizontal();
            GUILayout.Label("Transition Time");
            m_PointBar.m_TransitionTime = EditorGUILayout.Slider(m_PointBar.m_TransitionTime, 0.0f, 2.0f);
            GUILayout.EndHorizontal();
        }

        m_DebugMode = EditorGUILayout.Toggle("Debug Mode", m_DebugMode);

        if (m_DebugMode)
        {
            GUILayout.BeginVertical("box");
            EditorGUILayout.Space();

            GUILayout.BeginHorizontal();
            GUILayout.Label("Reduce   ");
            m_ReduceValue = EditorGUILayout.Slider(m_ReduceValue, 0.0f, m_PointBar.m_MaxValue);
            GUILayout.EndHorizontal();


            GUILayout.BeginHorizontal();
            GUILayout.Label("Increase ");
            m_IncreaseValue = EditorGUILayout.Slider(m_IncreaseValue, 0.0f, m_PointBar.m_MaxValue);
            GUILayout.EndHorizontal();

            EditorGUILayout.Space();

            GUILayout.BeginHorizontal();

            if (GUILayout.Button("Reduce"))
            {
                m_PointBar.ReduceValue(m_ReduceValue);
            }

            if (GUILayout.Button("Increase"))
            {
                m_PointBar.IncreaseValue(m_IncreaseValue);
            }

            GUILayout.EndHorizontal();

            GUILayout.EndVertical();
        }

        EditorGUILayout.Space();

        if (GUI.changed)
        {
            EditorUtility.SetDirty(m_PointBar);
            serializedObject.ApplyModifiedProperties();
        }
    }
}
