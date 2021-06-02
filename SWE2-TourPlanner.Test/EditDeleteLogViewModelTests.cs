using System;
using System.Collections.Generic;
using log4net;
using Moq;
using NUnit.Framework;
using SWE2_TourPlanner.Services;
using SWE2_TourPlanner.DAL;
using SWE2_TourPlanner.Factory.Window;
using SWE2_TourPlanner.Models;
using SWE2_TourPlanner.ViewModels;

namespace SWE2_TourPlanner.Test
{
    public class EditDeleteLogViewModelTests
    {
        private EditDeleteLogViewModel _editDeleteLogViewModel;
        private Mock<ILog> _loggerMock;
        private Mock<IWindowFactory> _editWindowFactoryMock;
        private Mock<object> _senderMock;

        [SetUp]
        public void Setup()
        {
            _loggerMock = new Mock<ILog>();
            _senderMock = new Mock<object>();
            _editWindowFactoryMock = new Mock<IWindowFactory>();
            _editDeleteLogViewModel = new EditDeleteLogViewModel(_editWindowFactoryMock.Object, _loggerMock.Object);

            _editDeleteLogViewModel.LogId = String.Empty;
        }

        [Test]
        public void Test_EditLogWithEmptyLogId()
        {
            _editDeleteLogViewModel.EditLog(_senderMock.Object);

            Assert.AreEqual("No Log chosen!", ErrorSingleton.GetInstance.ErrorText);
        }

        [Test]
        public void Test_DeleteLogWithEmptyLogId()
        {
            _editDeleteLogViewModel.DeleteLog(_senderMock.Object);

            Assert.AreEqual("No Log chosen!", ErrorSingleton.GetInstance.ErrorText);
        }
    }
}