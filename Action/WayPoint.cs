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

            // 탐지 범위 구형 기즈모 표시
            Gizmos.color = Color.green;
            Gizmos.DrawSphere(this.transform.GetChild(i).position, 0.1f);

            // 웨이포인트 경로 표시
            Gizmos.color = Color.white;
            Gizmos.DrawLine(GetWaypoint(i), GetWaypoint(next));
        }
    }

    public int GetNextIndex(int index)
    {
        // 자식 갯수 초과값으로 인한 오류 방지를 위해 나머지값 사용
        return (index + 1) % this.transform.childCount;
    }

    public Vector3 GetWaypoint(int index)
    {
        return this.transform.GetChild(index).position;
    }
}
