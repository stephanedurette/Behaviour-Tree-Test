using System.Collections;
using System.Collections.Generic;
using System.Linq;
using UnityEngine;

public class JobManager : MonoBehaviour
{
    private Dictionary<Job, List<Unit>> jobWorkers;
    private Dictionary<Unit, Coroutine> workingCoroutines;

    private readonly float workingInterval = .25f;

    private void Awake()
    {
        jobWorkers = new();
        workingCoroutines = new();
    }

    public void RegisterJob(Job job)
    {
        jobWorkers.Add(job, new());
    }

    public void UnregisterJob(Job job)
    {
        foreach (var worker in jobWorkers[job]) { 
            StopWorkCoroutine(worker);
        }
        jobWorkers.Remove(job);
    }

    public Job FindJob(JobData jobData)
    {
        return jobWorkers.Where((kv) => kv.Key.JobData == jobData).OrderBy((kv) => kv.Value.Count).FirstOrDefault().Key;
    }

    public void AssignJob(Unit unit, Job job)
    {
        jobWorkers[job].Add(unit);
        workingCoroutines.Add(unit, StartCoroutine(WorkingCoroutine(unit, job)));

    }

    private IEnumerator WorkingCoroutine(Unit u, Job j)
    {
        while (true)
        {
            yield return new WaitForSeconds(workingInterval);
            j.Update(u, workingInterval);
        }
    }

    public void UnassignJob(Unit unit) { 
        StopWorkCoroutine(unit);
        jobWorkers.FirstOrDefault((kv) => kv.Value.Contains(unit)).Value.Remove(unit);
    }

    private void StopWorkCoroutine(Unit unit) {
        StopCoroutine(workingCoroutines[unit]);
        workingCoroutines.Remove(unit);
    }
}
