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
        Model1 m1;
        Model2 m2;
        [TestInitialize]
        public void Init()
        {
            m1 = new() { Name = "Boom" };
            m2 = new() { Name2 = "Shaka Laka" };
        }
        [TestMethod()]
        public void CreateDynamicObjectTest()
        {
            Assert.Fail();
        }

        [TestMethod()]
        public void ObjectMergeTest()
        {
            dynamic obj = PropertyMerger.ObjectMerge(m1, m2, "Dhoom");
            Assert.IsTrue(obj.Name == m1.Name);
        }

        [TestMethod()]
        public void MergeWithListTest()
        {
            var ps = new string[] { "Prop1", "Prop2" };
            dynamic obj = PropertyMerger.MergeWithList(m1, "Pivoted", ps);
            var testValue = "Palash";
            PropertyMerger.AssignProperties(obj, new KeyValuePair<string, object?>[] { new(ps[0], testValue) });
            Assert.AreEqual(obj.Prop1, testValue);
        }
    }
    public class Model1
    {
        public string Name { get; set; }
    }
    public class Model2
    {
        public string Name2 { get; set; }
    }
}