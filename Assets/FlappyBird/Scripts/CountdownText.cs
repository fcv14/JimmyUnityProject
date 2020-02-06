using System.Collections;
using System.Collections.Generic;
using UnityEngine;
using UnityEngine.UI;
[RequireComponent(typeof(Text))]
public class CountdownText : MonoBehaviour
{
    public delegate void CountdownFinished();
    public static event CountdownFinished OnCountdownFinished;
    Text countdown;

    void OnEnable()
    {
        countdown = GetComponent<Text>();
        countdown.text = "3";
        StartCoroutine("Countdown");
        //StartCoroutine(Countdown());
        
    }

    IEnumerator Countdown(){      // 迭代器 https://dev.twsiyuan.com/2016/03/csharp-ienumerable-ienumerator-and-yield-return.html
        int count = 3;            
        for(int i = 0 ; i < count ; i++)
        {
            countdown.text = (count-i).ToString();
            yield return new WaitForSeconds(1);
        }
        OnCountdownFinished();           //倒數完後就執行OnCountdownFinished();
    }

}
