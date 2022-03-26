﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace DLHBuilder.Desktop.UI
{
    class DataArtifactNode : ProjectTreeNode
    {
        public DataArtifactNode(DataArtifact artifact)
        {
            Artifact = artifact;

            Text = Artifact.Name;
            Nodes.Add(new DataSourcesNode(artifact.DataSources));
            Nodes.Add(new DataArtifactSchemaItemsNode(artifact.Schema, artifact.DataSources));
        }

        public DataArtifact Artifact
        {
            get => (DataArtifact)Tag;
            set
            {
                value.PropertyUpdated += OnPropertyUpdated;
                Tag = value;
            }
        }

        public override string CollapsedImage => "Data Artifact";

        public override string ExpandedImage => "Data Artifact";

        void OnPropertyUpdated(object sender, EventArgs e)
        {
            Text = Artifact.Name;
        }

        public override void LabelChanged(string text)
        {
            Artifact.Name = text;
            base.LabelChanged(text);
        }
    }
}
