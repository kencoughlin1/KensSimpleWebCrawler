using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KensSimpleWebCrawler;
using System.Collections.Generic;

namespace UnitTestKensSimpleWebCrawler
{

   
    

    [TestClass]
    public  class ExcludeRootUriFilterTests
    {

        const string OriginUrl = "http://www.ThisIsATestUrl.com";
        const string domainTestUrl = "http://ThisIsATestUrl.com";
        const string OtherDomainUrl = "http://www.ThisIsATestUrl.com/content";


        IUriFilter filters;

        [TestInitialize]
        public void RunBeforeEachTest()
        {
            filters = new ExcludeRootUriFilter(new Uri(OriginUrl));
        }

        [TestMethod]
        public void ThisIsDifferentFromRoot()
        {
            List<Uri> expected = new List<Uri>();
            expected.Add(new Uri(OtherDomainUrl));
            //list in should equal list out
            List<Uri> actual = filters.Filter(expected);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ThisIsSameAsRoot()
        {
            List<Uri> expected = new List<Uri>();
            expected.Add(new Uri(OriginUrl));
            //list in should equal list out
            List<Uri> actual = filters.Filter(expected);
            CollectionAssert.AreNotEqual(expected, actual);
        }

        [TestMethod]
        public void ThisIsSameAsRootDomain()
        {
            List<Uri> expected = new List<Uri>();
            expected.Add(new Uri(domainTestUrl));
            //list in should equal list out
            List<Uri> actual = filters.Filter(expected);
            CollectionAssert.AreNotEqual(expected, actual);
        }

    }
}

