using NUnit.Framework;
using System;
using System.Runtime.InteropServices;
using Lab2;

namespace Lab2Tests
{
    [TestFixture]
    public class ExceptionManagerTestsLab1
    {
        [TestCase(typeof(AccessViolationException))]
        [TestCase(typeof(ExternalException))]
        [TestCase(typeof(COMException))]
        public void IsExceptionCritical_NonCriticalException_ReturnFalse(Type exceptionType)
        {
            Exception ex = (Exception)Activator.CreateInstance(exceptionType);
            ExceptionManager exceptionManager = new ExceptionManager();

            Assert.IsFalse(exceptionManager.IsExceptionCritical(ex));
        }

        [TestCase(typeof(ArgumentException))]
        [TestCase(typeof(ArgumentNullException))]
        [TestCase(typeof(ArgumentOutOfRangeException))]
        public void IsExceptionCritical_CriticalException_ReturnTrue(Type exceptionType)
        {
            Exception ex = (Exception)Activator.CreateInstance(exceptionType);
            ExceptionManager exceptionManager = new ExceptionManager();

            Assert.IsTrue(exceptionManager.IsExceptionCritical(ex));
        }

        [Test]
        public void IsExceptionCritical_NullException_ReturnFalse()
        {
            ExceptionManager exceptionManager = new ExceptionManager();
            Exception ex = null;

            Assert.IsFalse(exceptionManager.IsExceptionCritical(ex));
        }

        [Test]
        public void HandleException_CriticalException_OnlyCriticalCounterIncreased()
        {
            ExceptionManager exceptionManager = new ExceptionManager();
            Exception ex = new ArgumentException();

            exceptionManager.HandleException(ex);

            Assert.IsTrue(exceptionManager.CriticalExceptionsCounter == 1 && exceptionManager.NonCriticalExceptionsCounter == 0);
        }

        [Test]
        public void HandleException_NonCriticalException_OnlyNonCriticalCounterIncreased()
        {
            ExceptionManager exceptionManager = new ExceptionManager();
            Exception ex = new AccessViolationException();

            exceptionManager.HandleException(ex);

            Assert.IsTrue(exceptionManager.NonCriticalExceptionsCounter == 1 && exceptionManager.CriticalExceptionsCounter == 0);
        }

        [Test]
        public void HandleException_NullException_NoChanges()
        {
            ExceptionManager exceptionManager = new ExceptionManager();
            Exception ex = null;

            exceptionManager.HandleException(ex);

            Assert.IsTrue(exceptionManager.NonCriticalExceptionsCounter == 0 && exceptionManager.CriticalExceptionsCounter == 0);
        }
    }
}