using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WayPoint : MonoBehaviour
{
    private void OnDrawGizmos()
    {
        for (int i = 0; i < this.transform.childCount; i++)
        {
            int next = GetNextIndex(i);
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(this.transform.GetChild(i).position, 0.1f);

            Gizmos.color = Color.white;
            Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(next));
        }
    }

    public int GetNextIndex(int index)
    {
        return (index + 1) % this.transform.childCount;
    }

    public Vector3 GetWaypoint(int index)
    {
        return this.transform.GetChild(index).position;
    }
}
