using Moq;
using NUnit.Framework;
using OT.StateManagement.Business.Service.Concretes;
using OT.StateManagement.Business.Service.DTOs.Flow;
using OT.StateManagement.DataAccess.EF.Repository.Abstracts;
using OT.StateManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OT.StateManagement.Business.Service.Test
{
    public class FlowServiceTest
    {
        private List<Flow> flows;
        private Mock<IRepository<Flow>> mockFlowRepo;
        [SetUp]
        public void Setup()
        {
            flows = new List<Flow>
            {
                new Flow { Id = Guid.Parse("17007b98-1f5a-4d7c-bd27-f02023999887"), Title = "Test Flow1", CreatedAt = new DateTime(2020,12,14) }
            };
            mockFlowRepo = new Mock<IRepository<Flow>>();
            mockFlowRepo.Setup(mfr => mfr.Get())
                .Returns(flows.AsQueryable());
            mockFlowRepo.Setup(mfr => mfr.Add(It.IsAny<Flow>()));
            mockFlowRepo.Setup(mfr => mfr.Update(It.IsAny<Flow>()));
            mockFlowRepo.Setup(mfr => mfr.Delete(It.IsAny<Flow>()));
        }

        [Test]
        public void FlowService_Get_WithCorrectParameter_Returns_Data()
        {
            // Arrange
            var service = new FlowService(mockFlowRepo.Object);

            // Act
            var flow = service.Get(Guid.Parse("17007b98-1f5a-4d7c-bd27-f02023999887"));

            // Assert
            Assert.IsNotNull(flow);
            Assert.AreEqual(Guid.Parse("17007b98-1f5a-4d7c-bd27-f02023999887"), flow.Id);
            Assert.AreEqual("Test Flow1", flow.Title);
        }

        [Test]
        public void FlowService_Get_WithIncorrectParameter_Returns_Null()
        {
            // Arrange
            var service = new FlowService(mockFlowRepo.Object);

            // Act
            var flow = service.Get(Guid.Empty);

            // Assert
            Assert.IsNull(flow);
        }

        [Test]
        public void FlowService_Add_Returns_ParamaterItself()
        {
            // Arrange
            var service = new FlowService(mockFlowRepo.Object);
            var flowData = new FlowDto
            {
                Id = Guid.NewGuid(),
                Title = "Test Flow"
            };

            // Act
            var flow = service.Add(flowData);

            // Assert
            Assert.IsNotNull(flow);
            Assert.AreEqual(flowData.Id, flow.Id);
            Assert.AreEqual(flowData.Title, flow.Title);
        }

        [Test]
        public void FlowService_Update_WithCorrectParamater_Returns_True()
        {
            // Arrange
            var service = new FlowService(mockFlowRepo.Object);

            // Act
            var result = service.Update(Guid.Parse("17007b98-1f5a-4d7c-bd27-f02023999887"), new FlowDto
            {
                Title = "Test Flow2"
            });

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void FlowService_Update_WithIncorrectParamater_Returns_False()
        {
            // Arrange
            var service = new FlowService(mockFlowRepo.Object);

            // Act
            var result = service.Update(Guid.Empty, new FlowDto
            {
                Title = "Test Flow2"
            });

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void FlowService_Delete_WithCorrectParamater_Returns_True()
        {
            // Arrange
            var service = new FlowService(mockFlowRepo.Object);

            // Act
            var result = service.Delete(Guid.Parse("17007b98-1f5a-4d7c-bd27-f02023999887"));

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void FlowService_Delete_WithIncorrectParamater_Returns_False()
        {
            // Arrange
            var service = new FlowService(mockFlowRepo.Object);

            // Act
            var result = service.Delete(Guid.Empty);

            // Assert
            Assert.AreEqual(false, result);
        }
    }
}