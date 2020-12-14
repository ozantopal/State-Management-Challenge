using Moq;
using NUnit.Framework;
using OT.StateManagement.Business.Service.Concretes;
using OT.StateManagement.Business.Service.DTOs.State;
using OT.StateManagement.DataAccess.EF.Repository.Abstracts;
using OT.StateManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OT.StateManagement.Business.Service.Test
{
    public class StateServiceTest
    {
        private List<State> states;
        private Mock<IRepository<State>> mockStateRepo;
        [SetUp]
        public void Setup()
        {
            states = new List<State>
            {
                new State
                {
                    Id = Guid.Parse("17007b98-1f5a-4d7c-bd27-f02023999887"),
                    Title = "Test State1",
                    FlowId = Guid.Parse("25688b6a-5731-4b33-9be9-ec14f17089e3"),
                    CreatedAt = new DateTime(2020, 12, 14)
                }
            };

            mockStateRepo = new Mock<IRepository<State>>();
            mockStateRepo.Setup(mfr => mfr.Get())
                .Returns(states.AsQueryable());
            mockStateRepo.Setup(mfr => mfr.Add(It.IsAny<State>()));
            mockStateRepo.Setup(mfr => mfr.Update(It.IsAny<State>()));
            mockStateRepo.Setup(mfr => mfr.Delete(It.IsAny<State>()));
        }

        [Test]
        public void StateService_Get_WithCorrectParameter_Returns_Data()
        {
            // Arrange
            var service = new StateService(mockStateRepo.Object);

            // Act
            var state = service.Get(Guid.Parse("17007b98-1f5a-4d7c-bd27-f02023999887"));

            // Assert
            Assert.IsNotNull(state);
            Assert.AreEqual(Guid.Parse("17007b98-1f5a-4d7c-bd27-f02023999887"), state.Id);
            Assert.AreEqual("Test State1", state.Title);
        }

        [Test]
        public void StateService_Get_WithIncorrectParameter_Returns_Null()
        {
            // Arrange
            var service = new StateService(mockStateRepo.Object);

            // Act
            var state = service.Get(Guid.Empty);

            // Assert
            Assert.IsNull(state);
        }

        [Test]
        public void StateService_Add_Returns_ParamaterItself()
        {
            // Arrange
            var service = new StateService(mockStateRepo.Object);
            var stateData = new StateDto
            {
                Id = Guid.NewGuid(),
                Title = "Test Flow"
            };

            // Act
            var state = service.Add(stateData);

            // Assert
            Assert.IsNotNull(state);
            Assert.AreEqual(stateData.Id, state.Id);
            Assert.AreEqual(stateData.Title, state.Title);
        }

        [Test]
        public void StateService_Update_WithCorrectParamater_Returns_True()
        {
            // Arrange
            var service = new StateService(mockStateRepo.Object);

            // Act
            var result = service.Update(Guid.Parse("17007b98-1f5a-4d7c-bd27-f02023999887"), new StateDto
            {
                Title = "Test Flow2"
            });

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void StateService_Update_WithIncorrectParamater_Returns_False()
        {
            // Arrange
            var service = new StateService(mockStateRepo.Object);

            // Act
            var result = service.Update(Guid.Empty, new StateDto
            {
                Title = "Test Flow2"
            });

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void StateService_Delete_WithCorrectParamater_Returns_True()
        {
            // Arrange
            var service = new StateService(mockStateRepo.Object);

            // Act
            var result = service.Delete(Guid.Parse("17007b98-1f5a-4d7c-bd27-f02023999887"));

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void StateService_Delete_WithIncorrectParamater_Returns_False()
        {
            // Arrange
            var service = new StateService(mockStateRepo.Object);

            // Act
            var result = service.Delete(Guid.Empty);

            // Assert
            Assert.AreEqual(false, result);
        }
    }
}
