using System.Collections.Generic;

namespace ViberBot.Helpers
{
    public class TrackingDataDTO
    {
        public List<TrackingDataItem> Items { get; set; }
    }

    public class TrackingDataItem
    {
        //rules for tracker
        // {userId}-{questionNumber}-{counter}
        public string UserId { get; set; }

        public string QuestionNumber { get; set; }
        public int Counter { get; set; }
    }
}