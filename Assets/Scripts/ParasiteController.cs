using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ParasiteController : Enemy
{

    public bool isLeftSideMove = true;

    //private bool stopFlag = false;
    private int lastBasis = -1;
    private Bomb bomb;

    //public bool StopFlag
    //{
    //    set
    //    {
    //        stopFlag = StopFlag;
    //    }
    //    get
    //    {
    //        return stopFlag;
    //    }
    //}

    protected override Vector3 GetDestination()
    {
      //  if (stopFlag) return Vector3.zero;

        Collider2D leftNeighbour = null;
        Collider2D rightNeighbour = null;
        Collider2D upNeighbour = null;
        Collider2D downNeighbour = null;
        Collider2D upLeftNeighbour = null;
        Collider2D upRightNeighbour = null;
        Collider2D downLeftNeighbour = null;
        Collider2D downRightNeighbour = null;


        Vector2 tileSize = new Vector2(0.9f, 0.9f);



        leftNeighbour = Physics2D.OverlapBox(transform.position + new Vector3(-1f, 0f), tileSize, 0f);
        rightNeighbour = Physics2D.OverlapBox(transform.position + new Vector3(1f, 0f), tileSize, 0f);
        upNeighbour = Physics2D.OverlapBox(transform.position + new Vector3(0f, 1f), tileSize, 0f);
        downNeighbour = Physics2D.OverlapBox(transform.position + new Vector3(0f, -1f), tileSize, 0f);
        upLeftNeighbour = Physics2D.OverlapBox(transform.position + new Vector3(-1f, 1f), tileSize, 0f);
        upRightNeighbour = Physics2D.OverlapBox(transform.position + new Vector3(1f, 1f), tileSize, 0f);
        downLeftNeighbour = Physics2D.OverlapBox(transform.position + new Vector3(-1f, -1f), tileSize, 0f);
        downRightNeighbour = Physics2D.OverlapBox(transform.position + new Vector3(1f, -1f), tileSize, 0f);



        GameObject[] neighboursGO = new GameObject[8];
        if (isLeftSideMove)
        {
            if (upNeighbour != null) neighboursGO[0] = upNeighbour.gameObject;
            if (upRightNeighbour != null) neighboursGO[1] = upRightNeighbour.gameObject;
            if (rightNeighbour != null) neighboursGO[2] = rightNeighbour.gameObject;
            if (downRightNeighbour != null) neighboursGO[3] = downRightNeighbour.gameObject;
            if (downNeighbour != null) neighboursGO[4] = downNeighbour.gameObject;
            if (downLeftNeighbour != null) neighboursGO[5] = downLeftNeighbour.gameObject;
            if (leftNeighbour != null) neighboursGO[6] = leftNeighbour.gameObject;
            if (upLeftNeighbour != null) neighboursGO[7] = upLeftNeighbour.gameObject;
        }
        else
        {
            if (upNeighbour != null) neighboursGO[0] = upNeighbour.gameObject;
            if (upRightNeighbour != null) neighboursGO[7] = upRightNeighbour.gameObject;
            if (rightNeighbour != null) neighboursGO[6] = rightNeighbour.gameObject;
            if (downRightNeighbour != null) neighboursGO[5] = downRightNeighbour.gameObject;
            if (downNeighbour != null) neighboursGO[4] = downNeighbour.gameObject;
            if (downLeftNeighbour != null) neighboursGO[3] = downLeftNeighbour.gameObject;
            if (leftNeighbour != null) neighboursGO[2] = leftNeighbour.gameObject;
            if (upLeftNeighbour != null) neighboursGO[1] = upLeftNeighbour.gameObject;
        }
        for (int i = 0; i < neighboursGO.Length; i++)
        {
            if (neighboursGO[i] == null) continue;
            if (neighboursGO[i].CompareTag("Player") || neighboursGO[i].CompareTag("Enemy"))
            {
                neighboursGO[i] = null;
                continue;
            }
            MovingObjectController moc = neighboursGO[i].GetComponent<MovingObjectController>();
            if (moc != null && !moc.CanBeUsed(transform.position)) neighboursGO[i] = null;

        }


        //Debug.Log("new line***************");
        //Debug.Log(isLeftSideMove);
       // foreach (var neoghbour in neighboursGO) Debug.Log(neoghbour);


        return CalculateVector(neighboursGO);

    }
    protected override void ActionAfterMovement()
    {
        
    }
    protected override void BeforeFixedUpdate()
    {
    }
    protected override Vector3 GetDestinationInMovement(Vector3 calculatedNewPosition)
    {
        return calculatedNewPosition;
    }
    protected override void RotationStart()
    {
    }

    private new void Start()
    {
        base.Start();
        bomb = GetComponent<Bomb>();
    }

    private Vector3 CalculateVector(GameObject[] neighbours)
    {
        // check if we can move
        bool exitExistsFlag = false;
        foreach (var neigbour in neighbours) if (neigbour == null) exitExistsFlag = true;
        if (!exitExistsFlag) return Vector3.zero;


        //find basis
        int basis = -1;
        int startingVal = 0;
        if (lastBasis > 0) startingVal = lastBasis;
        for (int i = 0; i < neighbours.Length; i++)
        {
            int index = (startingVal + i) % 8;

            if (neighbours[index] != null) basis = index;
            else if (basis != -1) break;
        }
        
      //  Debug.Log(basis);

 

        if (basis == -1)
        {
            lastBasis = -1;
            return ApplicationController.gravity;//new Vector3(0f, -1f, 0f);
        }

        var destin = FindDirection(basis, neighbours, out basis);
        if (basis == 0) lastBasis = 7;
        if (basis > 0) lastBasis = basis - 1;
        return destin;

    }

    private Vector3 FindDirection(int basis, GameObject[] neighbours, out int finishBasis)
    {
        finishBasis = basis;
        if (basis == 8) return Vector3.zero;

        if (basis % 2 == 0)
        {
            int postBasis = basis + 2;
            if (postBasis == 8) postBasis = 0;

            if (neighbours[postBasis] == null)
            {
                Vector3 direction = Vector3.zero;
                switch (postBasis)
                {
                    case 0:
                        if (isLeftSideMove)
                            direction = new Vector3(0f, 1f);
                        else direction = new Vector3(0f, 1f);
                        break;
                    case 2:
                        if (isLeftSideMove)
                            direction = new Vector3(1f, 0f);
                        else direction = new Vector3(-1f, 0f);
                        break;
                    case 4:
                        if (isLeftSideMove)
                            direction = new Vector3(0f, -1f);
                        else direction = new Vector3(0f, -1f);
                        break;
                    case 6:
                        if (isLeftSideMove)
                            direction = new Vector3(-1f, 0f);
                        else direction = new Vector3(1f, 0f);
                        break;
                }
                return direction;
            }
            else return FindDirection(basis + 2, neighbours, out finishBasis);

        }
        else
        {
            Vector3 direction = Vector3.zero;
            switch (basis)
            {
                case 1:
                    if (isLeftSideMove)
                        direction = new Vector3(1f, 0f);
                    else direction = new Vector3(-1f, 0f);
                    break;
                case 3:
                    if (isLeftSideMove)
                        direction = new Vector3(0f, -1f);
                    else direction = new Vector3(0f, -1f);
                    break;
                case 5:
                    if (isLeftSideMove)
                        direction = new Vector3(-1f, 0f);
                    else direction = new Vector3(1f, 0f);
                    break;
                case 7:
                    if (isLeftSideMove)
                        direction = new Vector3(0f, 1f);
                    else direction = new Vector3(0f, 1f);
                    break;
            }
            return direction;
        }
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        //Debug.Log(1);
        bomb.ShouldExplode();
   //     base.Stop();
    }
}
