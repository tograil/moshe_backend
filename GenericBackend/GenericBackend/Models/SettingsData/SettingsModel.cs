using System;
using System.Collections.Generic;
using System.Globalization;
using System.Linq;
using System.Web;
using GenericBackend.DataModels.Actual;
using GenericBackend.DataModels.Document;
using GenericBackend.DataModels.Plan;

namespace GenericBackend.Models.SettingsData
{
    public class SettingsModel
    {

        public ICollection<SettingItem> SettingItems { get; set; }
        public ICollection<int> Years { get; set; }
        public ICollection<string> Monthes { get; set; }

        public static SettingsModel FromData(DocumentInfo info)
        {
            var internalIndex = 0;
            var settings = info.Plan.PlanItems.Select(plan => new SettingItem
            {
                Subject = plan.Subject,
                PlanDataItems = GetPlanData(plan.TimelineData.ToArray()).ToArray(),
                ActualDataItems = GetActualData(info.Actual.ActualItems[internalIndex++].TimelineData.ToArray()).ToArray()
                
            }).ToArray();

            var years = info.Plan.PlanItems.First().TimelineData.Select(timeline => timeline.Year).ToArray();

            var culture = new CultureInfo("en-US");

            var monthes =
                info.Plan.PlanItems.First()
                    .TimelineData.Select(timeline => culture.DateTimeFormat.GetAbbreviatedMonthName(timeline.Month))
                    .ToArray();

            return new SettingsModel
            {
                SettingItems = settings,
                Years = years,
                Monthes = monthes
            };
        }

        private static IEnumerable<DataItem> GetActualData(ICollection<ActualTimelineData> timelineData)
        {
            yield return new DataItem
            {
                Title = "Actual",
                Data = timelineData.Select(x => S(x.Actual)).ToArray()
            };

            yield return new DataItem
            {
                Title = "Updated Actual",
                Data = timelineData.Select(x => S(x.UpdateActual)).ToArray()
            };

            yield return new DataItem
            {
                Title = "Accumulated Actual",
                Data = timelineData.Select(x => S(x.AccumulatedActual)).ToArray()
            };

            yield return new DataItem
            {
                Title = "Accumulated Updated Actual",
                Data = timelineData.Select(x => S(x.AccumulatedUpdate)).ToArray()
            };
        }

        private static int S(string x)
        {
            return Convert.ToInt32(Math.Round(decimal.Parse(x, NumberStyles.Any, new CultureInfo("en-US"))).ToString(new CultureInfo("en-US")));
        }

        private static IEnumerable<DataItem> GetPlanData(ICollection<PlanTimelineData> timelineData)
        {
            yield return new DataItem
            {
                Title = "Plan",
                Data = timelineData.Select(x => S(x.Plan)).ToArray()
            };

            yield return new DataItem
            {
                Title = "Accumulated Plan",
                Data = timelineData.Select(x => S(x.AccumulatedPlan)).ToArray()
            };
        }
    }

    

    public class SettingItem
    {
        public string Subject { get; set; }
        public ICollection<DataItem> PlanDataItems { get; set; }
        public ICollection<DataItem> ActualDataItems { get; set; }

    }

    public class DataItem
    {
        public string Title { get; set; }
        public ICollection<int> Data { get; set; }
    }
}
