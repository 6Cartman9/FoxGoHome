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

        // ѕроверка, достиг ли враг текущей целевой точки
        if (Vector2.Distance(transform.position, currentWaypoint.position) < 0.1f)
        {
            // –азворот врага, если он достиг текущей точки
            if (currentWaypointIndex == 0)
            {
                // ¬раг движетс€ влево к следующей точке
                transform.localScale = new Vector3(-1, 1);
            }
            else
            {
                // ¬раг движетс€ вправо к следующей точке
                transform.localScale = new Vector3(1, 1);
            }

            // ”величиваем индекс текущей точки
            currentWaypointIndex++;

            // ≈сли враг достиг последней точки, вернем его к началу
            if (currentWaypointIndex >= waypoints.Length)
            {
                currentWaypointIndex = 0;
            }
        }

        // ѕеремещаем врага к текущей целевой точке
        transform.position = Vector2.MoveTowards(transform.position, currentWaypoint.position, moveSpeed * Time.deltaTime);
    }

}