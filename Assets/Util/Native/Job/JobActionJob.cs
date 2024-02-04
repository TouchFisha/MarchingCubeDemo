using System.Collections;
using System.Collections.Generic;
using Unity.Burst;
using Unity.Jobs;

namespace NUtil.NThreadNative
{
    public struct JobActionJob : IJob
    {
        public JobAction action;
        public JobActionJob(JobAction action)
        {
            this.action = action;
        }
        public void Execute()
        {
            action.Invoke();
        }
    }
}
