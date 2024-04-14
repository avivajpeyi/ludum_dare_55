using System.Collections;
using System.Collections.Generic;
using Unity.VisualScripting;
using UnityEngine;

public class GoalPointer : StaticInstance<GoalPointer> {
    private Vector3 targetPosition {
        get {
            if (currentGoal == null)
                return Vector3.zero;

            return new Vector3(currentGoal.transform.position.x, currentGoal.transform.position.y, 0);
        }
    }
    private GameObject pointer;
    public GameObject currentGoal;

    public float offsetDistanceFromPlayer = 2f;


    private void Awake() {
        pointer = transform.Find("Pointer").gameObject;
    }



    private void Update() {
        Vector3 toPosition = targetPosition;
        Vector3 fromPosition = new Vector3(Player.Instance.transform.position.x, Player.Instance.transform.position.y);
        Vector3 dir = (toPosition - fromPosition).normalized;
        //pointerRectTransform.localEulerAngles = new Vector3(0,0,-90f); //Rotates the direction the pointer points in.
        // 90f is a placeholder which points it to the right and should be replaced with the value of the angle between the player and goal
         pointer.transform.position = fromPosition + dir * offsetDistanceFromPlayer;
         Color color = Color.red;
        Debug.DrawLine(fromPosition, toPosition, color);
    }
}