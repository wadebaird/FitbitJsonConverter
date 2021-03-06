using Newtonsoft.Json;

namespace Sunbreak.FitbitJsonConverter;

public class FtibitJsonToCsvConverter
{
    private string inputFolderName;

    public async Task Convert(string inputFolderName, string outputFileName)
    {
        this.inputFolderName = inputFolderName;

        var outputFileNameExtension = Path.GetExtension(outputFileName);
        if(string.IsNullOrEmpty(outputFileNameExtension))
        {
            outputFileName = ".csv";
        }

        outputFileName = Path.Combine(Path.GetDirectoryName(outputFileName), Path.GetFileNameWithoutExtension(outputFileName));

        var earliestDateTime = DateTime.Today;
        var altitudes = ReadFiles<Altitude, int>("altitude", false, ref earliestDateTime);
        var calories = ReadFiles<Calories, decimal>("calories", false, ref earliestDateTime);
        var distances = ReadFiles<Distance, int>("distance", true, ref earliestDateTime);
        var lightlyActiveMinutes = ReadFiles<LightlyActiveMinutes, int>("lightly_active_minutes", false, ref earliestDateTime);
        var moderatelyActiveMinutes = ReadFiles<ModeratelyActiveMinutes, int>("moderately_active_minutes", false, ref earliestDateTime);
        var sedentaryMinutes = ReadFiles<SedentaryActiveMinutes, int>("sedentary_minutes", false, ref earliestDateTime);
        var steps = ReadFiles<Steps, int>("steps", true, ref earliestDateTime);
        var veryActiveMinutes = ReadFiles<VeryActiveMinutes, int>("very_active_minutes", false, ref earliestDateTime);
        var sleeps = ReadFiles<Sleep>("sleep").OrderBy(x => x.StartTime);
        var weights = ReadFiles<WeightData>("weight").OrderBy(x => x.Date);

        int i = 1;
        int counter = 0;
        StreamWriter writer = null;

        async Task CreateWriter(string headerName, string columnHeaders, string filePostFix)
        {
            if (null != writer)
            {
                writer.Close();
            }

            counter = 0;
            writer = File.CreateText($"{outputFileName}-{headerName}{filePostFix}-{i++}{outputFileNameExtension}");
            await writer.WriteLineAsync(headerName);
            await writer.WriteLineAsync(columnHeaders);
        }

        async Task ResetWriter(string headerName, string columnHeaders, string filePostFix = null)
        {
            i = 1;
            await CreateWriter(headerName, columnHeaders, filePostFix);
        }

        async Task CheckToSplitWriter(string headerName, string columnHeaders, string filePostFix = null)
        {
            if (counter++ > 184)
            {
                await CreateWriter(headerName, columnHeaders, filePostFix);
            }
        }

        string bodyHeaderName = "Body";
        string bodyColumnHeaders = "Date,Weight,BMI,Fat";
        await ResetWriter(bodyHeaderName, bodyColumnHeaders);

        foreach (var weight in weights)
        {
            await writer.WriteLineAsync($"\"{weight.Date:yyyy-MM-dd}\",\"{weight.Weight:0.0}\",\"{weight.Bmi:0.0}\",\"{weight.Fat:#,##0}\"");

            await CheckToSplitWriter(bodyHeaderName, bodyColumnHeaders);
        }

        string activitiesHeaderName = "Activities";
        string activitiesColumnHeaders = "Date,Calories Burned,Steps,Distance,Floors,Minutes Sedentary,Minutes Lightly Active,Minutes Fairly Active,Minutes Very Active,Activity Calories";
        await ResetWriter(activitiesHeaderName, activitiesColumnHeaders, $"-{earliestDateTime.Year}");

        for (DateTime date = earliestDateTime; date < DateTime.Today; date = date.AddDays(1))
        {
            if (altitudes.TryGetValue(date, out int floors))
            {
                floors /= 10; //Convert feet to floors
            }

            calories.TryGetValue(date, out decimal calorie);
            steps.TryGetValue(date, out int step);

            if (distances.TryGetValue(date, out int distance))
            {
                distance /= 160400; //Average number from monthly vs archived data. No clue what this means.
            }

            lightlyActiveMinutes.TryGetValue(date, out int lightlyActiveMin);
            moderatelyActiveMinutes.TryGetValue(date, out int moderatelyActiveMin);
            sedentaryMinutes.TryGetValue(date, out int sedentaryMin);
            veryActiveMinutes.TryGetValue(date, out int veryActiveMin);

            //                                                                                                                                                                                                                                           -1300 is just an average extrapolated from the archive data vs last month data
            await writer.WriteLineAsync($"\"{date:yyyy-MM-dd}\",\"{calorie:#,##0}\",\"{step:#,##0}\",\"{distance}\",\"{floors:#,##0}\",\"{sedentaryMin:#,##0}\",\"{lightlyActiveMin:#,##0}\",\"{moderatelyActiveMin:#,##0}\",\"{veryActiveMin:#,##0}\",\"{(calorie - 1300):#,##0}\"");

            await CheckToSplitWriter(activitiesHeaderName, activitiesColumnHeaders, $"-{date.Year}");
        }

        string sleepHeaderName = "Sleep";
        string sleepColumnNames = "Start Time,End Time, Minutes Asleep,Minutes Awake, Number of Awakenings, Time in Bed,Minutes REM Sleep,Minutes Light Sleep,Minutes Deep Sleep";
        await ResetWriter(sleepHeaderName, sleepColumnNames);

        foreach (var sleep in sleeps)
        {
            var numberOfAwakenings = 0;
            if (sleep.Levels.Summary.Wake != null)
            {
                numberOfAwakenings = sleep.Levels.Summary.Wake.Count;
            }
            else if (sleep.Levels.Summary.Awake != null)
            {
                numberOfAwakenings = sleep.Levels.Summary.Awake.Count;
            }

            await writer.WriteLineAsync($"\"{sleep.StartTime:yyyy-MM-dd h:mm tt}\",\"{sleep.EndTime:yyyy-MM-dd h:mm tt}\",\"{sleep.MinutesAsleep:#,##0}\",\"{sleep.MinutesAwake:#,##0}\",\"{numberOfAwakenings:#,##0}\",\"{sleep.TimeInBed:#,##0}\",\"{(sleep.Levels.Summary.Rem != null ? sleep.Levels.Summary.Rem.Minutes : "N/A")}\",\"{(sleep.Levels.Summary.Light != null ? sleep.Levels.Summary.Light.Minutes : "N/A")}\",\"{(sleep.Levels.Summary.Deep != null ? sleep.Levels.Summary.Deep.Minutes : "N/A")}\"");

            await CheckToSplitWriter(sleepHeaderName, sleepColumnNames);
        }
    }

    private List<T> ReadFiles<T>(string filePrefix)
    {
        var resultCollection = new List<T>();

        foreach (var file in Directory.GetFiles(inputFolderName, $"{filePrefix}*.json", SearchOption.AllDirectories))
        {
            using StreamReader streamReader = new(file);
            using JsonTextReader reader = new(streamReader);
            JsonSerializer serializer = new();

            var result = serializer.Deserialize<List<T>>(reader);
            resultCollection.AddRange(result);
        }

        return resultCollection;
    }

    private Dictionary<DateTime, T2> ReadFiles<T1, T2>(string filePrefix, bool isTimeInUTC, ref DateTime earliestDateTime) where T1 : DateTimeValueTuple<T2>
    {
        var masterCollection = new Dictionary<DateTime, T2>();
        var tempMasterCollection = ReadFiles<T1>(filePrefix);

        foreach (var entry in tempMasterCollection)
        {
            var dateTime = isTimeInUTC ? entry.DateTime.ToLocalTime().Date : entry.DateTime.Date;
            earliestDateTime = new DateTime(Math.Min(dateTime.Ticks, earliestDateTime.Ticks));

            if (!masterCollection.ContainsKey(dateTime))
            {
                masterCollection.Add(dateTime, default);
            }

            dynamic a = masterCollection[dateTime];
            masterCollection[dateTime] = a + entry.Value;
        }

        return masterCollection;
    }
}