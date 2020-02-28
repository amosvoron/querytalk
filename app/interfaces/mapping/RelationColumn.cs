using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace QueryTalk.Mapper
{
    internal class RelationColumn
    {
        public int RELATION_ID { get; set; }
        public int NODE_ID { get; set; }
        public string COLUMN_NAME { get; set; }
        public int COLUMN_ORDINAL { get; set; }
        public int RELATED_ID { get; set; }
        public string RELATED_COLUMN_NAME { get; set; }
        public int RELATED_COLUMN_ORDINAL { get; set; }
        public int RELATION_TYPE { get; set; }

        // 1 when the relation is mirrored
        internal int IsMirrored { get; set; }

        public override string ToString()
        {
            return String.Format("{0}:{1}, {2}:{3}", NODE_ID, COLUMN_NAME, RELATED_ID, RELATED_COLUMN_NAME);
        }
    }
}
