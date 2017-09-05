﻿using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CornersAndBordersController : MonoBehaviour {

    public GameObject tileBorderLeft;
    public GameObject tileBorderRight;
    public GameObject tileBorderUp;
    public GameObject tileBorderDown;

    public GameObject tileCornerUpLeft;
    public GameObject tileCornerUpRight;
    public GameObject tileCornerDownLeft;
    public GameObject tileCornerDownRight;


    public Sprite borderUpLeftCorner;
    public Sprite borderUpRightCorner;
    public Sprite borderDownLeftCorner;
    public Sprite borderDownRightCorner;

    public Sprite wallUpLeftCorner;
    public Sprite wallUpRightCorner;
    public Sprite wallDownLeftCorner;
    public Sprite wallDownRightCorner;

    public Sprite groundUpLeftCorner;
    public Sprite groundUpRightCorner;
    public Sprite groundDownLeftCorner;
    public Sprite groundDownRightCorner;

    public Sprite borderBorderLeft;
    public Sprite borderBorderRight;
    public Sprite borderBorderUp;
    public Sprite borderBorderDown;
    public Sprite borderBorderLeftGR;
    public Sprite borderBorderRightGR;
    public Sprite borderBorderUpGR;
    public Sprite borderBorderDownGR;

    public Sprite wallBorderLeft;
    public Sprite wallBorderRight;
    public Sprite wallBorderUp;
    public Sprite wallBorderDown;
    public Sprite wallBorderLeftGR;
    public Sprite wallBorderRightGR;
    public Sprite wallBorderUpGR;
    public Sprite wallBorderDownGR;

    public Sprite groundBorderLeft;
    public Sprite groundBorderRight;
    public Sprite groundBorderUp;
    public Sprite groundBorderDown;
    public Sprite groundBorderLeftGR;
    public Sprite groundBorderRightGR;
    public Sprite groundBorderUpGR;
    public Sprite groundBorderDownGR;

    public bool needToUpdate;
    // Use this for initialization
    void Start () {
        needToUpdate = false;
    }

    // Update is called once per frame
    void FixedUpdate () {
        if (needToUpdate)
        {
            UpdateCornersAndBorders();
            needToUpdate = false;
        }
    }

    private void UpdateCornersAndBorders()
    {
        for (int i = 0; i < transform.childCount; i++) Destroy(transform.GetChild(i).gameObject);


        GameObject leftNeighbour = null;
        GameObject rightNeighbour = null;
        GameObject upNeighbour = null;
        GameObject downNeighbour = null;
        GameObject upLeftNeighbour = null;
        GameObject upRightNeighbour = null;
        GameObject downLeftNeighbour = null;
        GameObject downRightNeighbour = null;
        Collider2D[] surroundings = Physics2D.OverlapBoxAll(transform.position, new Vector2(1.9f, 1.9f), 0f);
        foreach(var neighbour in surroundings)
        {
            if (neighbour.transform.position.x == transform.position.x - 1 && neighbour.transform.position.y == transform.position.y) leftNeighbour = neighbour.gameObject;
            if (neighbour.transform.position.x == transform.position.x + 1 && neighbour.transform.position.y == transform.position.y) rightNeighbour = neighbour.gameObject;
            if (neighbour.transform.position.x == transform.position.x && neighbour.transform.position.y == transform.position.y - 1) downNeighbour = neighbour.gameObject;
            if (neighbour.transform.position.x == transform.position.x && neighbour.transform.position.y == transform.position.y + 1) upNeighbour = neighbour.gameObject;

            if (neighbour.transform.position.x == transform.position.x - 1 && neighbour.transform.position.y == transform.position.y + 1) upLeftNeighbour = neighbour.gameObject;
            if (neighbour.transform.position.x == transform.position.x + 1 && neighbour.transform.position.y == transform.position.y + 1) upRightNeighbour = neighbour.gameObject;
            if (neighbour.transform.position.x == transform.position.x - 1 && neighbour.transform.position.y == transform.position.y - 1) downLeftNeighbour = neighbour.gameObject;
            if (neighbour.transform.position.x == transform.position.x + 1 && neighbour.transform.position.y == transform.position.y - 1) downRightNeighbour = neighbour.gameObject;
        }

        
        
        /////////////////////////////////////////////////////
        
        if (leftNeighbour == null)
        {
            if (CompareTag("Border"))
            {
                tileBorderLeft.GetComponent<SpriteRenderer>().sprite = borderBorderLeft;
            }
            else if (CompareTag("Wall"))
            {
                tileBorderLeft.GetComponent<SpriteRenderer>().sprite = wallBorderLeft;
            }
            else if (CompareTag("Ground"))
            {
                tileBorderLeft.GetComponent<SpriteRenderer>().sprite = groundBorderLeft;
            }
            Instantiate(tileBorderLeft, transform.position + new Vector3(-0.5f, 0f, 0f), Quaternion.identity, transform);
        }
        else if (leftNeighbour.CompareTag("Border"))
        {

        }
        else if (leftNeighbour.CompareTag("Wall"))
        {
            if (CompareTag("Border"))
            {
                tileBorderLeft.GetComponent<SpriteRenderer>().sprite = borderBorderLeftGR;
                Instantiate(tileBorderLeft, transform.position + new Vector3(-0.5f, 0f, 0f), Quaternion.identity, transform);
            }
        }
        else if (leftNeighbour.CompareTag("Ground"))
        {
            if (CompareTag("Wall") || CompareTag("Border"))
            {
                if (CompareTag("Wall"))
                {
                    tileBorderLeft.GetComponent<SpriteRenderer>().sprite = wallBorderLeftGR;
                }
                else if (CompareTag("Border"))
                {
                    tileBorderLeft.GetComponent<SpriteRenderer>().sprite = borderBorderLeftGR;
                }
                Instantiate(tileBorderLeft, transform.position + new Vector3(-0.5f, 0f, 0f), Quaternion.identity, transform);
            }
        }
        else
        {
            if (CompareTag("Border"))
            {
                tileBorderLeft.GetComponent<SpriteRenderer>().sprite = borderBorderLeft;
            }
            else if (CompareTag("Wall"))
            {
                tileBorderLeft.GetComponent<SpriteRenderer>().sprite = wallBorderLeft;
            }
            else if (CompareTag("Ground"))
            {
                tileBorderLeft.GetComponent<SpriteRenderer>().sprite = groundBorderLeft;
            }
            Instantiate(tileBorderLeft, transform.position + new Vector3(-0.5f, 0f, 0f), Quaternion.identity, transform);
        }

        /////////////////////////////////////////////////////
        if (rightNeighbour == null)
        {
            if (CompareTag("Border"))
            {
                tileBorderRight.GetComponent<SpriteRenderer>().sprite = borderBorderRight;
            }
            else if (CompareTag("Wall"))
            {
                tileBorderRight.GetComponent<SpriteRenderer>().sprite = wallBorderRight;
            }
            else if (CompareTag("Ground"))
            {
                tileBorderRight.GetComponent<SpriteRenderer>().sprite = groundBorderRight;
            }
            Instantiate(tileBorderRight, transform.position + new Vector3(0.5f, 0f, 0f), Quaternion.identity, transform);
        }
        else if (rightNeighbour.CompareTag("Border"))
        {

        }
        else if (rightNeighbour.CompareTag("Wall"))
        {
            if (CompareTag("Border"))
            {
                tileBorderRight.GetComponent<SpriteRenderer>().sprite = borderBorderRightGR;
                Instantiate(tileBorderRight, transform.position + new Vector3(0.5f, 0f, 0f), Quaternion.identity, transform);
            }
        }
        else if (rightNeighbour.CompareTag("Ground"))
        {
            if (CompareTag("Wall") || CompareTag("Border"))
            {
                if (CompareTag("Border"))
                {
                    tileBorderRight.GetComponent<SpriteRenderer>().sprite = borderBorderRightGR;
                }
                else if (CompareTag("Wall"))
                {
                    tileBorderRight.GetComponent<SpriteRenderer>().sprite = wallBorderRightGR;
                }
                Instantiate(tileBorderRight, transform.position + new Vector3(0.5f, 0f, 0f), Quaternion.identity, transform);
            }
        }
        else
        {
            if (CompareTag("Border"))
            {
                tileBorderRight.GetComponent<SpriteRenderer>().sprite = borderBorderRight;
            }
            else if (CompareTag("Wall"))
            {
                tileBorderRight.GetComponent<SpriteRenderer>().sprite = wallBorderRight;
            }
            else if (CompareTag("Ground"))
            {
                tileBorderRight.GetComponent<SpriteRenderer>().sprite = groundBorderRight;
            }
            Instantiate(tileBorderRight, transform.position + new Vector3(0.5f, 0f, 0f), Quaternion.identity, transform);
        }

        /////////////////////////////////////////////////////
        if (upNeighbour == null)
        {
            if (CompareTag("Border"))
            {
                tileBorderUp.GetComponent<SpriteRenderer>().sprite = borderBorderUp;
            }
            else if (CompareTag("Wall"))
            {
                tileBorderUp.GetComponent<SpriteRenderer>().sprite = wallBorderUp;
            }
            else if (CompareTag("Ground"))
            {
                tileBorderUp.GetComponent<SpriteRenderer>().sprite = groundBorderUp;
            }
            Instantiate(tileBorderUp, transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity, transform);
        }
        else if (upNeighbour.CompareTag("Border"))
        {

        }
        else if (upNeighbour.CompareTag("Wall"))
        {
            if (CompareTag("Border"))
            {
                tileBorderUp.GetComponent<SpriteRenderer>().sprite = borderBorderUpGR;
                Instantiate(tileBorderUp, transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity, transform);
            }
        }
        else if (upNeighbour.CompareTag("Ground"))
        {
            if (CompareTag("Wall") || CompareTag("Border"))
            {
                if (CompareTag("Border"))
                {
                    tileBorderUp.GetComponent<SpriteRenderer>().sprite = borderBorderUpGR;
                }
                else if (CompareTag("Wall"))
                {
                    tileBorderUp.GetComponent<SpriteRenderer>().sprite = wallBorderUpGR;
                }
                Instantiate(tileBorderUp, transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity, transform);
            }
        }
        else
        {
            if (CompareTag("Border"))
            {
                tileBorderUp.GetComponent<SpriteRenderer>().sprite = borderBorderUp;
            }
            else if (CompareTag("Wall"))
            {
                tileBorderUp.GetComponent<SpriteRenderer>().sprite = wallBorderUp;
            }
            else if (CompareTag("Ground"))
            {
                tileBorderUp.GetComponent<SpriteRenderer>().sprite = groundBorderUp;
            }
            Instantiate(tileBorderUp, transform.position + new Vector3(0f, 0.5f, 0f), Quaternion.identity, transform);
        }

        /////////////////////////////////////////////////////
        if (downNeighbour == null)
        {
            if (CompareTag("Border"))
            {
                tileBorderDown.GetComponent<SpriteRenderer>().sprite = borderBorderDown;
            }
            else if (CompareTag("Wall"))
            {
                tileBorderDown.GetComponent<SpriteRenderer>().sprite = wallBorderDown;
            }
            else if (CompareTag("Ground"))
            {
                tileBorderDown.GetComponent<SpriteRenderer>().sprite = groundBorderDown;
            }
            Instantiate(tileBorderDown, transform.position + new Vector3(0f, -0.5f, 0f), Quaternion.identity, transform);
        }
        else if (downNeighbour.CompareTag("Border"))
        {

        }
        else if (downNeighbour.CompareTag("Wall"))
        {
            if (CompareTag("Border"))
            {
                tileBorderDown.GetComponent<SpriteRenderer>().sprite = borderBorderDownGR;
                Instantiate(tileBorderDown, transform.position + new Vector3(0f, -0.5f, 0f), Quaternion.identity, transform);
            }
        }
        else if (downNeighbour.CompareTag("Ground"))
        {
            if (CompareTag("Wall") || CompareTag("Border"))
            {
                if (CompareTag("Border"))
                {
                    tileBorderDown.GetComponent<SpriteRenderer>().sprite = borderBorderDownGR;
                }
                else if (CompareTag("Wall"))
                {
                    tileBorderDown.GetComponent<SpriteRenderer>().sprite = wallBorderDownGR;
                }
                Instantiate(tileBorderDown, transform.position + new Vector3(0f, -0.5f, 0f), Quaternion.identity, transform);
            }
        }
        else
        {
            if (CompareTag("Border"))
            {
                tileBorderDown.GetComponent<SpriteRenderer>().sprite = borderBorderDown;
            }
            else if (CompareTag("Wall"))
            {
                tileBorderDown.GetComponent<SpriteRenderer>().sprite = wallBorderDown;
            }
            else if (CompareTag("Ground"))
            {
                tileBorderDown.GetComponent<SpriteRenderer>().sprite = groundBorderDown;
            }
            Instantiate(tileBorderDown, transform.position + new Vector3(0f, -0.5f, 0f), Quaternion.identity, transform);
        }

        //////
        
        if (CompareTag("Border"))
        {
            tileCornerUpLeft.GetComponent<SpriteRenderer>().sprite = borderUpLeftCorner;
        }
        else if (CompareTag("Wall"))
        {
            tileCornerUpLeft.GetComponent<SpriteRenderer>().sprite = wallUpLeftCorner;
        }
        else if (CompareTag("Ground"))
        {
            tileCornerUpLeft.GetComponent<SpriteRenderer>().sprite = groundUpLeftCorner;
        }
        if (leftNeighbour != null && upNeighbour != null && leftNeighbour.CompareTag(gameObject.tag) && upNeighbour.CompareTag(gameObject.tag))
        {
            if (upLeftNeighbour == null)
                Instantiate(tileCornerUpLeft, transform.position + new Vector3(-0.5f, 0.5f, 0f), Quaternion.identity, transform);

            else if (upLeftNeighbour.CompareTag("Border"))
            {

            }
            else if (upLeftNeighbour.CompareTag("Wall") && CompareTag("Border"))
                Instantiate(tileCornerUpLeft, transform.position + new Vector3(-0.5f, 0.5f, 0f), Quaternion.identity, transform);

            else if (upLeftNeighbour.CompareTag("Ground") && (CompareTag("Border") || CompareTag("Wall")))
                Instantiate(tileCornerUpLeft, transform.position + new Vector3(-0.5f, 0.5f, 0f), Quaternion.identity, transform);

            //else Instantiate(tileCornerUpLeft, transform.position + new Vector3(-0.5f, 0.5f, 0f), Quaternion.identity, transform);
        }
        
        /////////////////////////////////////////////////////////////
        if (CompareTag("Border"))
        {
            tileCornerUpRight.GetComponent<SpriteRenderer>().sprite = borderUpRightCorner;
        }
        else if (CompareTag("Wall"))
        {
            tileCornerUpRight.GetComponent<SpriteRenderer>().sprite = wallUpRightCorner;
        }
        else if (CompareTag("Ground"))
        {
            tileCornerUpRight.GetComponent<SpriteRenderer>().sprite = groundUpRightCorner;
        }
        if (rightNeighbour != null && upNeighbour != null && rightNeighbour.CompareTag(gameObject.tag) && upNeighbour.CompareTag(gameObject.tag))
        {
            if (upRightNeighbour == null)
                Instantiate(tileCornerUpRight, transform.position + new Vector3(0.5f, 0.5f, 0f), Quaternion.identity, transform);
            else if (upRightNeighbour.CompareTag("Border"))
            {

            }
            else if (upRightNeighbour.CompareTag("Wall") && CompareTag("Border"))
                Instantiate(tileCornerUpRight, transform.position + new Vector3(0.5f, 0.5f, 0f), Quaternion.identity, transform);
            else if (upRightNeighbour.CompareTag("Ground") && (CompareTag("Border") || CompareTag("Wall")))
                Instantiate(tileCornerUpRight, transform.position + new Vector3(0.5f, 0.5f, 0f), Quaternion.identity, transform);
            //else Instantiate(tileCornerUpRight, transform.position + new Vector3(0.5f, 0.5f, 0f), Quaternion.identity, transform);
        }

        /////////////////////////////////////////////////////////////
        if (CompareTag("Border"))
        {
            tileCornerDownLeft.GetComponent<SpriteRenderer>().sprite = borderDownLeftCorner;
        }
        else if (CompareTag("Wall"))
        {
            tileCornerDownLeft.GetComponent<SpriteRenderer>().sprite = wallDownLeftCorner;
        }
        else if (CompareTag("Ground"))
        {
            tileCornerDownLeft.GetComponent<SpriteRenderer>().sprite = groundDownLeftCorner;
        }
        if (leftNeighbour != null && downNeighbour != null && leftNeighbour.CompareTag(gameObject.tag) && downNeighbour.CompareTag(gameObject.tag))
        {
            if (downLeftNeighbour == null)
                Instantiate(tileCornerDownLeft, transform.position + new Vector3(-0.5f, -0.5f, 0f), Quaternion.identity, transform);
            else if (downLeftNeighbour.CompareTag("Border"))
            {

            }
            else if (downLeftNeighbour.CompareTag("Wall") && CompareTag("Border"))
                Instantiate(tileCornerDownLeft, transform.position + new Vector3(-0.5f, -0.5f, 0f), Quaternion.identity, transform);
            else if (downLeftNeighbour.CompareTag("Ground") && (CompareTag("Border") || CompareTag("Wall")))
                Instantiate(tileCornerDownLeft, transform.position + new Vector3(-0.5f, -0.5f, 0f), Quaternion.identity, transform);
            //else Instantiate(tileCornerDownLeft, transform.position + new Vector3(-0.5f, -0.5f, 0f), Quaternion.identity, transform);
        }

        /////////////////////////////////////////////////////////////
        if (CompareTag("Border"))
        {
            tileCornerDownRight.GetComponent<SpriteRenderer>().sprite = borderDownRightCorner;
        }
        else if (CompareTag("Wall"))
        {
            tileCornerDownRight.GetComponent<SpriteRenderer>().sprite = wallDownRightCorner;
        }
        else if (CompareTag("Ground"))
        {
            tileCornerDownRight.GetComponent<SpriteRenderer>().sprite = groundDownRightCorner;
        }
        if (rightNeighbour != null && downNeighbour != null && rightNeighbour.CompareTag(gameObject.tag) && downNeighbour.CompareTag(gameObject.tag))
        {
            if (downRightNeighbour == null)
                Instantiate(tileCornerDownRight, transform.position + new Vector3(0.5f, -0.5f, 0f), Quaternion.identity, transform);
            else if (downRightNeighbour.CompareTag("Border"))
            {

            }
            else if (downRightNeighbour.CompareTag("Wall") && CompareTag("Border"))
                Instantiate(tileCornerDownRight, transform.position + new Vector3(0.5f, -0.5f, 0f), Quaternion.identity, transform);
            else if (downRightNeighbour.CompareTag("Ground") && (CompareTag("Border") || CompareTag("Wall")))
                Instantiate(tileCornerDownRight, transform.position + new Vector3(0.5f, -0.5f, 0f), Quaternion.identity, transform);
            //else Instantiate(tileCornerDownRight, transform.position + new Vector3(0.5f, -0.5f, 0f), Quaternion.identity, transform);
        }

        
    }
}