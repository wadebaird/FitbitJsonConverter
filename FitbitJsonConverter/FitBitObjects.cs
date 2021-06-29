using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace Sunbreak.FitbitJsonConverter
{
    public class DateTimeValueTuple<T>
    {
        public DateTime DateTime { get; set; }
        public T Value { get; set; }
    }

    public class Altitude : DateTimeValueTuple<int>
    {
    }

    public class Calories : DateTimeValueTuple<decimal>
    {
    }

    public class Distance : DateTimeValueTuple<int>
    {
    }

    public class LightlyActiveMinutes : DateTimeValueTuple<int>
    {
    }

    public class ModeratelyActiveMinutes : DateTimeValueTuple<int>
    {
    }

    public class SedentaryActiveMinutes : DateTimeValueTuple<int>
    {
    }

    public class Steps : DateTimeValueTuple<int>
    {
    }

    public class VeryActiveMinutes : DateTimeValueTuple<int>
    {
    }

    public class Sleep
    {
        public string LogId { get; set; }
        public DateTime DateOfSleep { get; set; }
        public DateTime StartTime { get; set; }
        public DateTime EndTime { get; set; }
        public int Duration { get; set; }
        public int MinutesToFallAsleep { get; set; }
        public int MinutesAsleep { get; set; }
        public int MinutesAwake { get; set; }
        public int MinutesAfterWakeup { get; set; }
        public int TimeInBed { get; set; }
        public int Efficiency { get; set; }
        public string Type { get; set; }
        public string InfoCode { get; set; }
        public int MinutesREMSleep { get; set; }
        public int MinutesLightSleep { get; set; }
        public int MinutesDeepSleep { get; set; }
        public SleepLevel Levels { get; set; }
        public bool MainSleep { get; set; }
    }

    public class SleepLevel
    {
        public SleepSummary Summary { get; set; }
        public List<SleepData> Data { get; set; }
        public List<SleepData> ShortData { get; set; }
    }

    public class SleepSummary
    {
        public SleepType Restless { get; set; }
        public SleepType Awake { get; set; }
        public SleepType Asleep { get; set; }
        public SleepType Deep { get; set; }
        public SleepType Wake { get; set; }
        public SleepType Light { get; set; }
        public SleepType Rem { get; set; }
    }

    public class SleepType
    {
        public int Count { get; set; }
        public int Minutes { get; set; }
        public int ThirtyDayAvgMinutes { get; set; }
    }

    public class SleepData
    {
        public DateTime DateTime { get; set; }
        public string Level { get; set; }
        public int Seconds { get; set; }
    }

    public class WeightData
    {
        public string LogId { get; set; }
        public decimal Weight { get; set; }
        public decimal Bmi { get; set; }
        public DateTime Date { get; set; }
        public decimal Fat { get; set; }
    }
}