using System;
using System.Runtime.Serialization;
using Autodesk.DesignScript.Runtime;
using DynamoServices;
using EdificeCore;
using EdificeDb;
using EdificeMongoDb;

namespace EdificeDynamo
{
    [IsVisibleInDynamoLibrary(false)]
    public class EdificeObjectManager
    {
        private const string REVIT_TRACE_ID = "{0459D869-0C72-447F-96D8-08A7FB92214B}-REVIT";

        public static Guid GetNextUnusedID()
        {
            return Guid.NewGuid();
        }

        public static TraceableId GetObjectIdFromTrace()
        {
            return TraceUtils.GetTraceData(REVIT_TRACE_ID) as TraceableId;
        }

        public static object GetTracedObjectById(Guid id)
        {
            var connection = new EdificeMongoDbConnection();

            var db = connection.FindDatabaseByName(EdificeMongoDbConnection.DEFAULT_DB_NAME);
            var coll = db.FindElementCollectionByName(EdificeMongoDbConnection.DEFAULT_ELEMENT_COLLECTION_NAME);
            var el = coll.FindElementById(id);
            return el;
        }

        public static void RegisterTraceableObjectForId(TraceableId id, Element element)
        {
            TraceUtils.SetTraceData(REVIT_TRACE_ID, id);
        }

        public static IEdificeElement FindElement()
        {
            var traceId = GetObjectIdFromTrace();

            IEdificeElement element = null;

            if (traceId != null)
            {
                element = (IEdificeElement)GetTracedObjectById(traceId.Guid);
            }

            return element;
        }
    }

    [IsVisibleInDynamoLibrary(false)]
    [Serializable]
    public class TraceableId : ISerializable
    {
        public Guid Guid { get; set; }

        public void GetObjectData(SerializationInfo info, StreamingContext context)
        {
            info.AddValue("Guid", Guid, typeof(int));
        }

        public TraceableId(Guid id)
        {
            Guid = id;
        }

        /// <summary>
        /// Ctor used by the serialisation engine
        /// </summary>
        /// <param name="info"></param>
        /// <param name="context"></param>
        public TraceableId(SerializationInfo info, StreamingContext context)
        {
            Guid = (Guid)info.GetValue("Guid", typeof(Guid));
        }
    }
}
