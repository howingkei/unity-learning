using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Disk : MonoBehaviour
{
    public GameObject disk;
    public int diskID;
    public int level;
    public Disk(int id, int level)
    {
        this.diskID = id;
        disk = GameObject.Instantiate(Resources.Load<GameObject>("prefabs/disk"), Vector3.zero, Quaternion.identity);
        disk.name = "disk" + id.ToString();
        reset(level);
    }

    public void reset(int level)
    {
        disk.transform.position = new Vector3(Random.Range(-10f, 10f), Random.Range(-10f, 10f), Random.Range(0f, 2f));
        switch (level)
        {
            case 1:
                disk.GetComponent<Renderer>().material.color = Color.red;
                disk.transform.localScale = new Vector3(3f, 0.3f, 3f);
                break;
            case 2:
                disk.GetComponent<Renderer>().material.color = Color.yellow;
                disk.transform.localScale = new Vector3(2f, 0.2f, 2f);
                break;
            default:
                disk.GetComponent<Renderer>().material.color = Color.blue;
                disk.transform.localScale = new Vector3(1f, 0.1f, 1f);
                break;
        }
        this.level = level;
        disk.SetActive(true);
    }
}
