using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using BusinessEntities;
using Raven.Abstractions.Indexing;
using Raven.Client.Indexes;

namespace Data.Indexes
{
    public class UsersByTagsIndex : AbstractIndexCreationTask<User>
    {
        public UsersByTagsIndex()
        {
            Map = users => from user in users
                           from tag in user.Tags
                           select new { Tag = tag };

            Index("Tag", FieldIndexing.Analyzed);
        }
    }
}
