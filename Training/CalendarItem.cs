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

        public IEnumerable<CalendarItem> SplitItems()
        {
            ICollection<CalendarItem> splittedCalendarItems = new List<CalendarItem>();
            
            DateTime firstDayOfVacantionInThisYear = StartDate;
           

            for (int year= StartDate.Year; year<EndDate.Year; year++)
            {
                var lastDayOfVacantionInThisYear = new DateTime(year, 12, 31, 23, 59, 0, EndDate.Kind);
                var currentYearItem = new CalendarItem
                {
                    Id = Id,
                    Title = Title,
                    StartDate = firstDayOfVacantionInThisYear,
                    EndDate = lastDayOfVacantionInThisYear,
                };
                splittedCalendarItems.Add(currentYearItem);
                firstDayOfVacantionInThisYear = new DateTime(year+1, 1, 1, 0, 0, 0, StartDate.Kind);
            }

            splittedCalendarItems.Add(new CalendarItem
            {
                Id = Id,
                Title = Title,
                StartDate = firstDayOfVacantionInThisYear,
                EndDate = EndDate
                
            });

            return splittedCalendarItems;
        }

        public IEnumerable<CalendarItem> MaybeSplit(int year)
        {
            ICollection<CalendarItem> splittedCalendarItems = new List<CalendarItem>();

            var currentYear = StartDate.Year;
            var nextYear = currentYear + 1; 
            
            var lastDayOfWork = new DateTime(currentYear, 12, 31, 23, 59, 0);
            var firstDayOfTheNextYear = new DateTime(nextYear, 1, 1, 0, 0, 0);

            var lastDayOfTheCurrentYear = new DateTime(currentYear, 12, 31, 23, 59, 59);

            if (EndDate <= lastDayOfTheCurrentYear)
            {
                splittedCalendarItems.Add(this);
            }
            else
            {
                splittedCalendarItems = SplitItems().ToArray();
            }

            return splittedCalendarItems.ToArray();
        }
    }}
