using System.Collections;
using System.Collections.Generic;
using UnityEngine;

public class CountingVisitors : MonoBehaviour
{
    public GameObject stadium_crowd1;
    public GameObject stadium_crowd2;
    public GameObject stadium_crowd3;
    public GameObject stadium_crowd4;
    public GameObject stadium_crowd5;
    public GameObject stadium_crowd6;
    public GameObject stadium_crowd7;
    public GameObject stadium_crowd8;
    public GameObject stadium_crowd9;

    private int num_visitors = 0;

    // Start is called before the first frame update
    void Start()
    {
        CheckCrowd();
    }

    // Update is called once per frame
    void Update()
    {

    }

    void CheckCrowd()
    {
        stadium_crowd1.SetActive(false);
        stadium_crowd2.SetActive(false);
        stadium_crowd3.SetActive(false);
        stadium_crowd4.SetActive(false);
        stadium_crowd5.SetActive(false);
        stadium_crowd6.SetActive(false);
        stadium_crowd7.SetActive(false);
        stadium_crowd8.SetActive(false);
        stadium_crowd9.SetActive(false);

        if (num_visitors > 5)  stadium_crowd1.SetActive(true);
        if (num_visitors > 10) stadium_crowd2.SetActive(true);
        if (num_visitors > 15) stadium_crowd3.SetActive(true);
        if (num_visitors > 20) stadium_crowd4.SetActive(true);
        if (num_visitors > 25) stadium_crowd5.SetActive(true);
        if (num_visitors > 30) stadium_crowd6.SetActive(true);
        if (num_visitors > 35) stadium_crowd7.SetActive(true);
        if (num_visitors > 40) stadium_crowd8.SetActive(true);
        if (num_visitors > 45) stadium_crowd9.SetActive(true);
    }

    public int GetNumVisitors()
    {
        return num_visitors;
    }

    public void AddVisitor()
    {
        num_visitors++;
        CheckCrowd();
    }

    public void VisitorLeft()
    {
        num_visitors--;
        CheckCrowd();
    }
}
