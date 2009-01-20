using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SpeakFriend.Utilities.Tagging;
using SpeakTag;
using SpeakTag.Tests._Code;

namespace SpeakTag.Tests.Environment
{
    class Setup : BaseTest
    {
        
        public static void CleanUp()
        {
            //foreach (var type in new[] { typeof(TestTarget), typeof(Tag), typeof(TagPrototype) })
            //    NHibernateHelper.EmptyTable(type);

            throw new NotImplementedException("NHibernateHelpoer");
        }

        public static void CreateTestEnvironment()
        {
            CleanUp();

            //some random objects
            foreach (var name in new[] { "Apple", "Apple Juice", "Banana", "Raw Potato", "Old Shoe", "Car", "Coffee" })
            {
                var item = new TestTarget {Name = name};
                _targetService.Create(item);
            }

            //some possible tags
            foreach (var text in new[] { "Food", "Yummy", "Expensive", "I need it!", "Liquid" })
            {
                var proto = new TagPrototype(typeof (TestTarget)) {Text = text};
                _prototypeService.Create(proto);
            }

        }

        private static void TagObject(string targetName, string tagText)
        {
            TestTarget target = _targetService.GetByName(targetName);
            _tagService.Create(new Tag(target,_prototypeService.GetByTargetTypeAndText(target.GetType(),tagText)));
        }

        public static void TagObjects()
        {
            TagObject("Apple", "Food");
            TagObject("Apple", "Yummy");

            TagObject("Apple Juice", "Food");
            TagObject("Apple Juice", "Yummy");
            TagObject("Apple Juice", "Liquid");

            TagObject("Banana", "Food");
            TagObject("Banana", "Yummy");

            TagObject("Raw Potato", "Food");

            TagObject("Car", "Expensive");

            TagObject("Coffee", "Food");
            TagObject("Coffee", "Yummy");
            TagObject("Coffee", "I need it!");
            TagObject("Coffee", "Liquid");
    
        }

    }
}
