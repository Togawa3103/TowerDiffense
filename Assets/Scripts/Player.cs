using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Player : MonoBehaviour
{
    public Bow bowPrefab;

    public int hp;

    public int gold;

    public Bow selectBow;

    public void CreateBow(Transform t) {
        if (gold < 100) return;
        gold -= 100;
        selectBow = Instantiate(bowPrefab,t);
        selectBow.transform.localPosition=Vector3.zero;
    }

    public void LevelUpBow()
    {
        if (selectBow == null) return;
        if (gold < selectBow.Cost) return;
        gold -= selectBow.Cost ;
        selectBow.lv++;

    }
    public void SellBow()
    {
        if (selectBow == null) return;
        gold += selectBow.Price;
        Destroy(selectBow.gameObject);
        selectBow = null;
    }

    // Update is called once per frame
    void Update()
    {
        if (Input.GetMouseButtonDown(0)) {
            var mousePos=Camera.main.ScreenToWorldPoint(Input.mousePosition);
            var col = Physics2D.OverlapPoint(mousePos,LayerMask.GetMask("Block"));
            if (col == null) return;
            var childBow = col.GetComponentInChildren<Bow>();
            if (childBow == null)
            {
                CreateBow(col.transform);
            }
            else {
                selectBow = childBow;
            }
        }
        if (Input.GetMouseButtonDown(1)) {
            SellBow();
        }
    }
}
