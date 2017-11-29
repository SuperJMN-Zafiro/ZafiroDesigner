using System;
using System.Collections.Generic;
using Xunit;

namespace Designer.Model.Tests
{
    public class UnitTest1
    {
        public static IEnumerable<object[]> Data =>
            new List<object[]>
            {
                new object[] { new Point(0,0), new Point(10,10), new Rect(0,0,10,10) },
                new object[] { new Point(50,20), new Point(30,60), new Rect(30,20,20,40) },
            };


        [Theory]
        [MemberData(nameof(Data))]
        public void Test1(Point p1, Point p2, Rect bounds)
        {
            var sut = new Line();
            sut.SetAbsolutePoints(p1, p2);
            var rect = sut.GetBounds();
            Assert.Equal(bounds, rect);
        }
    }
}
