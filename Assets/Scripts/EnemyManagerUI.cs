using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class EnemyManagerUI : MonoBehaviour
{
    public EnemyManager enemyManager;
    public Text waveText;
    public Text enemyText;
    
    // Update is called once per frame
    void Update()
    {
        waveText.text = $"Wave:{enemyManager.wave+1}/{enemyManager.waves.Length}";
       enemyText.text = $"Enemy:{enemyManager.EnemyCnt}";
    }
}
