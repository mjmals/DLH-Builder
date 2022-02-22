﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder
{
    public class DataArtifact
    {
        public string Name { get; set; }

        public string Description { get; set; }

        public DataArtifactFormat Format { get; set; }

        public DataArtifactSchemaCollection Schemas 
        { 
            get
            {
                if(schemas == null)
                {
                    schemas = new DataArtifactSchemaCollection();
                }
                return schemas;
            }
            set => schemas = value; 
        }

        private DataArtifactSchemaCollection schemas { get; set; }

        public DataArtifactSource Source { get; set; }

        public DataArtifactSchemaItemCollection SourceSchema { get; set; }

        public DataArtifactSchemaItemMapping SchemaMapping { get; set; }
    }
}
