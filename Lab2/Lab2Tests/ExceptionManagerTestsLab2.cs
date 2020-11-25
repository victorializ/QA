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
            var loggerFactory = new LoggerFactory();
            var loggerMock = new Mock<ILogger>();
            var ex = new AccessViolationException();
            loggerMock.Setup(x => x.Log(ex)).Returns(false);
            loggerFactory.SetLogger(loggerMock.Object);
            var exManager = new ExceptionManager(loggerFactory);

            exManager.SendToLogger(ex);

            Assert.AreEqual(1, exManager.FailedRequestsCounter);
        }

        [Test]
        public void FailedRequestsCounter_Is1_If2ExceptionsOccurButOnly1Logs()
        {
            var loggerFactory = new LoggerFactory();
            var loggerMock = Mock.Of<ILogger>
                (
                l => l.Log(It.Is<AccessViolationException>(ex => ex.GetType() == typeof(AccessViolationException))) == true
                && l.Log(It.Is<NullReferenceException>(ex => ex.GetType() == typeof(NullReferenceException))) == false
                );
            loggerFactory.SetLogger(loggerMock);
            var exManager = new ExceptionManager(loggerFactory);

            exManager.SendToLogger(new AccessViolationException());
            exManager.SendToLogger(new NullReferenceException());

            Assert.AreEqual(1, exManager.FailedRequestsCounter);
        }

        [Test]
        public void FailedRequestsCounter_Is0_IfNoExceptionOccurs()
        {
            var loggerMock = new Mock<ILogger>();
            var loggerFactory = new LoggerFactory();
            var ex = new AccessViolationException();
            loggerMock.Setup(x => x.Log(ex)).Returns(true);
            loggerFactory.SetLogger(loggerMock.Object);
            var exManager = new ExceptionManager(loggerFactory);

            exManager.SendToLogger(ex);

            Assert.AreEqual(0, exManager.FailedRequestsCounter);
        }

    }
}
