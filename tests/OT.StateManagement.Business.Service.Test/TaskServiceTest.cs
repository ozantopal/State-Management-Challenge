using Moq;
using NUnit.Framework;
using OT.StateManagement.Business.Service.Concretes;
using OT.StateManagement.Business.Service.DTOs.Task;
using OT.StateManagement.DataAccess.EF.Repository.Abstracts;
using OT.StateManagement.Domain.Entities;
using System;
using System.Collections.Generic;
using System.Linq;

namespace OT.StateManagement.Business.Service.Test
{
    public class TaskServiceTest
    {
        private List<Task> tasks;
        private Mock<IRepository<Task>> mockTaskRepo;
        private Mock<IRepository<StateChange>> mockStateChangeRepo;
        [SetUp]
        public void Setup()
        {
            tasks = new List<Task>
            {
                new Task
                {
                    Id = Guid.Parse("17007b98-1f5a-4d7c-bd27-f02023999887"),
                    Title = "Test Task1",
                    StateId = Guid.Parse("25688b6a-5731-4b33-9be9-ec14f17089e3"),
                    CreatedAt = new DateTime(2020, 12, 14)
                }
            };

            mockTaskRepo = new Mock<IRepository<Task>>();
            mockTaskRepo.Setup(mfr => mfr.Get())
                .Returns(tasks.AsQueryable());
            mockTaskRepo.Setup(mfr => mfr.Add(It.IsAny<Task>()));
            mockTaskRepo.Setup(mfr => mfr.Update(It.IsAny<Task>()));
            mockTaskRepo.Setup(mfr => mfr.Delete(It.IsAny<Task>()));

            mockStateChangeRepo = new Mock<IRepository<StateChange>>();
            mockStateChangeRepo.Setup(mfr => mfr.Get());
            mockStateChangeRepo.Setup(mfr => mfr.Add(It.IsAny<StateChange>()));
            mockStateChangeRepo.Setup(mfr => mfr.Update(It.IsAny<StateChange>()));
            mockStateChangeRepo.Setup(mfr => mfr.Delete(It.IsAny<StateChange>()));
        }

        [Test]
        public void TaskService_Get_WithCorrectParameter_Returns_Data()
        {
            // Arrange
            var service = new TaskService(mockTaskRepo.Object, mockStateChangeRepo.Object);

            // Act
            var state = service.Get(Guid.Parse("17007b98-1f5a-4d7c-bd27-f02023999887"));

            // Assert
            Assert.IsNotNull(state);
            Assert.AreEqual(Guid.Parse("17007b98-1f5a-4d7c-bd27-f02023999887"), state.Id);
            Assert.AreEqual("Test Task1", state.Title);
        }

        [Test]
        public void TaskService_Get_WithIncorrectParameter_Returns_Null()
        {
            // Arrange
            var service = new TaskService(mockTaskRepo.Object, mockStateChangeRepo.Object);

            // Act
            var state = service.Get(Guid.Empty);

            // Assert
            Assert.IsNull(state);
        }

        [Test]
        public void TaskService_Add_Returns_ParamaterItself()
        {
            // Arrange
            var service = new TaskService(mockTaskRepo.Object, mockStateChangeRepo.Object);
            var stateData = new TaskDto
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
        public void TaskService_Update_WithCorrectParamater_Returns_True()
        {
            // Arrange
            var service = new TaskService(mockTaskRepo.Object, mockStateChangeRepo.Object);

            // Act
            var result = service.Update(Guid.Parse("17007b98-1f5a-4d7c-bd27-f02023999887"), new TaskDto
            {
                Title = "Test Flow2"
            });

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void TaskService_Update_WithIncorrectParamater_Returns_False()
        {
            // Arrange
            var service = new TaskService(mockTaskRepo.Object, mockStateChangeRepo.Object);

            // Act
            var result = service.Update(Guid.Empty, new TaskDto
            {
                Title = "Test Flow2"
            });

            // Assert
            Assert.AreEqual(false, result);
        }

        [Test]
        public void TaskService_Delete_WithCorrectParamater_Returns_True()
        {
            // Arrange
            var service = new TaskService(mockTaskRepo.Object, mockStateChangeRepo.Object);

            // Act
            var result = service.Delete(Guid.Parse("17007b98-1f5a-4d7c-bd27-f02023999887"));

            // Assert
            Assert.AreEqual(true, result);
        }

        [Test]
        public void TaskService_Delete_WithIncorrectParamater_Returns_False()
        {
            // Arrange
            var service = new TaskService(mockTaskRepo.Object, mockStateChangeRepo.Object);

            // Act
            var result = service.Delete(Guid.Empty);

            // Assert
            Assert.AreEqual(false, result);
        }
    }
}
