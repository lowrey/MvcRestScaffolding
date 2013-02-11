using System;
using System.Diagnostics;
using System.IO;
using System.Threading;
using NUnit.Framework;
using MvcRestScaffoldingLib.Calls;
using MvcRestScaffoldingLib.Models;

namespace MvcRestScaffoldingTest
{
    [TestFixture]
    [Category("Single")]
    public class JobCycleTests
    {
        private string rmAddr;
        private string testCallback;
        private string testFile;
        private long lastId;

        [SetUp]
        public void Init()
        {
            rmAddr = "http://localhost:56459/";
            testCallback = "http://httpbin.com/";
            string startupPath = Directory.GetParent(Directory.GetCurrentDirectory()).Parent.FullName;
            testFile = Path.Combine(startupPath, "json.pdf");
            lastId = TestAddJob();
        }

        [TearDown]
        public void Cleanup()
        {
            TestDeleteJob(lastId);
        }

        [Test]
        public void TestJobCycle()
        {
            TestEditJob(lastId);
            TestGetJob(lastId);
        }

        public long TestAddJob()
        {
            JobCreate jobCall = new JobCreate(rmAddr, testFile, Path.GetExtension(testFile), testCallback);
            var response = jobCall.Execute();
            Assert.True(response.statusCode == StatusCode.Success);
            Assert.IsInstanceOf<long>(response.Id);
            bool largerThanZero = response.Id > 0;
            Assert.True(largerThanZero);
            return response.Id;
        }

        private JobViewModel GetJob(long id)
        {
            var call = new JobGet(rmAddr, id);
            return call.Execute();
        }

        public void TestGetJob(long id)
        {
            var job = GetJob(id);
            TimeSpan timeoutSec = TimeSpan.FromSeconds(60);
            Stopwatch sw = Stopwatch.StartNew();
            while (sw.Elapsed < timeoutSec && !(job.Status == JobStatus.Processed || job.Status == JobStatus.Failed))
            {
                job = GetJob(id);
                Thread.Sleep(1);
            }
            if (sw.Elapsed >= timeoutSec)
                throw new TimeoutException("Timed out wating for processing");
            Assert.That(job.Status, Is.EqualTo(JobStatus.Processed));
        }


        public void TestEditJob(long id)
        {
            string newCallback = "http://httpbin.com/post/";
            JobViewModel job = new JobViewModel(callbackUrl: newCallback, id: id);

            //string newFileLocaton = "test.edit";
            var jobCall = new JobEdit(rmAddr, job);
            var response = jobCall.Execute();
            Assert.True(response.statusCode == StatusCode.Success);
            JobViewModel jobTest = GetJob(response.Id);
            Assert.That(jobTest.CallbackUrl, Is.EqualTo(newCallback));
        }

        public void TestDeleteJob(long id)
        {
            var jobCall = new JobDelete(rmAddr, id);
            var response = jobCall.Execute();
            Assert.True(response.statusCode == StatusCode.Success);
        }
    }
}
