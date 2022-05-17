﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Desktop.UI
{ 
    class DataArtifactReferenceNode : ProjectTreeNode
    {
        public DataArtifactReferenceNode(DataArtifactReference reference, IDataStage stage, IDataApplication application)
        {
            Reference = reference;
            Stage = stage;
            Application = application;
            Text = reference.ReferencedArtifact.Name;
            Nodes.Add(new DataArtifactReferenceScriptsNode(reference, stage, application));
            Nodes.Add(new DataArtifactReferenceSchemaItemsNode(reference.ReferencedArtifact.Schema, reference));
        }

        public DataArtifactReference Reference
        {
            get => (DataArtifactReference)Tag;
            set => Tag = value;
        }

        public IDataStage Stage { get; set; }

        public IDataApplication Application { get; set; }

        public override string ExpandedImage => "Data Artifact";

        public override string CollapsedImage => "Data Artifact";
    }
}
