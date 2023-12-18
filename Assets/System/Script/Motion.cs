using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Motion : MonoBehaviour
{
    public float rad = 1;
    // Start is called before the first frame update
    void Start()
    {
        // float rad;
    }

    // Update is called once per frame
    private void Update()
    {
        ran();
    }
    public void ran() {
        rad = 2;
        // rad = rad * Random.Range(2,5);
        float k = 0;
        k = Mathf.Sin(Time.time * 360 * Mathf.Deg2Rad * Random.Range(1,2)) * rad;
        transform.rotation = Quaternion.Euler(k, 0, 0);
    }
}
