using System;
using System.Collections;
using System.Collections.Generic;
using System.IO;
using System.Linq;
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
        Load();
    }

    public void Save()
    {
        scoreList.Add(current);
        File.WriteAllBytes(PATH, Encoding.UTF8.GetBytes(JsonUtility.ToJson(scoreList, false)));
        current = new Score();
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
        if (scores.Count > 10)
            scores.RemoveAt(scores.Count - 1);
    }

    public override string ToString()
    {
        return scores.Select(s => s.km > 0 ? s.ToString() : "").Aggregate((a, b) => $"{a}\n-------------------\n{b}");
    }
}


[System.Serializable]
public class Score
{
    public float time;
    public float km;
    public float Gems;


    public float Compute() => Mathf.Clamp(km * (Gems / 10f + 1) - time, 0f, 9999999f);

    public override string ToString()
    {
        return $"Seconds:{time.ToString("0.00")}\nKm: {(km / 10f).ToString("0.00")}\nGemas: {Gems.ToString("0")}";
    }
}