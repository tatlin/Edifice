using System;
using EdificeDb;

namespace EdificeCore
{
    public class Element : IEdificeElement
    {
        public Guid Id { get; private set; }
        public string Name { get; set; }
        public DateTime CreatedAt { get; private set; }
        public DateTime UpdatedAt { get; set; }

        protected Element()
        {
            Id = Guid.NewGuid();
            CreatedAt = DateTime.UtcNow;
        }

        protected Element(Guid id)
        {
            Id = id;
            CreatedAt = DateTime.UtcNow;
        }
    }
}
