using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour {

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
            //var neighbours = Physics2D.OverlapBoxAll(transform.position, new Vector2(2f, 2f), 0f);
            //foreach (var neighbour in neighbours)
            //{
            //    CornersAndBordersController cabc = neighbour.gameObject.GetComponent<CornersAndBordersController>();
            //    if (cabc != null) cabc.needToUpdate = true;
            //}
        }
    }

    private void OnDestroy()
    {
        var neighbours = Physics2D.OverlapBoxAll(transform.position, new Vector2(2f, 2f), 0f);
        foreach (var neighbour in neighbours)
        {
            CornersAndBordersController cabc = neighbour.gameObject.GetComponent<CornersAndBordersController>();
            if (cabc != null) cabc.needToUpdate = true;
        }
    }
}
