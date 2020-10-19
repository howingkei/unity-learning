using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using baseCode;

public class roundController : MonoBehaviour
{
    public string status = "running"; // running , pause , gameover
    DiskFactory diskFac;
    int roundLevel = 1;
    int numOfDiskAlredySend = 0;
    float time = 0;
    float sendDiskTime = 1;

    CCActionManager actionManager;
    public ScoreController scoreCtrl = new ScoreController();

    void Awake()
    {
        Director director = Director.getInstance();
        director.currentSceneController = this;
        this.gameObject.AddComponent<DiskFactory>();
        this.gameObject.AddComponent<CCActionManager>();
        this.gameObject.AddComponent<InteractGUI>();

    }
    // Use this for initialization
    void Start()
    {
        diskFac = Singleton<DiskFactory>.Instance;
        actionManager = Singleton<CCActionManager>.Instance;
    }

    // Update is called once per frame
    void Update()
    {
        if (status == "pause" || status == "gameover")
        {
            return;
        }
        else if (roundLevel > 3)
        {
            if (diskFac.getNowUsedDisk() == 0)
                status = "gameover";
            return;
        }
        else if (numOfDiskAlredySend >= 10)
        {
            numOfDiskAlredySend = 0;
            roundLevel++;
        }
        time += Time.deltaTime;
        checkIfSendDisk();

    }


    public void hitDisk(GameObject disk)
    {
        Disk temp = diskFac.getHitDisk(disk);
        if (temp == null)
        {
            Debug.Log("the disk of clicked is null? ");
        }
        else
        {
            scoreCtrl.addScore(temp.level);
            diskFac.freeDisk(temp);
        }


    }

    private void checkIfSendDisk()
    {
        if (time > sendDiskTime)
        {
            float randomNumOfSend = Random.Range(0f, roundLevel);
            int numOfSendDisk;
            //决定要送的碟数
            if (randomNumOfSend <= 1)
            {
                numOfSendDisk = 1;
            }
            else if (randomNumOfSend <= 2)
            {
                numOfSendDisk = 2;
            }
            else
            {
                numOfSendDisk = 3;
            }
            sendSomeDisks(numOfSendDisk);
            time = 0;
        }
    }

    private void sendSomeDisks(int num)
    {
        for (int loop = 0; loop < num; loop++)
        {
            float randomNumOfLevel = Random.Range(0f, roundLevel);
            int thisDiskLevel;
            //决定要送的碟的level
            if (randomNumOfLevel <= 1)
            {
                thisDiskLevel = 1;
            }
            else if (randomNumOfLevel <= 2)
            {
                thisDiskLevel = 2;
            }
            else
            {
                thisDiskLevel = 3;
            }
            sendOneDisk(thisDiskLevel);
        }
    }

    private void sendOneDisk(int sendLevel)
    {
        numOfDiskAlredySend++;
        Disk oneDisk = diskFac.getDisk(sendLevel);
        Move moveAction = Move.getDiskMove(oneDisk, sendLevel);
        actionManager.RunAction(oneDisk.disk, moveAction, null);
    }

    public void loadResources()
    {

    }

    public void reset()
    {
        actionManager.reset();
        diskFac.reset();
        scoreCtrl.reset();
        roundLevel = 1;
        numOfDiskAlredySend = 0;
        status = "running";
    }

}
