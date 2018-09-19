using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class GroundController : MonoBehaviour {

    public int Value;


    public void SetValue(int value)
    {
        Value = value;
    }
    public int GetValue()
    {
        return Value;
    }

    private void OnTriggerEnter2D(Collider2D collision)
    {
        if (Value!=0) collision.gameObject.GetComponent<PlayerController>().PickUpMinerals(Value);
        
        if (collision.gameObject.CompareTag("Player"))
        {
            Destroy(gameObject);
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
