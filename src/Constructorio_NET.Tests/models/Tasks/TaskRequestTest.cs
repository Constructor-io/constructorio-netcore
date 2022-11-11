using System;
using Constructorio_NET.Models.Tasks;
using NUnit.Framework;

namespace Constructorio_NET.Tests
{
    [TestFixture]
    public class TaskRequestTest
    {
        private readonly int TaskId = 1234;

        [Test]
        public void TaskRequestSetsId()
        {
            TaskRequest req = new TaskRequest(TaskId);

            Assert.AreEqual(this.TaskId, req.TaskId);
        }
    }
}
