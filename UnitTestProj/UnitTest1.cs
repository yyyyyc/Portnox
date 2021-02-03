using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using Test.Controllers;
using System.Linq;
using Models;

namespace UnitTestProj
{
    [TestClass]
    public class UnitTest1
    {
        
        [TestMethod]
        /// Add the same switches several times and check that the unique number of switches were added
        public void TestUniqueSwitchesNumber()
        {
            NetworkEvents nw = new NetworkEvents();
            nw.AddEvent(new Event(1, DateTime.Now, "1.1.1.1", 1, "AABBCC000001", "New Device Added #1"));
            nw.AddEvent(new Event(2, DateTime.Now, "1.1.1.2", 1, "AABBCC000002", "New Device Added #2"));
            nw.AddEvent(new Event(2, DateTime.Now, "1.1.1.2", 1, "AABBCC000003", "New Device Added #2 - AGAIN"));
            nw.AddEvent(new Event(1, DateTime.Now, "1.1.1.1", 1, "AABBCC000004", "New Device Added #1 - AGAIN"));

            // Check that only 2 Switches were added
            int switches_count = nw.switches.Count;
            Assert.IsTrue(switches_count == 2);
        }

        [TestMethod]
        /// Check if ports were updated correctly
        public void Test_Unique_Ports_In_Switch()
        {
            NetworkEvents nw = new NetworkEvents();
            nw.AddEvent(new Event(1, DateTime.Now, "1.1.1.1", 11, "AABBCC000001", "New Device Added #1"));
            nw.AddEvent(new Event(1, DateTime.Now, "1.1.1.1", 12, "AABBCC000002", "New Device Added #2"));
            nw.AddEvent(new Event(1, DateTime.Now, "1.1.1.1", 12, "", "Device #2 Removed"));

            Switch sw = nw.switches.Where(s => s.ip == "1.1.1.1").FirstOrDefault();
            Assert.IsTrue(sw.ports.Count == 2);
            
        }

        [TestMethod]
        /// Check port's devices
        public void Test_Port_Devices()
        {
            NetworkEvents nw = new NetworkEvents();
            nw.AddEvent(new Event(1, DateTime.Now, "1.1.1.1", 11, "AABBCC000001", "New Device Added #1"));
            nw.AddEvent(new Event(1, DateTime.Now, "1.1.1.1", 12, "AABBCC000002", "New Device Added #2"));
            nw.AddEvent(new Event(1, DateTime.Now, "1.1.1.1", 12, "", "Device #2 Removed"));

            Switch sw = nw.switches.Where(s => s.ip == "1.1.1.1").FirstOrDefault();

            Port prt11 = sw.ports.Where(p => p.id == 11).FirstOrDefault();
            Port prt12 = sw.ports.Where(p => p.id == 12).FirstOrDefault();

            Assert.IsTrue(prt11.current_device == "AABBCC000001");
            Assert.IsTrue(prt12.current_device == "");
            Assert.IsTrue(prt12.id == 12);
        }

        [TestMethod]
        /// Check Device MAC address from event
        public void Test_Device()
        {
            Device dv = new Device(new Event(1, DateTime.Now, "1.1.1.1", 11, "AABBCC000001", "New Device Added #1"));
            Assert.IsTrue(dv.mac == "AABBCC000001");
        }
    }
}
