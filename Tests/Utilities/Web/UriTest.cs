using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SpeakFriend.Utilities.Web;

namespace Tests.Utilities.Web
{
    [TestFixture]
    public class UriTest
    {
        [Test]
        public void Subdomain()
        {
            var domain = "speak-lib.com";
            var url = "http://" + domain;
            var uri = new Uri(url);
            var subdomain = UriUtils.SubdomainString(uri, domain);
            Assert.AreEqual("", subdomain);

            url = "http://www." + domain;
            uri = new Uri(url);
            subdomain = UriUtils.SubdomainString(uri, domain);
            Assert.AreEqual("www", subdomain);

            url = "http://en." + domain;
            uri = new Uri(url);
            subdomain = UriUtils.SubdomainString(uri, domain);
            Assert.AreEqual("en", subdomain);

            url = "http://www.en." + domain;
            uri = new Uri(url);
            subdomain = UriUtils.SubdomainString(uri, domain);
            Assert.AreEqual("www.en", subdomain);
        }

        [Test]
        public void SubdomainNotWww()
        {
            var domain = "speak-lib.com";
            var url = "http://" + domain;
            var uri = new Uri(url);
            var subdomain = UriUtils.FirstSubdomainNotWww(uri, domain);
            Assert.AreEqual("", subdomain);

            url = "http://www." + domain;
            uri = new Uri(url);
            subdomain = UriUtils.FirstSubdomainNotWww(uri, domain);
            Assert.AreEqual("", subdomain);

            url = "http://en." + domain;
            uri = new Uri(url);
            subdomain = UriUtils.FirstSubdomainNotWww(uri, domain);
            Assert.AreEqual("en", subdomain);

            url = "http://www.en." + domain;
            uri = new Uri(url);
            subdomain = UriUtils.FirstSubdomainNotWww(uri, domain);
            Assert.AreEqual("en", subdomain);
        }

        [Test]
        public void ChangeSubdomain()
        {
            var domain = "speak-lib.com";
            var url = "http://en." + domain;
            var uri = new Uri(url);
            var newUrl = UriUtils.ChangeSubdomain(uri, domain, "pl");
            Assert.AreEqual("http://pl." + domain + "/", newUrl);

            url = "http://www." + domain;
            uri = new Uri(url);
            newUrl = UriUtils.ChangeSubdomain(uri, domain, "pl");
            Assert.AreEqual("http://pl." + domain + "/", newUrl);
        }
    }
}
