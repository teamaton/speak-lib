using System;

namespace SpeakFriend.Utilities.Tagging
{
    public class Tag
    {
        public Tag(){}
        
        public Tag(ITaggable target, TagPrototype prototype)
        {
            if (prototype.TargetType != target.GetType())
                throw new ArgumentException("The given prototype is not compatible with the given target's type.",
                                            "prototype");

            Target = target;
            Prototype = prototype;
        }

        public int Id { get; set; }
        public ITaggable Target { get; private set; }
        public int TargetId{ get { return Target.Id; } set { } } //empty, NHibernate requirs the setter
        public TagPrototype Prototype { get; set; } //ToDo: prevent setting when not null
        public string Text { get { return Prototype.Text; } }
        public Type TargetType { get { return Prototype.TargetType; } }
        public DateTime DateCreated { get; set; }
        public DateTime DateModified { get; set; }
    }
}