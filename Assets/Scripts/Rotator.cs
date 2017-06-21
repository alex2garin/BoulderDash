using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Rotator : MonoBehaviour {

    public float rotationSpeed = 20f;

    private SpriteRenderer sr;
    private bool isRotating = false;

    public enum RotationSide { left, right, norotation };


	// Use this for initialization
	void Start () {
        sr = GetComponent<SpriteRenderer>();
	}
	

    public IEnumerator Rotate(RotationSide side)
    {
        isRotating = true;
        int sign = 0;
        if (side == RotationSide.left) sign = 1;
        else if (side == RotationSide.right) sign = -1;
        else isRotating = false;
        
        while (isRotating)
        {
            sr.transform.Rotate(new Vector3(0f, 0f, sign * rotationSpeed));//* Time.deltaTime);
            yield return null;
        }
    }

    public void StopRotation()
    {
        isRotating = false;
    }

    public static RotationSide GetRotationSide(Vector3 direction)
    {
          if (direction.x < 0) return RotationSide.left;
          else if (direction.x > 0) return RotationSide.right;
          else return RotationSide.norotation;

    }

}
