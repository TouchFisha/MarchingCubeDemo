using System.Collections.Generic;
using UnityEngine;

public class WorkerThread : MonoBehaviour
{
    public static WorkerThread instance;

    public delegate bool Job();

    private void Awake()
    {
        instance = this;
    }
    public static List<Job> fixedJobs = new List<Job>();
    public static List<Job> updateJobs = new List<Job>();
    public static void DoJob(Job job, bool isRuningAtThisTime = false)
    {
        if (isRuningAtThisTime) if (job()) return;
        fixedJobs.Add(job);
    }
    public static void DoUpdateJob(Job job, bool isRuningAtThisTime = false)
    {
        if (isRuningAtThisTime) if (job()) return;
        updateJobs.Add(job);
    }
    private void Update()
    {
        if (updateJobs.Count > 0)
            foreach (Job job in updateJobs.ToArray())
                if (job()) updateJobs.Remove(job);
    }
    private void FixedUpdate()
    {
        foreach (Job job in fixedJobs.ToArray())
            if (job()) fixedJobs.Remove(job);
    }
    public static void Release()
    {
        fixedJobs.Clear();
        updateJobs.Clear();
    }
}

