using Moq;
using NUnit.Framework;
using System;
using System.Collections.Generic;
using System.Linq;
using TestNinja.Mocking;

namespace TestNinja.UnitTests.Mocking
{
    [TestFixture]
    public class HouseKeeperServiceTests
    {
        private HousekeeperService _service;
        private Mock<IUnitOfWork> _unitOfWork = new Mock<IUnitOfWork>();
        private Mock<IStatementGenerator> _statementGenerator;
        private Mock<IEmailSender> _emailSender;
        private Mock<IXtraMessageBox> _messageBox;
        private readonly DateTime _statementDate = DateTime.Now;
        private string _statementfileName;

        private Housekeeper _housekeeper;

        [SetUp]
        public void Setup()
        {
            _housekeeper = new Housekeeper { Email = "a", FullName = "b", Oid = 1, StatementEmailBody = "c" };
            _unitOfWork = new Mock<IUnitOfWork>();

            _unitOfWork.Setup(uow => uow.Query<Housekeeper>()).Returns(new List<Housekeeper> {
                _housekeeper
            }.AsQueryable());

            _statementfileName = "filename";
            _statementGenerator = new Mock<IStatementGenerator>();
            _statementGenerator.Setup(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate))
                .Returns(() =>_statementfileName);

            _emailSender = new Mock<IEmailSender>();
            _messageBox = new Mock<IXtraMessageBox>();

            _service = new HousekeeperService(_unitOfWork.Object, _statementGenerator.Object, _emailSender.Object, _messageBox.Object);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_generateStatments()
        {
            _service.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate));
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [Test]
        public void SendStatementEmails_HouseKeeperEmailIsNull_ShouldNotGenerateStatement(string email)
        {
            _housekeeper.Email = email;

            _service.SendStatementEmails(_statementDate);

            _statementGenerator.Verify(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate), Times.Never);
        }

        [Test]
        public void SendStatementEmails_WhenCalled_EmailTheStatement()
        {
            
            _statementGenerator.Setup(sg => sg.SaveStatement(_housekeeper.Oid, _housekeeper.FullName, _statementDate)).Returns(_statementfileName);

            _service.SendStatementEmails(_statementDate);

            _emailSender.Verify(es =>
            es.EmailFile(_housekeeper.Email, _housekeeper.StatementEmailBody, _statementfileName, It.IsAny<string>())
            );
        }

        [TestCase(null)]
        [TestCase("")]
        [TestCase(" ")]
        [Test]
        public void SendStatementEmails_StatementFileNameIsNullOrEmptyOrWhiteSpace_ShouldNotEmailTheStatement(string statementName)
        {
            _statementfileName = statementName;

            _service.SendStatementEmails(_statementDate);
            
            VerfiyEmailNotSent();
        }

        [Test]
        public void SendStatementEmails_EmailSendingFails_ShouldDisplayAMessageBox()
        {
            _emailSender.Setup(es => 
                es.EmailFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>()))
                    .Throws<Exception>();



            _service.SendStatementEmails(_statementDate);

            _messageBox.Verify(mb => mb.Show(It.IsAny<string>(), It.IsAny<string>(), MessageBoxButtons.OK));
        }

        private void VerfiyEmailNotSent()
        {
            _emailSender.Verify(es =>
                        es.EmailFile(It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>(), It.IsAny<string>())
                        , Times.Never);
        }
    }
}