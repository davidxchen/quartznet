using System;

namespace Quartz
{
    public interface IJobExecutionContext
    {
        /// <summary>
        /// Get a handle to the <see cref="IScheduler" /> instance that fired the
        /// <see cref="IJob" />.
        /// </summary>
        IScheduler Scheduler { get; }

        /// <summary>
        /// Get a handle to the <see cref="ITrigger" /> instance that fired the
        /// <see cref="IJob" />.
        /// </summary>
        ITrigger Trigger { get; }

        /// <summary>
        /// Get a handle to the <see cref="ICalendar" /> referenced by the <see cref="ITrigger" />
        /// instance that fired the <see cref="IJob" />.
        /// </summary>
        ICalendar Calendar { get; }

        /// <summary>
        /// If the <see cref="IJob" /> is being re-executed because of a 'recovery'
        /// situation, this method will return <see langword="true" />.
        /// </summary>
        bool Recovering { get; }

        /// <summary>
        /// Gets the refire count.
        /// </summary>
        /// <value>The refire count.</value>
        int RefireCount { get; }

        /// <summary>
        /// Get the convenience <see cref="JobDataMap" /> of this execution context.
        /// </summary>
        /// <remarks>
        /// <p>
        /// The <see cref="JobDataMap" /> found on this object serves as a convenience -
        /// it is a merge of the <see cref="JobDataMap" /> found on the 
        /// <see cref="JobDetail" /> and the one found on the <see cref="ITrigger" />, with 
        /// the value in the latter overriding any same-named values in the former.
        /// <i>It is thus considered a 'best practice' that the Execute code of a Job
        /// retrieve data from the JobDataMap found on this object.</i>
        /// </p>
        /// 
        /// <p>NOTE: Do not expect value 'set' into this JobDataMap to somehow be 
        /// set back onto a <see cref="IStatefulJob" />'s own JobDataMap.
        /// </p>
        /// 
        /// <p>
        /// Attempts to change the contents of this map typically result in an 
        /// illegal state.
        /// </p>
        /// 
        /// </remarks>
        JobDataMap MergedJobDataMap { get; }

        /// <summary>
        /// Get the <see cref="JobDetail" /> associated with the <see cref="IJob" />.
        /// </summary>
        IJobDetail JobDetail { get; }

        /// <summary>
        /// Get the instance of the <see cref="IJob" /> that was created for this
        /// execution.
        /// <p>
        /// Note: The Job instance is not available through remote scheduler
        /// interfaces.
        /// </p>
        /// </summary>
        IJob JobInstance { get; }

        /// <summary>
        /// The actual time the trigger fired. For instance the scheduled time may
        /// have been 10:00:00 but the actual fire time may have been 10:00:03 if
        /// the scheduler was too busy.
        /// </summary>
        /// <returns> Returns the fireTimeUtc.</returns>
        /// <seealso cref="ScheduledFireTimeUtc" />
        DateTimeOffset? FireTimeUtc { get; }

        /// <summary> 
        /// The scheduled time the trigger fired for. For instance the scheduled
        /// time may have been 10:00:00 but the actual fire time may have been
        /// 10:00:03 if the scheduler was too busy.
        /// </summary>
        /// <returns> Returns the scheduledFireTimeUtc.</returns>
        /// <seealso cref="FireTimeUtc" />
        DateTimeOffset? ScheduledFireTimeUtc { get; }

        /// <summary>
        /// Gets the previous fire time.
        /// </summary>
        /// <value>The previous fire time.</value>
        DateTimeOffset? PreviousFireTimeUtc { get; }

        /// <summary>
        /// Gets the next fire time.
        /// </summary>
        /// <value>The next fire time.</value>
        DateTimeOffset? NextFireTimeUtc { get; }

        /// <summary>
        /// Returns the result (if any) that the <see cref="IJob" /> set before its 
        /// execution completed (the type of object set as the result is entirely up 
        /// to the particular job).
        /// </summary>
        /// <remarks>
        /// <p>
        /// The result itself is meaningless to Quartz, but may be informative
        /// to <see cref="IJobListener" />s or 
        /// <see cref="ITriggerListener" />s that are watching the job's 
        /// execution.
        /// </p> 
        /// 
        /// Set the result (if any) of the <see cref="IJob" />'s execution (the type of 
        /// object set as the result is entirely up to the particular job).
        /// 
        /// <p>
        /// The result itself is meaningless to Quartz, but may be informative
        /// to <see cref="IJobListener" />s or 
        /// <see cref="ITriggerListener" />s that are watching the job's 
        /// execution.
        /// </p> 
        /// </remarks>
        object Result { get; set; }

        /// <summary> 
        /// The amount of time the job ran for.  The returned 
        /// value will be <see cref="TimeSpan.MinValue" /> until the job has actually completed (or thrown an 
        /// exception), and is therefore generally only useful to 
        /// <see cref="IJobListener" />s and <see cref="ITriggerListener" />s.
        /// </summary>
        TimeSpan JobRunTime { get; }

        /// <summary> 
        /// Put the specified value into the context's data map with the given key.
        /// Possibly useful for sharing data between listeners and jobs.
        /// <p>
        /// NOTE: this data is volatile - it is lost after the job execution
        /// completes, and all TriggerListeners and JobListeners have been 
        /// notified.
        /// </p> 
        /// </summary>
        /// <param name="key">
        /// </param>
        /// <param name="objectValue">
        /// </param>
        void Put(object key, object objectValue);

        /// <summary> 
        /// Get the value with the given key from the context's data map.
        /// </summary>
        /// <param name="key">
        /// </param>
        object Get(object key);
    }
}