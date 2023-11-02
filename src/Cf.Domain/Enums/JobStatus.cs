namespace Cf.Domain.Enums;

public enum JobStatus
{   
    /// <summary>
    /// This status is set when the job is first created and waiting for client approval
    /// </summary>
    Pending = 0,

    /// <summary>
    /// This status is set if the client approves the job
    /// </summary>
    Accepted = 1,

    /// <summary>
    /// This status is set if client approved job and the service is starting to work on a car
    /// </summary>
    InProgress = 2,

    /// <summary>
    /// This status is set when service has finished working on a car
    /// </summary>
    Done = 3,

    /// <summary>
    /// This status is set if job is forcefully stopped after being accepted
    /// </summary>
    Cancelled = 4,

    /// <summary>
    /// This status is set if client rejects the job
    /// </summary>
    Declined = 5,
}

