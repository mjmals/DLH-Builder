﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace DLHApp.Model.Properties {
    using System;
    
    
    /// <summary>
    ///   A strongly-typed resource class, for looking up localized strings, etc.
    /// </summary>
    // This class was auto-generated by the StronglyTypedResourceBuilder
    // class via a tool like ResGen or Visual Studio.
    // To add or remove a member, edit your .ResX file then rerun ResGen
    // with the /str option, or rebuild your VS project.
    [global::System.CodeDom.Compiler.GeneratedCodeAttribute("System.Resources.Tools.StronglyTypedResourceBuilder", "17.0.0.0")]
    [global::System.Diagnostics.DebuggerNonUserCodeAttribute()]
    [global::System.Runtime.CompilerServices.CompilerGeneratedAttribute()]
    internal class Resources {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal Resources() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("DLHApp.Model.Properties.Resources", typeof(Resources).Assembly);
                    resourceMan = temp;
                }
                return resourceMan;
            }
        }
        
        /// <summary>
        ///   Overrides the current thread's CurrentUICulture property for all
        ///   resource lookups using this strongly typed resource class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Globalization.CultureInfo Culture {
            get {
                return resourceCulture;
            }
            set {
                resourceCulture = value;
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to SELECT
        ///	FORMATMESSAGE(&apos;%s.%s&apos;, col.TABLE_SCHEMA, col.TABLE_NAME) AS TableFullname,
        ///	col.TABLE_SCHEMA AS SchemaName,
        ///	col.TABLE_NAME AS [TableName],
        ///	col.COLUMN_NAME AS [ColumnName],
        ///	FORMATMESSAGE(&apos;StructField(&quot;%s&quot;, %s, %s, {%s})&apos;,
        ///		col.COLUMN_NAME,
        ///		CASE
        ///			WHEN RIGHT(col.DATA_TYPE, 3) IN (&apos;int&apos;)
        ///				THEN CASE col.DATA_TYPE
        ///					WHEN &apos;bigint&apos; THEN &apos;BigIntegerDataType()&apos;
        ///					WHEN &apos;smallint&apos; THEN &apos;SmallIntegerDataType()&apos;
        ///					WHEN &apos;tinyint&apos; THEN &apos;TinyIntegerDataType()&apos;
        ///					ELSE FORMATMESSAGE [rest of string was truncated]&quot;;.
        /// </summary>
        internal static string SqlServerStructImporter {
            get {
                return ResourceManager.GetString("SqlServerStructImporter", resourceCulture);
            }
        }
    }
}
