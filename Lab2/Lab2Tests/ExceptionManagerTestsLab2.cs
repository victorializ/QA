using System;
using Lab2;
using NUnit.Framework;
using Moq;

namespace Lab2Tests
{
    [TestFixture]
    public class ExceptionManagerTestsLab2
    {
        [Test]
        public void FailedRequestsCounter_Is1_IfExceptionOccurs()
        {
            var loggerMock = new Mock<ILogger>();
            var ex = new AccessViolationException();
            loggerMock.Setup(x => x.Log(ex)).Returns(false);
            var exManager = new ExceptionManager(loggerMock.Object);

            exManager.SendToLogger(ex);

            Assert.AreEqual(1, exManager.FailedRequestsCounter);
        }

        [Test]
        public void FailedRequestsCounter_Is1_If2ExceptionsOccurButOnly1Logs()
        {
            var loggerMock = Mock.Of<ILogger>
                (
                l => l.Log(It.Is<AccessViolationException>(ex => ex.GetType() == typeof(AccessViolationException))) == true
                && l.Log(It.Is<NullReferenceException>(ex => ex.GetType() == typeof(NullReferenceException))) == false
                );
            var exManager = new ExceptionManager(loggerMock);

            exManager.SendToLogger(new AccessViolationException());
            exManager.SendToLogger(new NullReferenceException());

            Assert.AreEqual(1, exManager.FailedRequestsCounter);
        }

        [Test]
        public void FailedRequestsCounter_Is0_IfNoExceptionOccurs()
        {
            var loggerMock = new Mock<ILogger>();
            var exManager = new ExceptionManager(loggerMock.Object);
            var ex = new AccessViolationException();
            loggerMock.Setup(x => x.Log(ex)).Returns(true);

            exManager.SendToLogger(ex);

            Assert.AreEqual(0, exManager.FailedRequestsCounter);
        }

    }
}
