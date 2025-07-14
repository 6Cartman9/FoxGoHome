using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class EnemeisMoveController : Enemy
{
    [SerializeField] private Transform[] waypoints;
    [SerializeField] private float moveSpeed;
    private int currentWaypointIndex = 0;

    protected override void Start()
    {
        base.Start();
    }

    private void Update()
    {
        EnemyMove();
    }

    private void EnemyMove()
    {
        if (waypoints.Length == 0)
            return;

        Transform currentWaypoint = waypoints[currentWaypointIndex];

        // ��������, ������ �� ���� ������� ������� �����
        if (Vector2.Distance(transform.position, currentWaypoint.position) < 0.1f)
        {
            // �������� �����, ���� �� ������ ������� �����
            if (currentWaypointIndex == 0)
            {
                // ���� �������� ����� � ��������� �����
                transform.localScale = new Vector3(-1, 1);
            }
            else
            {
                // ���� �������� ������ � ��������� �����
                transform.localScale = new Vector3(1, 1);
            }

            // ����������� ������ ������� �����
            currentWaypointIndex++;

            // ���� ���� ������ ��������� �����, ������ ��� � ������
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }

        // ���������� ����� � ������� ������� �����
        transform.position = Vector2.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);
    }

}