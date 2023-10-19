using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class WaveSystemScript : MonoBehaviour
{
    private List<Wave> waves;
    public struct Wave {
        public int numEnemyOne;
        public int numEnemyTwo;
    }
    // Start is called before the first frame update
    void Start()
    {
        waves = new List<Wave>();
        waves.Add(new Wave());
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
