using System;
using EdificeMongoDb;

namespace EdificeDynamo.Wrappers
{
    public abstract class Element : IDisposable
    {
        internal EdificeCore.Element InternalElement { get; set; }

        public void Dispose()
        {
            //Delete();
        }
    }
}
