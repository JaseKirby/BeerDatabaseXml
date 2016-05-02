using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using BootstrapGenerator;
using System.Collections.Generic;

namespace BootstrapGeneratorTests
{
    [TestClass]
    public class FunctionsTests
    {
        [TestMethod]
        public void TestStripScopeObjString()
        {
            string testScopeStrSing = "$scope.Object = {}";
            string expected = "$scope.Object";
            string actual = Functions.StripScopeObjString(testScopeStrSing);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestCreateAngularPropView()
        {
            string objName = "Person";
            string propName = "Name";
            string expected = "Person.Name";
            string actual = Functions.CreateAngularPropView(objName, propName);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestCreateAngularExpression()
        {
            string objName = "Person";
            string propName = "Name";
            string expected = "{{Person.Name}}";
            string actual = Functions.CreateAngularExpression(objName, propName);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestConvertCamelCase()
        {
            string cCase = "FirstName";
            string expected = "First Name";
            string actual = Functions.ConvertCamelCase(cCase);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestCapitalizeFirstLetter()
        {
            string actual = "first Name";
            string expected = "First Name";
            actual = Functions.CapitalizeFirstLetter(actual);
            Assert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void TestJoinStringList()
        {
            List<string> singList = new List<string>() { "table" };
            List<string> multList = new List<string>() { "table", "table-bordered" };

            string singExpect = "table";
            string singActual = Functions.JoinStringList(singList);
            Assert.AreEqual(singExpect, singActual);

            string multExpect = "table table-bordered";
            string multActual = Functions.JoinStringList(multList);
            Assert.AreEqual(multExpect, multActual);
        }
    }
}
