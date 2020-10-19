using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class Move : SSAction
{
    public Vector3 aim;
    public float speed;
    public Disk thisDisk;
    public float time = 0;
    
    public static Move getDiskMove(Disk disk, int level)
    {
        Move action = ScriptableObject.CreateInstance<Move>();
        switch (level)
        {
            case 1:
                action.speed = 6f;
                break;
            case 2:
                action.speed = 8f;
                break;
            case 3:
                action.speed = 10f;
                break;
        }
        action.thisDisk = disk;
        action.aim = new Vector3(Random.Range(-2f, 2f), Random.Range(-2f, 2f), Random.Range(4f, 10f));
        return action;

    }
    // Use this for initialization
    public override void Start()
    {

    }
    // Update is called once per frame
    public override void Update()
    {
        gameobject.transform.position = Vector3.MoveTowards(gameobject.transform.position, aim, speed * Time.deltaTime);

        if (this.transform.position == aim)
        {
            this.destroy = true;
            this.enable = false;
            Singleton<DiskFactory>.Instance.freeDisk(thisDisk);
            thisDisk.disk.SetActive(false);
        }
    }
}