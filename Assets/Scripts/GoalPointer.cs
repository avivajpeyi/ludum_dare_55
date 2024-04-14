using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoalPointer : StaticInstance<GoalPointer> {
    private Vector3 targetPosition {
        get {
            if (currentGoal == null)
                return Vector3.zero;

            return new Vector3(currentGoal.transform.position.x, currentGoal.transform.position.y);
        }
    }
    private GameObject pointer;    public GameObject currentGoal;

    public float offsetDistanceFromPlayer = 2f;


    private void Awake() {
        pointer = transform.Find("Pointer").gameObject;
    }



    private void Update() {
        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = new Vector3(Player.Instance.transform.position.x, Player.Instance.transform.position.y);
        Vector3 dir = (toPosition - fromPosition).normalized;
        
        float angle = (Mathf.Atan2(dir.y, dir.x) - 89.5f) * Mathf.Rad2Deg;
        pointer.transform.rotation = Quaternion.Euler(0, 0, angle);

        pointer.transform.position = fromPosition + dir * offsetDistanceFromPlayer;
        
        // Debug red line drawn between player and goal
        Color color = Color.red;
        Debug.DrawLine(fromPosition, toPosition, color);
    }
}