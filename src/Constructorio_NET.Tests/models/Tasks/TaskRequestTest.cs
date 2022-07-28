using System;
using System.Collections;
using System.Collections.Generic;
using Constructorio_NET.Models;
using Constructorio_NET.Utils;
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
