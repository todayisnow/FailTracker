using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace FailTracker.Web.Filters
{
    [AttributeUsage(AttributeTargets.Property, AllowMultiple = false, Inherited = true)]
    public sealed class RenderModeAttribute : Attribute , IMetadataAware
    {
        public RenderMode RenderMode { get; set; }

        public RenderModeAttribute(RenderMode renderMode)
        {
            RenderMode = renderMode;
        }

        public RenderModeAttribute()
        {
            RenderMode = RenderMode.Any;
        }
        public void OnMetadataCreated(System.Web.Mvc.ModelMetadata metadata)
        {
          
            switch (RenderMode)
            {
                case RenderMode.DisplayModeOnly:
                    metadata.ShowForDisplay = true;
                    metadata.ShowForEdit = false;
                    break;

                case RenderMode.EditModeOnly:
                    metadata.ShowForDisplay = false;
                    metadata.ShowForEdit = true;
                    break;

                case RenderMode.None:
                    metadata.ShowForDisplay = false;
                    metadata.ShowForEdit = false;
                    break;
                case RenderMode.Any:
                    metadata.ShowForDisplay = true;
                    metadata.ShowForEdit = true;
                    break;
            }
        }
    }

    public enum RenderMode
    {
        Any,
        EditModeOnly,
        DisplayModeOnly,
        None
    }
}