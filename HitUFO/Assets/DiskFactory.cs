using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class DiskFactory : MonoBehaviour
{
    private int allDiskNum = 0;
    private List<Disk> used = new List<Disk>();
    private List<Disk> free = new List<Disk>();

    public Disk getDisk(int level)
    {
        Disk nowDisk = null;
        if (free.Count > 0)
        {
            nowDisk = free[0];
            nowDisk.reset(level);
            used.Add(free[0]);
            free.Remove(free[0]);

        }
        else
        {
            allDiskNum++;
            nowDisk = new Disk(allDiskNum, level);
            used.Add(nowDisk);
        }

        return nowDisk;

    }

    public void reset()
    {
        foreach (Disk temp in used)
        {
            temp.disk.SetActive(false);
            free.Add(temp);
        }
        used.Clear();
    }

    public int getNowUsedDisk()
    {
        return used.Count;
    }

    public void freeDisk(Disk diskdata)
    {
        if (used.Contains(diskdata))
        {
            diskdata.disk.SetActive(false);
            used.Remove(diskdata);
            free.Add(diskdata);
        }

    }

    public Disk getHitDisk(GameObject disk)
    {
        foreach (Disk i in used)
        {
            if (i.disk == disk)
            {
                return i;
            }
        }
        return null;
    }
}
