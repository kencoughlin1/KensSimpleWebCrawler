using System;
using KensSimpleWebCrawler;
using Microsoft.VisualStudio.TestTools.UnitTesting;

namespace UnitTestKensSimpleWebCrawler
{
    [TestClass]
    public class ParameterAndUrlValidationTests
    {
        [TestMethod]
        public void AreThereAnyArgsPassedInNoArgs()
        {
            var args = new string[] { };
           Assert.IsFalse( ArgsChecker.ValidateConsoleArguments(args));
        }

        [TestMethod]
        public void AreThereAnyArgsPassedInOneArgs()
        {
            var args = new string[] {""};
            Assert.IsTrue(ArgsChecker.ValidateConsoleArguments(args));
        }

        [TestMethod]
        public void AreTheyArgsNull()
        {
            string[] args = null;
            Assert.IsFalse(ArgsChecker.ValidateConsoleArguments(args));
        }

        [TestMethod]
        public void AValidURLIsInArgs()
        {
            var args = "https://blog.codinghorror.com/";
            Assert.IsTrue(ArgsChecker.ValidateURL(args));
        }

        [TestMethod]
        public void AValidURLIsNotInArgs()
        {
            var args = "";
            Assert.IsFalse(ArgsChecker.ValidateURL(args));
        }

        [TestMethod]
        public void RetriveAValidURLIsInArgs()
        {
            var args = new string[] { "https://blog.codinghorror.com/"}; 
            Assert.AreEqual(ArgsChecker.ExtractURLFromConsoleArguments(args), "https://blog.codinghorror.com/");
        }

        [TestMethod]
        public void RetriveAValidURLIsNotInArgs()
        {
            var args = new string[] { "" };
            Assert.AreEqual(ArgsChecker.ExtractURLFromConsoleArguments(args),"");
        }

        [TestMethod]
        public void RetriveAValidURLIsInMultipleArgs()
        {
            var args = new string[] { "https://blog.codinghorror.com/","","Hello" };
            Assert.AreEqual(ArgsChecker.ExtractURLFromConsoleArguments(args), "https://blog.codinghorror.com/");
        }
    }
}
