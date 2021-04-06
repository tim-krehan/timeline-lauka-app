using Microsoft.Azure.Cosmos.Table;
using System;

namespace timeline_lauka_app
{
    class TimelineItem : TableEntity
    {
        public string Text { get; set; }
        public string TimeText { get; set; }
        public DateTime OrderByTime { get; set; }
    }
}
