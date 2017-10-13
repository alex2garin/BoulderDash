using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using System.Linq;

public class ParasiteController : Enemy {

    public bool isLeftSideMove = true;

    protected override Vector3 GetDestination()
    {

        Collider2D leftNeighbour = null;
        Collider2D rightNeighbour = null;
        Collider2D upNeighbour = null;
        Collider2D downNeighbour = null;
        Collider2D upLeftNeighbour = null;
        Collider2D upRightNeighbour = null;
        Collider2D downLeftNeighbour = null;
        Collider2D downRightNeighbour = null;

        Vector2 tileSize = new Vector2(0.9f, 0.9f);


        //var neigbours = Physics2D.OverlapBoxAll(transform.position, new Vector2(1.9f, 1.9f), 0f);
        //foreach(var neighbourCollider in neigbours)
        //{
        //    if (neighbourCollider.transform.position.x == transform.position.x - 1 && neighbourCollider.transform.position.y == transform.position.y) leftNeighbour = neighbourCollider.gameObject;
        //    if (neighbourCollider.transform.position.x == transform.position.x + 1 && neighbourCollider.transform.position.y == transform.position.y) rightNeighbour = neighbourCollider.gameObject;
        //    if (neighbourCollider.transform.position.x == transform.position.x && neighbourCollider.transform.position.y == transform.position.y - 1) downNeighbour = neighbourCollider.gameObject;
        //    if (neighbourCollider.transform.position.x == transform.position.x && neighbourCollider.transform.position.y == transform.position.y + 1) upNeighbour = neighbourCollider.gameObject;

        //    if (neighbourCollider.transform.position.x == transform.position.x - 1 && neighbourCollider.transform.position.y == transform.position.y + 1) upLeftNeighbour = neighbourCollider.gameObject;
        //    if (neighbourCollider.transform.position.x == transform.position.x + 1 && neighbourCollider.transform.position.y == transform.position.y + 1) upRightNeighbour = neighbourCollider.gameObject;
        //    if (neighbourCollider.transform.position.x == transform.position.x - 1 && neighbourCollider.transform.position.y == transform.position.y - 1) downLeftNeighbour = neighbourCollider.gameObject;
        //    if (neighbourCollider.transform.position.x == transform.position.x + 1 && neighbourCollider.transform.position.y == transform.position.y - 1) downRightNeighbour = neighbourCollider.gameObject;
        //}

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

      

      //  foreach (var neoghbour in neighboursGO) Debug.Log(neoghbour);


        return CalculateVector(neighboursGO);

    }

    private Vector3 CalculateVector(GameObject[] neighbours)
    {
        //find base
        int basis = -1;
        for(int i=0;i<neighbours.Length;i++)
        {
            if (neighbours[i] != null) basis = i;
            else if (basis != -1) break;
        }
        
        if (basis == -1) return new Vector3(0f, -1f, 0f);

        return FindDirection(basis, neighbours);

    }

    private Vector3 FindDirection(int basis, GameObject[] neighbours)
    {
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
            else return FindDirection(basis + 2, neighbours);

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
    
}
