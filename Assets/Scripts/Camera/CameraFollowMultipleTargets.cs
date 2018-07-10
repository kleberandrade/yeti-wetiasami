using System.Collections.Generic;
using UnityEngine;

public class CameraFollowMultipleTargets : MonoBehaviour
{
    public List<Transform> m_Targets;

    public Vector3 m_Offset;

    public float m_SmoothTime = 0.5f;

    [Header("Zoom")]
    public float m_MinZoom = 5.0f;

    public float m_MaxZoom = 20.0f;

    private Vector3 m_Velocity;

    private Vector3 m_CenterPoint;

	private void LateUpdate ()
    {
        if (m_Targets.Count == 0)
            return;

        Move();
        Zoom();
	}

    private void Move()
    {
        m_CenterPoint = GetCenterPoint();

        Vector3 newPosition = m_CenterPoint + m_Offset;

        transform.position = Vector3.SmoothDamp(transform.position, newPosition, ref m_Velocity, m_SmoothTime);
    }

    private void Zoom()
    {
        Debug.Log(GetGreatestDistance().ToString());
    }

    private Vector3 GetGreatestDistance()
    {
        return GetBound().size;
    }

    private Vector3 GetCenterPoint()
    {
        if (m_Targets.Count == 1)
            return m_Targets[0].position;

        return GetBound().center;
    }

    private Bounds GetBound()
    {
        Bounds bounds = new Bounds();
        for (int i = 0; i < m_Targets.Count; i++)
            bounds.Encapsulate(m_Targets[i].position);

        return bounds;
    }

    private void OnDrawGizmos()
    {
        Gizmos.DrawSphere(m_CenterPoint, 1.0f);
    }
}
