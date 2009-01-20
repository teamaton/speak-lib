using System;
using System.Collections.Generic;
using System.Linq;

namespace SpeakTag
{
    public class TagPrototype
    {
       
        public TagPrototype()
        {
            
        }

        public TagPrototype(Type targetType)
        {
            if ((typeof (ITaggable).IsAssignableFrom(targetType)))
            {
                TargetType = targetType;
            }
            else
            {
                throw new ArgumentException("Cannot create tag prototype for the given type " +
                                            "because it does not implement ITaggable.", "targetType");
            }
        }

        public Type TargetType { get; private set; }

        public string TargetTypeName
        {
            get
            {
                return TargetType.AssemblyQualifiedName;
            }
        set
            {
                if (TargetType == null)
                    TargetType = Type.GetType(value);
                else
                    throw new InvalidOperationException("Cannot change target type of an already initialized prototype.");
            }
        }

        public int Id { get; set; }

        public string Text { get; set; }

        public DateTime DateCreated { get; set; }

        public DateTime DateModified { get; set; }

     }
}