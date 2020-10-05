using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class TicTacToe : MonoBehaviour
{
    public Texture2D img1;   //棋子1
    public Texture2D img2;   //棋子2
    public Texture2D imgBack;//棋子背景
    private int player;      //奇数说明轮到1玩家，偶数说明轮到2玩家
    private int result;      //游戏结果
    private int[,] matrix2D;//棋盘矩阵
    // Start is called before the first frame update
    void Start()
    {
        Reset();
    }
    void Reset()
    {
        player = 0;
        result = 0;
        matrix2D = new int[3, 3]{
            {0,0,0},
            {0,0,0},
            {0,0,0}
        };
    }
    int check()
    {
        for (int i = 0; i < 3; i++)
        {
            if (matrix2D[i, 0] == matrix2D[i, 1] && matrix2D[i, 1] == matrix2D[i, 2] && matrix2D[i, 0] != 0) return matrix2D[i, 0];   // 检查列
            if (matrix2D[0, i] == matrix2D[1, i] && matrix2D[1, i] == matrix2D[2, i] && matrix2D[0, i] != 0) return matrix2D[0, i];   // 检查行
        }
        if (matrix2D[0, 0] == matrix2D[1, 1] && matrix2D[1, 1] == matrix2D[2, 2]) return matrix2D[1, 1];                              // 检查对角
        if (matrix2D[0, 2] == matrix2D[1, 1] && matrix2D[1, 1] == matrix2D[2, 0]) return matrix2D[1, 1];                              // 检查对角
        return 0;
    }
    void OnGUI()
    {
        // 设置游戏界面的大小
        GUI.Label(new Rect(0, 0, 300, 300), "");
        // 设置重新开始按钮
        if (GUI.Button(new Rect(200, 0, 100, 50), "restart"))
            Reset();
        else
            ;
        // 设置游戏结果显示风格
        GUIStyle fontStyle = new GUIStyle();
        fontStyle.normal.background = null;
        fontStyle.normal.textColor = new Color(1, 1, 1);
        fontStyle.fontSize = 20;

        // 根据matrix2D保存的值对棋盘上的每个按钮进行设置
        for (int i = 0; i < 3; i++)
        {
            for (int j = 0; j < 3; j++)
            {
                if (matrix2D[i, j] == 1)
                    GUI.Button(new Rect(50 * i, 50 * j, 50, 50), img1);
                else if (matrix2D[i, j] == 2)
                    GUI.Button(new Rect(50 * i, 50 * j, 50, 50), img2);
                else if (result != 0)
                {
                    if (GUI.Button(new Rect(50 * i, 50 * j, 50, 50), imgBack))
                        ;
                }
                else
                {
                    if (GUI.Button(new Rect(50 * i, 50 * j, 50, 50), imgBack))
                    {
                        matrix2D[i, j] = 1 + player % 2;
                        result = check();
                        player++;
                    }
                }
            }
        }
        // 结果显示
        if (result == 1)
        {
            GUI.Label(new Rect(200, 100, 100, 50), "Player1 wins!", fontStyle);
        }
        else if (result == 2)
        {
            GUI.Label(new Rect(200, 100, 100, 50), "Player2 wins!", fontStyle);
        }
        else
        {
            GUI.Label(new Rect(200, 100, 100, 50), "Playing...", fontStyle);
        }
    }

    // Update is called once per frame
    void Update()
    {
        
    }
}
