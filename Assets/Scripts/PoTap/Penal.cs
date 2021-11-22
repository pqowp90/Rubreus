using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;

public class Penal : MonoBehaviour
{
    public int num;
    public Image potapImage;
    public void MakePoTap(){
        GameManager.Instance.MakePoTapTap(num);
    }
}
