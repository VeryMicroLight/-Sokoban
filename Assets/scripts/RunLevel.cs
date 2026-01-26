using System.Collections;
using System.Collections.Generic;
using UnityEngine.SceneManagement;
using UnityEngine;
using System.Linq;

public class RunLevel : MonoBehaviour
{

    /*
    每一个可放置的游戏对象（兔子、箱子、还有终点的王冠）都有一个位移修正posOffset，编辑关卡时只需要把游戏对象拖到大致位置即可，脚本会自动对齐
 */


    private List<Transform> Boxes = new List<Transform>();
    private List<Transform> CheckPoints = new List<Transform>();

    public bool isMoving = false;
    public Vector3 checkPointPosOffset;
    public LayerMask Colliderable;
    public GameObject WinPanel;

    private void Start()
    {
        Listing("Pushable");
        Listing("CheckPoint");
    }
    private void Update()
    {
        /*
            在isMoving为true时，进行移动（如果要插入音效就在这个状态）   （ fade:不用了，插入到人物脚本了 ）
            反之在isMoving为false时，游戏会结算状态（比如解除箱子与玩家的联系，判断是否通关等）
            TODO: 也许可以加一些结算时的新机制

        fade
        fade
        fade
        fade
        fade
        fade:看这里
         fade:  游戏胜利时，使用WinPanel.SetActive (true); 显示胜利画面（觉得丑就自己改）
         */


        if (isMoving)
        {

        }
        else
        {

            ReleaseBoxes();
            if (isAllBoxesOnCheckPoints())
            {
                //TODO: 关卡完成方法
                //LevelComplete()
                print("Level Complete!");
            }
        }
    }

    //对游戏对象归类
    private void Listing(string tag)
    {
        GameObject[] gameObjects = GameObject.FindGameObjectsWithTag(tag);
        if (tag == "Pushable")
        {
            foreach (GameObject box in gameObjects)
            {
                Boxes.Add(box.transform);
            }
        }
        else if (tag == "CheckPoint")
        {
            foreach (GameObject checkpoint in gameObjects)
            {
                CheckPoints.Add(checkpoint.transform);
            }
        }
    }


    private void ReleaseBoxes()
    {
        foreach (Transform box in Boxes)
        {
            box.SetParent(null, true);
        }
    }

    private bool isAllBoxesOnCheckPoints()
    {
        bool yes = true;
        foreach(Transform checkpoint in CheckPoints)
        {
            Collider2D hit = Physics2D.OverlapBox(checkpoint.transform.position + checkPointPosOffset, .5f * Vector3.one, 0f, Colliderable);
            if (hit != null)
            {
                yes &= hit.transform.CompareTag("Pushable");
            }
            else
            {
                yes = false;
            }
            
        }
        return yes;
    }

}
