using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Heal : MonoBehaviour
{
    public Enemy targetEnemy;
    private float speed = 10;

    // Update is called once per frame
    void Update()
    {
        if (targetEnemy==null) {
            Destroy(gameObject);
            return;
        }

        var v = targetEnemy.transform.position - transform.position;
        transform.position += v.normalized * speed * Time.deltaTime;

        if (v.magnitude<0.7f) {
            if (targetEnemy.hp<10) {
                targetEnemy.hp += 1;
            }
            Destroy(gameObject);

        }
    }
}
