using System;
using System.Collections.Generic;
using System.Collections.Specialized;
using System.Linq;
using System.Text;
using NUnit.Framework;
using SpeakFriend.Utilities.Web;

namespace Tests.Utilities.Web
{
    [TestFixture]
    public class UriUtilsTest
    {
        [Test]
        public void Subdomain()
        {
            var domain = "speak-lib.com";
            var url = "http://" + domain;
            var uri = new Uri(url);
            var subdomain = UriUtils.GetSubdomain(uri);
            Assert.AreEqual("", subdomain);

            url = "http://www." + domain;
            uri = new Uri(url);
            subdomain = UriUtils.GetSubdomain(uri);
            Assert.AreEqual("www", subdomain);

            url = "http://en." + domain;
            uri = new Uri(url);
            subdomain = UriUtils.GetSubdomain(uri);
            Assert.AreEqual("en", subdomain);

            url = "http://www.en." + domain;
            uri = new Uri(url);
            subdomain = UriUtils.GetSubdomain(uri);
            Assert.AreEqual("www.en", subdomain);
        }

        [Test]
        public void SubdomainNotWww()
        {
            var url = "http://speak-lib.com";
            var uri = new Uri(url);
            var subdomain = UriUtils.FirstSubdomainNotWww(uri);
            Assert.AreEqual("", subdomain);

            url = "http://www.speak-lib.com";
            uri = new Uri(url);
            subdomain = UriUtils.FirstSubdomainNotWww(uri);
            Assert.AreEqual("", subdomain);

            url = "http://en.speak-lib.com";
            uri = new Uri(url);
            subdomain = UriUtils.FirstSubdomainNotWww(uri);
            Assert.AreEqual("en", subdomain);

            url = "http://www.en.speak-lib.com";
            uri = new Uri(url);
            subdomain = UriUtils.FirstSubdomainNotWww(uri);
            Assert.AreEqual("en", subdomain);
        }

        [Test]
        public void GetDomainPart()
        {
            var domain = "speak-lib.com";
            var url = "http://en." + domain;
            var uri = new Uri(url);
            var newDomain = UriUtils.GetDomainPart(uri);
            Assert.AreEqual(domain, newDomain);

            domain = "speak-lib.com";
            url = "http://" + domain;
            uri = new Uri(url);
            newDomain = UriUtils.GetDomainPart(uri);
            Assert.AreEqual(domain, newDomain);
        }

        [Test]
        public void ReplaceLeftMostSubdomain()
        {
            var domain = "speak-lib.com";
            var url = "http://en." + domain;
            var uri = new Uri(url);
            var newUrl = UriUtils.ReplaceLeftMostSubdomain(uri, "pl");
            Assert.AreEqual("http://pl." + domain + "/", newUrl.AbsoluteUri);

            url = "http://www." + domain;
            uri = new Uri(url);
            newUrl = UriUtils.ReplaceLeftMostSubdomain(uri, "pl");
            Assert.AreEqual("http://pl." + domain + "/", newUrl.AbsoluteUri);

            url = "http://en.stage." + domain;
            uri = new Uri(url);
            newUrl = UriUtils.ReplaceLeftMostSubdomain(uri, "pl");
            Assert.AreEqual("http://pl.stage." + domain + "/", newUrl.AbsoluteUri);

            url = "http://" + domain;
            uri = new Uri(url);
            newUrl = UriUtils.ReplaceLeftMostSubdomain(uri, "pl");
            Assert.AreEqual("http://pl." + domain + "/", newUrl.AbsoluteUri);

            uri = new Uri("http://" + domain + ":443/hallo.aspx?ballo=w");
            newUrl = UriUtils.ReplaceLeftMostSubdomain(uri, "pl");
            Assert.AreEqual("http://pl." + domain + ":443/hallo.aspx?ballo=w", newUrl.AbsoluteUri);
        }

        [Test]
        public void FirstSubdomainNotWww()
        {
            var host = "http://pl.camping.info";
            var subdomain = UriUtils.FirstSubdomainNotWww(new Uri(host));
            Assert.AreEqual("pl", subdomain);

            host = "http://pl.camping-info";
            subdomain = UriUtils.FirstSubdomainNotWww(new Uri(host));
            Assert.AreEqual("pl", subdomain);

            host = "http://sub.pl.camping-info";
            subdomain = UriUtils.FirstSubdomainNotWww(new Uri(host));
            Assert.AreEqual("pl", subdomain);
        }

        [Test]
        public void QueryToNameValueCollection()
        {
            var uri = new Uri("http://camping.info?rules=true&willGrow=yes");
            
            NameValueCollection querySring = UriUtils.GetNameValueCollectionFromQuery(uri.Query);
            Assert.That(querySring["rules"], Is.EqualTo("true"));
            Assert.That(querySring["willGrow"], Is.EqualTo("yes"));


        }
    }
}
