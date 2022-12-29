﻿namespace DLHApp.Model.Connections
{
    public abstract class Connection : ModelItem, IModelItem
    {
        public override string BasePath => "Connections";

        public TemplateReferenceCollection Templates = new TemplateReferenceCollection();
    }
}