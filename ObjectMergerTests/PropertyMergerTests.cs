using Microsoft.VisualStudio.TestTools.UnitTesting;
using ObjectMerger;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ObjectMerger.Tests
{
    [TestClass()]
    public class PropertyMergerTests
    {
        Model1 m1 = new();
        Model2 m2 = new();
        [TestInitialize]
        public void Init()
        {
            m1 = new() { Name = "Test Name 123" };
            m2 = new() { Name2 = "Test Name" };
        }
        [TestMethod()]
        public void CreateDynamicObjectTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ObjectMergeTest()
        {
            dynamic obj = PropertyMerger.ObjectMerge(m1, m2, "Tested Named Object");
            Assert.IsTrue(obj.Name == m1.Name);
        }

        [TestMethod()]
        public void MergeWithListTest()
        {
            var ps = new string[] { "Prop1", "Prop2" };
            dynamic obj = PropertyMerger.MergeWithList(m1, "Pivoted", ps);
            var testValue = "Palash";
            var lst = new KeyValuePair<string, object?>[] { new(ps[0], testValue) };
            PropertyMerger.AssignProperties(obj, lst);
            Assert.AreEqual(obj.Prop1, testValue);
        }
    }
    public class Model1
    {
        public string Name { get; set; } = string.Empty;
    }
    public class Model2
    {
        public string Name2 { get; set; } = string.Empty;
    }
}