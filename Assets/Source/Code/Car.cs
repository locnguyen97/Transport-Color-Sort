using System;
using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Car : MonoBehaviour
{

    public int index;
    
    public void Move()
    {
        var target = GameManager.Instance.GetCurLevel().gameObjectsTarget[index].transform;
        StartCoroutine(Move(target));
    }

    IEnumerator Move(Transform target)
    {
        var dis = Vector3.Distance(target.position , transform.position);
        var dir = target.position - transform.position;
        while (dis > 0.1f)
        {
            yield return new WaitForEndOfFrame();
            transform.position = transform.position + dir * 0.005f;
            dis = Vector3.Distance(target.position , transform.position);
        }
        gameObject.SetActive(false);
        GameManager.Instance.EnableDrag();
        GameManager.Instance.GetCurLevel().RemoveObject(gameObject);
    }

    private void OnMouseDown()
    {
        if (GameManager.Instance.canDrag)
        {
            if (GameManager.Instance.idxGame == index)
            {
                GameManager.Instance.canDrag = false;
                Move();
            }
        }
    }

    private void OnCollisionEnter(Collision other)
    {
        if (other.transform.CompareTag(gameObject.tag))
        {
            Destroy(other.gameObject);
        }
    }
}
