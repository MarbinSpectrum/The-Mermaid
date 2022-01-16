using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class MoveCamera : MonoBehaviour
{
    public Transform target;
    public static string nowRoom = "Room1-1";

    public void MoveRoom(bool walkSound)
    {
        if (walkSound)
            SoundManager.PlaySE(SoundManager.SE.foot);
        target.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        target.gameObject.SetActive(true);

        if(transform != null)
        {
            Transform temp = transform;
            for (int i = 0; (i < 10 && !temp.name.Contains("Room")); i++)
                temp = temp.parent;
            temp.gameObject.SetActive(false);
        }

        nowRoom = target.transform.name;

        DataManager.playData.nowRoom = nowRoom;
        DataManager.SaveData();
    }

    public void LoadRoom(bool walkSound)
    {
        if (walkSound)
            SoundManager.PlaySE(SoundManager.SE.foot);
        target.GetComponent<RectTransform>().anchoredPosition = Vector2.zero;
        target.gameObject.SetActive(true);

        nowRoom = target.transform.name;
    }


    private void OnDrawGizmos()
    {
        if (!target)
            return;

        Gizmos.color = Color.red;

        Vector3 direction = target.position - transform.position;
        direction = direction.normalized;
        Gizmos.DrawLine(transform.position, target.position);

        float dis = 50f;
        float angle = 30;

        Vector3 pos = target.position - direction * dis;
        Gizmos.DrawLine(target.position, pos);
        {
            pos = RotateVector(pos, +angle / 2f, target.position);
            Gizmos.DrawLine(target.position, pos);
        }
        {
            pos = RotateVector(pos, -angle, target.position);
            Gizmos.DrawLine(target.position, pos);
        }
    }

    private Vector2 RotateVector(Vector2 v,float angle,Vector2 m)
    {
        angle *= Mathf.Deg2Rad;
        float x = (v.x - m.x) * Mathf.Cos(angle) - (v.y - m.y) * Mathf.Sin(angle) + m.x;
        float y = (v.x - m.x) * Mathf.Sin(angle) + (v.y - m.y) * Mathf.Cos(angle) + m.y;
        return new Vector2(x, y);
    }

}
