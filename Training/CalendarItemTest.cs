using System;
using System.Linq;
using Shouldly;
using Xunit;

namespace Training
{
    public class WhenCalendarItem
    {
        [Fact]
        public void StartsOnFirstDayOfCurrentYear_MaybeSplit_ReturnsItem()
        {
            CalendarItem item = NewCalendarItem(
                new DateTime(2018, 1, 1), 
                new DateTime(2018, 1, 2, 23, 59, 59));

            CalendarItem[] result = item.MaybeSplit(2018).ToArray();

            result.ShouldHaveSingleItem();
            result[0].ShouldBe(item);
        }

        [Fact]
        public void IsDuringYear_MaybeSplit_ReturnsItem()
        {
            CalendarItem item = NewCalendarItem(
                new DateTime(2018, 6, 1), 
                new DateTime(2018, 7, 1));

            CalendarItem[] result = item.MaybeSplit(2018).ToArray();

            result.ShouldHaveSingleItem();
            result[0].ShouldBe(item);
        }

        [Fact]
        public void StartsOnFirstDayOfNextYear_MaybeSplit_ReturnsItem()
        {
            CalendarItem item = NewCalendarItem(
                new DateTime(2019, 1, 1), 
                new DateTime(2019, 1, 2));

            CalendarItem[] result = item.MaybeSplit(2018).ToArray();

            result.ShouldHaveSingleItem();
            result[0].ShouldBe(item);
        }


        [Fact]
        public void EndsOnLastDayOfPreviousYear_MaybeSplit_ReturnsItem()
        {
            CalendarItem item = NewCalendarItem(
                new DateTime(2017, 12, 1), 
                new DateTime(2017, 12, 31, 23, 59, 59));

            CalendarItem[] result = item.MaybeSplit(2018).ToArray();

            result.ShouldHaveSingleItem();
            result[0].ShouldBe(item);
        }
        
        [Fact]
        public void EndsOnEndOfLastDayOfCurrentYear_MaybeSplit_ReturnsItem()
        {
            CalendarItem item = NewCalendarItem(
                new DateTime(2018, 12, 30), 
                new DateTime(2018, 12, 31, 23, 59, 59));

            CalendarItem[] result = item.MaybeSplit(2018).ToArray();

            result.ShouldHaveSingleItem();
            result[0].ShouldBe(item);
        }


        [Fact]
        
        public void WhenStartsInPreviousYearAndEndsInCurrentYear_MaybeSplit_ReturnsTwoItems()
        {
            CalendarItem item = NewCalendarItem(
                new DateTime(2017, 12, 31), 
                new DateTime(2018, 1, 1, 23, 59, 59));

            CalendarItem[] result = item.MaybeSplit(2018).ToArray();

            result.Length.ShouldBe(2);
            result[0].Id.ShouldBe(item.Id);
            result[0].Title.ShouldBe(item.Title);
            result[0].StartDate.ShouldBe(item.StartDate);
            result[0].EndDate.ShouldBe(new DateTime(2017, 12, 31, 23, 59, 0));

            result[1].Id.ShouldBe(item.Id);
            result[1].Title.ShouldBe(item.Title);
            result[1].StartDate.ShouldBe(new DateTime(2018, 1, 1));
            result[1].EndDate.ShouldBe(item.EndDate);
        }

        [Fact(Skip = "FIXME")]
        public void StartsDuringYearAndEndsAfterYear_MaybeSplit_ReturnsTwoItems()
        {
            CalendarItem item = NewCalendarItem(
                new DateTime(2018, 12, 20), 
                new DateTime(2019, 1, 5));

            CalendarItem[] result = item.MaybeSplit(2018).ToArray();

            result.Length.ShouldBe(2);
            result[0].Id.ShouldBe(item.Id);
            result[0].Title.ShouldBe(item.Title);
            result[0].StartDate.ShouldBe(item.StartDate);
            result[0].EndDate.ShouldBe(new DateTime(2018, 12, 31, 23, 59, 0));

            result[1].Id.ShouldBe(item.Id);
            result[1].Title.ShouldBe(item.Title);
            result[1].StartDate.ShouldBe(new DateTime(2019, 1, 1));
            result[1].EndDate.ShouldBe(item.EndDate);
        }

        [Fact(Skip = "FIXME")]
        public void StartsInPreviousYearAndEndsAfterYear_MaybeSplit_ReturnsThreeItems()
        {
            CalendarItem item = NewCalendarItem(
                new DateTime(2017, 12, 20), 
                new DateTime(2019, 1, 5));

            CalendarItem[] result = item.MaybeSplit(2018).ToArray();

            result.Length.ShouldBe(3);
            result[0].Id.ShouldBe(item.Id);
            result[0].Title.ShouldBe(item.Title);
            result[0].StartDate.ShouldBe(item.StartDate);
            result[0].EndDate.ShouldBe(new DateTime(2017, 12, 31, 23, 59, 0));

            result[1].Id.ShouldBe(item.Id);
            result[1].Title.ShouldBe(item.Title);
            result[1].StartDate.ShouldBe(new DateTime(2018, 1, 1));
            result[1].EndDate.ShouldBe(new DateTime(2018, 12, 31, 23, 59, 0));

            result[2].Id.ShouldBe(item.Id);
            result[2].Title.ShouldBe(item.Title);
            result[2].StartDate.ShouldBe(new DateTime(2019, 1, 1));
            result[2].EndDate.ShouldBe(item.EndDate);
        }

        [Theory(Skip = "FIXME if you can")]
        [InlineData(DateTimeKind.Local)]
        [InlineData(DateTimeKind.Utc)]
        public void DatesAreInUtc_MaybeSplit_ReturnsItemsWithUtcTime(DateTimeKind kind)
        {
            CalendarItem item = NewCalendarItem(
                new DateTime(2018, 12, 20, 0, 0, 0, kind),
                new DateTime(2019, 1, 5, 0, 0, 0, kind));
            
            CalendarItem[] result = item.MaybeSplit(2018).ToArray();

            result[0].StartDate.Kind.ShouldBe(kind);
            result[0].EndDate.Kind.ShouldBe(kind);
            result[1].StartDate.Kind.ShouldBe(kind);
            result[1].EndDate.Kind.ShouldBe(kind);
        }

       
        private static CalendarItem NewCalendarItem(DateTime startDate, DateTime dateTime)
        {
            return new CalendarItem
            {
                Id = 1,
                StartDate = startDate,
                EndDate = dateTime,
                Title = "Title",
            };
        }
    }
}