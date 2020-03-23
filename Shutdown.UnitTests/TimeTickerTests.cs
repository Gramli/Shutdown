using Microsoft.VisualStudio.TestTools.UnitTesting;
using Shutdown.Models;
using System.Threading;

namespace Shutdown.UnitTests
{
    [TestClass]
    public class TimeTickerTests
    {
        [TestMethod]
        public void RunTests()
        {
            var intervals = (uint)0;
            var time = new System.TimeSpan(0, 0, 4);
            var wait = new ManualResetEvent(false);

            var ticker = new TimeTicker(2);
            ticker.IntervalElapsed += () =>
            {
                intervals += 1;
            };

            ticker.Elapsed += () =>
            {
                wait.Set();
            };

            for (int i = 0; i < 3; i++)
            {
                intervals = 0;
                wait.Reset();

                ticker.Start(time);

                wait.WaitOne();

                Assert.AreEqual((uint)2, intervals);

                Assert.AreEqual(0, ticker.RestTime.TotalSeconds);

                ticker.Clear();

                Assert.AreEqual(time.TotalSeconds, ticker.RestTime.TotalSeconds);
            }

        }
    }
}
