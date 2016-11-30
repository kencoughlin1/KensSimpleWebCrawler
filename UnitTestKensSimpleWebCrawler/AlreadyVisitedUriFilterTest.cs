using System;
using Microsoft.VisualStudio.TestTools.UnitTesting;
using KensSimpleWebCrawler;

namespace UnitTestKensSimpleWebCrawler
{
    [TestClass]
    public class AlreadyVisitedUriFilterTest
    {

        const string WWWUrl = "http://www.ThisIsATestUrl.com";
        const string NoWWWUrl = "http://ThisIsATestUrl.com";
        const string BrokenUrl = "htt://ThisIsATestUrl";

        [TestMethod]
        public void RemovalOfWWWFromUrlWithWWW()
        {
            Assert.AreEqual(new Uri(NoWWWUrl), URLS.RemoveWWWfromUrl(WWWUrl));
        }

        [TestMethod]
        public void RemovalOfWWWFromUrlWithOutWWW()
        {
            Assert.AreEqual(new Uri(NoWWWUrl), URLS.RemoveWWWfromUrl(NoWWWUrl));
        }

        [TestMethod]
        public void NotAValidUrl()
        {
            Assert.IsFalse(URLS.IsValidUri( BrokenUrl));
        }

        [TestMethod]
        public void AValidUrl()
        {
            Assert.IsTrue(URLS.IsValidUri(WWWUrl));
        }

    }
}
