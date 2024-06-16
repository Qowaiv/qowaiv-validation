﻿//------------------------------------------------------------------------------
// <auto-generated>
//     This code was generated by a tool.
//     Runtime Version:4.0.30319.42000
//
//     Changes to this file may cause incorrect behavior and will be lost if
//     the code is regenerated.
// </auto-generated>
//------------------------------------------------------------------------------

namespace Qowaiv.Validation.DataAnnotations {
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
    internal class QowaivValidationMessages {
        
        private static global::System.Resources.ResourceManager resourceMan;
        
        private static global::System.Globalization.CultureInfo resourceCulture;
        
        [global::System.Diagnostics.CodeAnalysis.SuppressMessageAttribute("Microsoft.Performance", "CA1811:AvoidUncalledPrivateCode")]
        internal QowaivValidationMessages() {
        }
        
        /// <summary>
        ///   Returns the cached ResourceManager instance used by this class.
        /// </summary>
        [global::System.ComponentModel.EditorBrowsableAttribute(global::System.ComponentModel.EditorBrowsableState.Advanced)]
        internal static global::System.Resources.ResourceManager ResourceManager {
            get {
                if (object.ReferenceEquals(resourceMan, null)) {
                    global::System.Resources.ResourceManager temp = new global::System.Resources.ResourceManager("Qowaiv.Validation.DataAnnotations.QowaivValidationMessages", typeof(QowaivValidationMessages).Assembly);
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
        ///   Looks up a localized string similar to The value of the {0} field is not allowed..
        /// </summary>
        internal static string AllowedValuesAttribute_ValidationError {
            get {
                return ResourceManager.GetString("AllowedValuesAttribute_ValidationError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The type {0} does not implement IEqualityComparer or IEqualityComarer&lt;object&gt;..
        /// </summary>
        internal static string ArgumentException_TypeIsNotEqualityComparer {
            get {
                return ResourceManager.GetString("ArgumentException_TypeIsNotEqualityComparer", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to All values of the {0} field should be distinct..
        /// </summary>
        internal static string DistinctValuesAttribute_ValidationError {
            get {
                return ResourceManager.GetString("DistinctValuesAttribute_ValidationError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The value of the {0} field is not a finite number..
        /// </summary>
        internal static string IsFiniteAttribute_ValidationError {
            get {
                return ResourceManager.GetString("IsFiniteAttribute_ValidationError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The length of the {0} field should be at least {1}..
        /// </summary>
        internal static string Length_AtLeast_ValdationError {
            get {
                return ResourceManager.GetString("Length_AtLeast_ValdationError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The length of the {0} field should be at most {1}..
        /// </summary>
        internal static string Length_AtMost_ValidationError {
            get {
                return ResourceManager.GetString("Length_AtMost_ValidationError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The length of the {0} field should be between {1} and {2}..
        /// </summary>
        internal static string Length_InRange_ValidationError {
            get {
                return ResourceManager.GetString("Length_InRange_ValidationError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The {0} field is required..
        /// </summary>
        internal static string MandatoryAttribute_ValidationError {
            get {
                return ResourceManager.GetString("MandatoryAttribute_ValidationError", resourceCulture);
            }
        }
        
        /// <summary>
        ///   Looks up a localized string similar to The value of the {0} field is not a multiple of {1}..
        /// </summary>
        internal static string MultipleOfAttribute_ValidationError {
            get {
                return ResourceManager.GetString("MultipleOfAttribute_ValidationError", resourceCulture);
            }
        }
    }
}
