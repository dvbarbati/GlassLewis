using GlassLewis.Core.Messages;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace GlassLewis.Core.DomainObjetcs
{
    public abstract class Entity
    {
        public Guid Id { get; set; }

        private List<Event> _notifications;
        public IReadOnlyCollection<Event> Notifications => _notifications.AsReadOnly();

        protected Entity()
        {
            Id = Guid.NewGuid();
            _notifications = new List<Event>();
        }

        public void AddEvent(Event evt)
        {
            _notifications = _notifications ?? new List<Event>();
            _notifications.Add(evt);
        }

        public void RemoveEvent(Event evt)
        {
            _notifications?.Remove(evt);
        } 


        public void ClearEvents()
        {
            _notifications?.Clear();
        }
    }
}
