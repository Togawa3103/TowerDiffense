using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Enemy_Healer : Enemy
{

    // Start is called before the first frame update
    void Start()
    {
        transform.position = route.points[0].transform.position;
        pointIndex = 0;
    }

    // Update is called once per frame
    void Update()
    {
        var v = route.points[pointIndex+1].transform.position-route.points[pointIndex].transform.position;
        transform.position += v.normalized * speed * Time.deltaTime;

        var pv = transform.position- route.points[pointIndex].transform.position;
        if (pv.magnitude>=v.magnitude) {
            pointIndex++;
            if (pointIndex>=route.points.Length-1) {
                Destroy(gameObject);
                //ToDo プレイヤーにダメージ
                FindObjectOfType<Player>().hp--;
            }
        }
    }
}
