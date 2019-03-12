using System;
using System.Collections.Generic;
using System.Linq;

namespace Training
{
    public class CalendarItem
    {
        public int Id { set; get; }
        public string Title { set; get; }
        public DateTime StartDate { set; get; }
        public DateTime EndDate { set; get; }

        public IEnumerable<CalendarItem> MaybeSplit(int year)
        {
            ICollection<CalendarItem> splittedCalendarItems = new List<CalendarItem>();
            DateTime date = DateTime.Today;
            var lastDayOfTheCurrentYear = new DateTime(date.Year, 12, 31);
            var firstDayOfTheNextYear = new DateTime(date.Year + 1, 1, 1);
            if (EndDate < lastDayOfTheCurrentYear)
            {
                splittedCalendarItems.Add(this);
            }
            else
            {
                var currentYearItem = new CalendarItem
                {
                    Id = Id,
                    Title = Title,
                    StartDate = StartDate,
                    EndDate = lastDayOfTheCurrentYear,
                };
                var nextYearItem = new CalendarItem
                {
                    Id = Id,
                    Title = Title,
                    StartDate = firstDayOfTheNextYear,
                    EndDate = EndDate,
                };
                splittedCalendarItems.Add(currentYearItem);
                splittedCalendarItems.Add(nextYearItem);
            }

            return splittedCalendarItems.ToArray();
        }
    }}
