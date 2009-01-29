using System;
using System.Collections;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace SpeakFriend.Utilities.Web
{
    public class RenderDurationFiFo : IEnumerable<RenderDuration>
    {
        private Queue<RenderDuration> _queue;

        public int Size;

        /// <summary>
        /// The size of the FiFo (First in, First out), 
        /// if the amount of objects in the queues reaches the <see cref="Size">Size</see>
        /// an ojbect on the end of the queue will be removed.
        /// </summary>
        /// <param name="size"></param>
        public RenderDurationFiFo(int size)
        {
            Size = size;
            _queue = new Queue<RenderDuration>(size);
        }

        public void Add(RenderDuration renderDuration)
        {
            if (_queue.Count == Size)
                _queue.Dequeue();

            _queue.Enqueue(renderDuration);
        }


        public IEnumerator<RenderDuration> GetEnumerator()
        {
            return _queue.GetEnumerator();
        }

        IEnumerator IEnumerable.GetEnumerator()
        {
            return GetEnumerator();
        }
    }
}
