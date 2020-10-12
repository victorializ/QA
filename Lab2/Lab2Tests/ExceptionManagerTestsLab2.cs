using System;
using Lab2;
using NUnit.Framework;

namespace Lab2Tests
{
    [TestFixture]
    public class ExceptionManagerTestsLab2
    {
        [Test]
        public void FailedRequestsCounter_Incremented_IfExceptionOccurs()
        {
            var logger = new Logger();
            logger.SendToLogger(null);
            Assert.AreEqual(1, Logger.FailedRequestsCounter);
        }

        [Test]
        public void FailedRequestsCounter_IsUpdated()
        {
            var logger = new Logger();
            logger.SendToLogger(null);
            logger.SendToLogger(new NullReferenceException());
            Assert.AreEqual(1, Logger.FailedRequestsCounter);
        }

        [Test]
        public void FailedRequestsCounter_Is0_IfNoExceptionOccurs()
        {
            var logger = new Logger();
            logger.SendToLogger(new NullReferenceException());
            Assert.AreEqual(0, Logger.FailedRequestsCounter);
        }

    }
}
