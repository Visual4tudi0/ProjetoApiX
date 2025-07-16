using System;

 namespace ClassLibraryDomain.Entity
{


    public abstract class EntityBase
    {
        public Guid Id { get; set; }

        protected EntityBase()
        {
            Id = Guid.NewGuid();
        }
    }
}