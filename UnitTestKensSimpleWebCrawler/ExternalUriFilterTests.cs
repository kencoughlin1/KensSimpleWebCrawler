using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KensSimpleWebCrawler;
using System.Collections.Generic;

namespace UnitTestKensSimpleWebCrawler
{
    [TestClass]
    public class ExternalUriFilterTests
    {
        const string OriginUrl = "http://www.ThisIsATestUrl.com";
        const string SubDomainUrl = "http://news.ThisIsATestUrl.com";
        const string ExternalUrl = "http://www.externalUrl.com";

        IUriFilter filters;

        [TestInitialize]
        public void RunBeforeEachTest()
        {
            filters = new ExternalUriFilter(new Uri(OriginUrl));
        }

        [TestMethod]
        public void ThisIsASubdomain()
        {
            List<Uri> expected = new List<Uri>();
            expected.Add(new Uri(SubDomainUrl));
            //list in should equal list out
            List<Uri> actual = filters.Filter(expected);
            CollectionAssert.AreEqual(expected, actual);
        }

        [TestMethod]
        public void ThisIsAnExtrnalUrl()
        {
            List<Uri> expected = new List<Uri>();
            expected.Add(new Uri(ExternalUrl));
            //list in should equal list out
            List<Uri> actual = filters.Filter(expected);
            CollectionAssert.AreNotEqual(expected, actual);
        }

    }
}

