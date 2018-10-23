using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GravitySwitcher : MonoBehaviour {

    public void ChangeGravity(Vector2 vector)
    {
        if (vector == Vector2.down || vector == Vector2.up || vector == Vector2.left || vector == Vector2.right) ApplicationController.gravity = vector;
    }
}
