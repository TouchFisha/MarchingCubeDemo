using System;
using Unity.Jobs;
using UnityEngine;

namespace NUtil.NThreadNative
{
    /// <summary>
    /// Can't Do:
    /// SetAllPixels32
    /// Mesh
    /// GameObject
    /// AllUnity
    /// </summary>
    public struct Job
    {
        public JobState state;
        JobHandle handle;
        JobAction action;
        JobActionJob job;
        float startTime;
        Action end;
        public Job(Action action, Action end = null, bool isRunAwake = true)
        {
            state = JobState.wait;
            handle = default;
            this.action = new JobAction(action);
            job = new JobActionJob(this.action);
            this.end = end;
            startTime = 0;
            if (isRunAwake) Run();
        }
        public void Run()
        {
            handle = job.Schedule();
            startTime = Time.realtimeSinceStartup;
            state = JobState.running;
            WorkerThread.DoJob(Update);
        }
        public bool Update()
        {
            if (state == JobState.running && handle.IsCompleted)
            {
                Complete();
                return true;
            }
            return false;
        }
        public void Complete()
        {
            handle.Complete();
            action.Dispose();
            end?.Invoke();
            state = JobState.die;
        }
    }
}
