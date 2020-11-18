using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class PatrolFactory : MonoBehaviour
{
    private GameObject patrol = null;
    private List<GameObject> used = new List<GameObject>();
    private Vector3[] pos = new Vector3[9];
    public FirstSceneController sceneControler;

    public List<GameObject> GetPatrols()
    {
        int[] pos_x = { -6, 4, 13 };
        int[] pos_z = { -4, 6, -13 };
        int index = 0;
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                pos[index] = new Vector3(pos_x[i], 5, pos_z[j]);
                index++;
            }
        }
        for (int i = 0; i < 9; i++)
        {
            patrol = Instantiate(Resources.Load<GameObject>("Prefabs/Patrols"));
            patrol.transform.position = pos[i];
            patrol.GetComponent<PatrolData>().sign = i + 1;
            patrol.GetComponent<PatrolData>().start_position = pos[i];
            used.Add(patrol);
        }
        return used;
    }
}
