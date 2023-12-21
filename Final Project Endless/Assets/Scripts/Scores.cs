using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Text;
using UnityEngine;

public class Scores : MonoBehaviour
{
    static public Scores Instance { get; private set; }
    static string PATH => Application.persistentDataPath + "/scores.bt";

    public Score current;
    public ScoreList scoreList;

    private void Awake()
    {
        Instance = this;
    }

    public void Save()
    {
        scoreList.Add(current);
        File.WriteAllBytes(PATH, Encoding.UTF8.GetBytes(JsonUtility.ToJson(scoreList, false)));
    }

    public void Load()
    {
        if (!File.Exists(PATH)) Save();
        scoreList = JsonUtility.FromJson<ScoreList>(Encoding.UTF8.GetString(File.ReadAllBytes(PATH)));
    }


    private void OnDestroy()
    {
        Instance = null;
    }
    private void OnApplicationQuit()
    {
        OnDestroy();
    }

}

[System.Serializable]
public class ScoreList
{
    static Comparison<Score> comparisor = new Comparison<Score>((s0, s1) => -s0.Compute().CompareTo(s1.Compute()));


    public List<Score> scores = new List<Score>();

    public void Add (Score score)
    {
        scores.Add(score);
        scores.Sort(comparisor);
    }
}


[System.Serializable]
public class Score
{
    public float time;
    public float km;
    public float Gems;


    public float Compute() => Mathf.Clamp(km * (Gems / 10f + 1) - time, 0f, 9999999f);
}